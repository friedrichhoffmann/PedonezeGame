using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

using Dice;
using CharacterFile;
using MainCharacterFile;
using PhyAttack;
using MagicSystem;
using CardFile;

using static CombatManagerFile.CombatManager;
using static EventSystem.EventHandler;
using EventSystem;

namespace GenericEnemy
{
    public class Enemy(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Combatant(name, health, armor, initiativeBonus, xp) {
        public static List<(string Name, Func<Attack> Create)> AttackSet { get; set; } = new();
        public List<Spell> SpellSet { get; set; } = new();

        public EnemyCard Card { get; set; }
        public AnimatedSprite2D AttackSetAnimation { get; set; }

        private string actionName;

        private const int PHYSICAL_ATTACK = 1;
        private const int MAGIC_ATTACK = 2;

        private MainCharacter RandomTarget() {
            List<MainCharacter> targets = Combatant.All
                .OfType<MainCharacter>()
                .Where(character => character.Health > 0)
                .ToList();

            return targets.Count == 0
                ? null
                : targets[Random.Shared.Next(targets.Count)];
        }

        private Attack RandomAttack() { 
            var selected = AttackSet[Random.Shared.Next(AttackSet.Count)];
            actionName = selected.Name;
            return selected.Create();
        }

        private Spell RandomSpell() {
            List<Spell> spells = SpellSet
                .Where(spell => spell != null)
                .ToList();

            return spells.Count == 0
                ? null
                : spells[Random.Shared.Next(spells.Count)];
        }

        public void EnemyTurn() {
            MainCharacter target = RandomTarget();
            if (target == null)
                return;

            bool canAttack = AttackSet.Any(factory => factory.Create != null);
            bool canCastSpell = SpellSet.Any(spell => spell != null);

            if (!canAttack && !canCastSpell)
                return;

            int action = canAttack && canCastSpell
                ? Die.Roll(1, Die.d2)
                : canAttack ? PHYSICAL_ATTACK : MAGIC_ATTACK;

            if (action == PHYSICAL_ATTACK) {
                Attack attack = RandomAttack();
                if (attack == null)
                    return;
                ThrowEvent(new Event("FadeOut UI", EventKind.Animation,
                            EventBody.Animation(animationManager, "FadeOut UI")));
                ThrowEvent(new Event("Makes Attack Animation Visible", EventKind.ToggleVisibility,
                            EventBody.ToggleVisibility(AttackSetAnimation)));
                ThrowEvent(new Event("Attack Animation", EventKind.Animation,
                            EventBody.Animation(AttackSetAnimation, actionName)));
                ThrowEvent(new Event("Makes Attack Animation Visible", EventKind.ToggleVisibility,
                            EventBody.ToggleVisibility(AttackSetAnimation)));
                ThrowEvent(new Event("Slide Character Back Up", EventKind.Animation,
                            EventBody.Animation(animationManager, "SlideUpCharacters", 0)));
                ThrowEvent(new Event("FadeIn UI", EventKind.Animation,
                            EventBody.Animation(animationManager, "FadeIn UI", 150)));

                switch (attack.Range) {
                    case ActRange.Broad:
                        AttackTarget(attack);
                        break;

                    case ActRange.Focused:
                        AttackTarget(attack, target);
                        break;
                }

                return;
            }

            Spell spell = RandomSpell();
            if (spell == null)
                return;

            switch (spell.Range) {
                case ActRange.Broad:
                    CastSpell(spell);
                    break;

                case ActRange.Focused:
                    CastSpell(spell, target);
                    target.Card?.UpdateValues(target);
                    break;

                case ActRange.Self:
                    CastSpell(spell, this);
                    break;
            }
        }
    }
}

