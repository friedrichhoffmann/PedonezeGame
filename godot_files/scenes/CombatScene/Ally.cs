using CharacterFile;
using EnumFile;

namespace AllyFile
{
    public class Cleric : MainCharacter
    {
        public static Cleric cleric = new(1, "Clérigo",8, DamageType.None, DamageResistance.None, 0, 0, 13, 1, 13, 0, false);
        
        public new static int Armor = 10;
        public new static int StateTurns = 0;
        public new static int EffectTurns = 0;

        public override void LevelStats()
        {
            if (Level >= 1) 
            { 
                Weapons.Add(ActionType.Guidance); 
                Weapons.Add(ActionType.Resistance); 
                Weapons.Add(ActionType.SacredFlame);
            }

            if (Level >= 2) 
            {
                
            }

        }

        public override void ChooseWeapon()
        {
            
        }

        public Cleric(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor, int initiativeBonus, int attackBonus, int xp, bool state)
            : base(levelXp, name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Archer : MainCharacter
    {
        public static Archer archer = new(1, "Arqueiro", 8, DamageType.None, DamageResistance.None, 0, 0, 12, 2, 12, 0, false);
        
        public new static int Armor = 12;
        public new static int StateTurns = 0;
        public new static int EffectTurns = 0;
        
        public override void LevelStats()
        {
            if (Level >= 5)
            {
                DamageBonus += 3;
            }
            
            else if (Level >= 9)
            {
                DamageBonus += 4;
            }
        }

        public override void ChooseWeapon()
        {
            
        }

        public Archer(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor, int initiativeBonus, int attackBonus, int xp, bool state)
            : base(levelXp, name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Wizard : MainCharacter
    {
        public static Wizard wizard = new(1, "Mago", 6, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 10, 0, false);
        
        public new static int Armor = 13;
        public new static int StateTurns = 0;
        public new static int EffectTurns = 0;
        
        public override void LevelStats()
        {
            if (Level >= 5)
            {
                DamageBonus += 3;
                if (Level >= 9)
                {
                    DamageBonus += 4;
                }
            }
            
            Health =  Level * 4;
        }

        public override void ChooseWeapon()
        {
            
        }

        public Wizard(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor, int initiativeBonus, int attackBonus, int xp, bool state)
        : base(levelXp, name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }
}