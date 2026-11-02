using System;
using MainCharacterFile;

namespace AllyFile
{
    public class Cleric : MainCharacter
    {
        public static Cleric cleric = new(
            levelXp: 1,
            name: "Clérigo",
            health: 8,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 13,
            initiativeBonus: 1,
            attackBonus: 13,
            xp: 0
        );

        public override void LevelStats()
        {
            // Level-based stat increases go here
            // Called when the character actually levels up, not every turn
        }

        public Cleric(
            int levelXp,
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                levelXp,
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }
    }

    public class Archer : MainCharacter
    {
        public static Archer archer = new(
            levelXp: 1,
            name: "Arqueiro",
            health: 8,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 12,
            initiativeBonus: 2,
            attackBonus: 12,
            xp: 0
        );

        public override void LevelStats()
        {
            // Archer damage scaling
            if (Level >= 5)
            {
                DamageBonus = Math.Max(DamageBonus, 3);
            }

            if (Level >= 9)
            {
                DamageBonus = Math.Max(DamageBonus, 4);
            }
        }

        public Archer(
            int levelXp,
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                levelXp,
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }
    }

    public class Wizard : MainCharacter
    {
        public static Wizard wizard = new(
            levelXp: 1,
            name: "Mago",
            health: 6,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 10,
            initiativeBonus: 0,
            attackBonus: 10,
            xp: 0
        );

        public override void LevelStats()
        {
            // Wizard scaling: health increases with level
            Health = Level * 4;

            // Damage scaling at higher levels
            if (Level >= 5)
            {
                DamageBonus = Math.Max(DamageBonus, 3);
            }

            if (Level >= 9)
            {
                DamageBonus = Math.Max(DamageBonus, 4);
            }
        }

        public Wizard(
            int levelXp,
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                levelXp,
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }
    }
}

