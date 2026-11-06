using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

using PhyAttack;
using MagicSystem;

namespace CharacterFile
{
    public abstract class Character2D
    {
        protected int healthMax;
        private int health;
        private int xp;

        public readonly int HealthMax;
        public string Name { get; set; }
        
        public int Health {
            get => health;
            set => health = Math.Clamp(value, 0, healthMax);
        }

        public int Xp {
            get => xp;
            set => xp = Math.Clamp(value, 1, 100);
        }
    }

    public abstract partial class Combatant : Character2D
    {
        public static Random rng = new();

        protected Combatant(
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
        {
            Name = name;
            Health = health;
            ResistanceBonus = resistanceBonus;
            DamageBonus = damageBonus;
            Armour = armor;
            InitiativeBonus = initiativeBonus;
            AttackBonus = attackBonus;
            Xp = xp;
        }

        public int Id { get; set; }
        public int PastState { get; set; }

        public List<int> StateTurns { get; set; }

        private int armour;

        private int initiativeBonus;
        private int attackBonus;
        private int damageBonus;
        private int resistanceBonus;

        private int healthCopy;
        private int armourCopy;

        public List<Effect> ActiveEffects { get; set; }
        public List<Resistance> Resistances { get; set; }

        public static List<Combatant> Hero { get; set; }
        public static List<Combatant> Enemy { get; set; }
        public static List<Combatant> All { get; set; }

        public int Armour
        {
            get => armour;
            set => armour = Math.Clamp(value, 0, 20);
        }

        public int InitiativeBonus
        {
            get => initiativeBonus;
            set => initiativeBonus = Math.Clamp(value, 0, 20);
        }

        public int AttackBonus
        {
            get => attackBonus;
            set => attackBonus = Math.Clamp(value, 0, 20);
        }

        public int DamageBonus
        {
            get => damageBonus;
            set => damageBonus = Math.Clamp(value, 0, 20);
        }

        public int ResistanceBonus
        {
            get => resistanceBonus;
            set => resistanceBonus = Math.Clamp(value, 0, 20);
        }


            }
    // methods/functions
    public abstract partial class Combatant {        
        public void Stats()
        {
            GD.Print(
                $"\n{Name}: {Health} HP, " +
                $"{ResistanceBonus} RB, " +
                $"{damageBonus} DB, {Armour} AC, {InitiativeBonus} IB, " +
                $"{Xp} XP");
        }

        public bool ValidTurn() {
            return 
                ActiveEffects.Any(eff => eff.Kind == EffectKind.Numbness) ||
                Health <= 0;
        }

        public void AttackTarget(Attack attack, Combatant target) {
            attack = attack.ApplyResistance(target);
            attack = attack.ApplyBoost(this);
            target.Health -= attack.Damage;
        }

        public void AttackTarget(Attack attack) {
            if (attack.Range == ActRange.Broad) {
                foreach (Combatant target in All) {
                    attack = attack.ApplyResistance(target);
                    attack = attack.ApplyBoost(this);
                    target.Health -= attack.Damage;
                }
            } else {
                throw new ArgumentException("You mut provide a target for an attack of range focused", nameof(ActRange));
            }
        }


        public void RemoveCharacter()
        {
            if (Health != 0)
            {
                return;
            }

            if (Hero.Contains(this))
            {
                Hero.Remove(this);
            }
            else if (Enemy.Contains(this))
            {
                Enemy.Remove(this);
            }
        }

    }
}
