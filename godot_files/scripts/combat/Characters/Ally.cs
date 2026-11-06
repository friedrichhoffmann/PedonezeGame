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

