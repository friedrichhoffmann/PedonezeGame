using System;
using System.Security.Cryptography;
using CharacterFile;
using EnumFile;

namespace EnemyFile
{ 
    public class Bandit : Combatant
    {
        public static Bandit banditOne = new("Bandido 1", 11, DamageType.None, DamageResistance.None, 0, 3, 12, 1, 1, 25, false);
        public static Bandit banditTwo = new("Bandido 2", 11, DamageType.None, DamageResistance.None, 0, 3, 12, 1, 1, 25, false);
        public override void ChooseWeapon()
        {
            int weapon = rng.Next(1, 6);
            if (weapon <= 2)
            {
                Weapons.Add(ActionType.BanditScimitar);
            }
            else
            {
                Weapons.Add(ActionType.BanditCrossbow);
            }
        }
        
        public Bandit(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Plebeian : Combatant
    {
        public static Plebeian plebeian = new("Plebeu", 4, DamageType.None, DamageResistance.None, 0, 0, 10, 1, 2, 10, false);

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.PlebeianStick);
        }

        public Plebeian(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class TribalWarrior : Combatant
    {
        public static TribalWarrior tribalWarrior = new("Guerreiro Tribal", 11, DamageType.None, DamageResistance.None, 0, 0, 12, 0, 3, 25, false);

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.TribalWarriorSpear);
        }
        
        
        public TribalWarrior(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }
    
    
    
    public class BanditCaptain : Combatant
    {
        public static BanditCaptain banditCaptain = new("Capitão dos Bandidos", 65, DamageType.None, DamageResistance.None, 0, 2, 15, 3, 3, 450, false);
        
        int weapon;
        
        public override void ChooseWeapon()
        {
            weapon = rng.Next(1, 6);
            if (weapon <= 2)
            {
                Weapons.Add(ActionType.BanditCaptainScimitar);
            }
            else if (weapon <= 4)
            {
                Weapons.Add(ActionType.BanditCaptainDagger);
            }
            else
            {
                Weapons.Add(ActionType.BanditCaptainScimitar);
                Weapons.Add(ActionType.BanditCaptainScimitar);
                Weapons.Add(ActionType.BanditCaptainDagger);
            }
        }
        
        public BanditCaptain(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }
}