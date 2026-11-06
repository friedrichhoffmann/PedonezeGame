using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

using Dice;

using CharacterFile;
using MainCharacterFile;

using PhyAttack;
using MagicSystem;

namespace GenericEnemy {
    public abstract class Enemy(
        string name,
        int health,
        int resistanceBonus,
        int damageBonus,
        int armor,
        int initiativeBonus,
        int attackBonus,
        int xp
    ) : Combatant(name, health, 
        resistanceBonus, 
        damageBonus, armor, 
        initiativeBonus, 
        attackBonus, xp
    ) {
        public static List<Func<Attack>> AttackSet { get; set; }
        public static List<Spell> SpellSet { get; set; }

        private Combatant RandomTarget() {
            List<Combatant> mainCharacters = Combatant.All.Where(chr => chr is MainCharacter).ToList();
            int targetIndex = Random.Shared.Next(0, 1+mainCharacters.Count());
            Combatant target = mainCharacters[targetIndex];

            return target;
        }

        private Attack RandomAttack() {
            int attackIndex = Random.Shared.Next(0, AttackSet.Count());
            Attack attack = AttackSet[attackIndex]();

            return attack;
        }

        private Spell RandomSpell() {
            int spellIndex = Random.Shared.Next(0, AttackSet.Count());
            Spell spell  = SpellSet[spellIndex];

            return spell;
        }

        private bool PhysicAttackAvailable() {
            return AttackSet.Count > 0;
        }

        private bool MagicAttackAvailable() {
            return SpellSet.Count > 0;
        }

        // the sole purpouse of this function is verbosity
        private bool HaveNot(bool verb) { return !verb; }

        private const int PHYSIC_ATTACK = 1;
        private const int MAGIC_ATTACK = 2;

        public void EnemyTurn() {
            Combatant target = RandomTarget();
            bool played = false;

            while (HaveNot(played)) {
                int action = Die.Roll(1, Die.d2);
                
                switch (action) {
                    case PHYSIC_ATTACK: {
                        if (!PhysicAttackAvailable()) { continue; }
                        
                        switch (RandomAttack()) {
                            case var atk when atk.Range == ActRange.Broad: {
                                AttackTarget(atk);
                            } break;
                            case var atk when atk.Range == ActRange.Focused: {
                                AttackTarget(atk, RandomTarget());
                            } break;                                                                 
                        }
                        
                        played = true;
                    } break;

                    case MAGIC_ATTACK: {
                        if (!MagicAttackAvailable()) { continue; }
                        
                        switch (RandomSpell()) {
                            case var spl when spl.Range == ActRange.Broad: {
                                CastSpell(spl);
                            } break;
                            case var spl when spl.Range == ActRange.Focused: {
                                CastSpell(spl, RandomTarget());
                            } break;
                            case var spl when spl.Range == ActRange.Self: {
                                CastSpell(spl, this);
                            } break;
                        }
                        
                        played = true;
                    } break;
                }
            }
        }
    }
}
