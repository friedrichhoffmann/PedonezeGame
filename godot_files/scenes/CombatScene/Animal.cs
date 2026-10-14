using System;
using CharacterFile;
using EnumFile;

namespace AnimalFile
{
    public class BigRat : Combatant
    {
        public static BigRat rat = new("Rato",  7, DamageType.None, DamageResistance.None, 0, 0, 13, 3, 5, 25, false);
        
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.RatBite);
            DamageType = DamageType.PiercingDamage;
        }

        public BigRat(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Boar : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }
        
        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.BoarCharge);
        }
        
        public Boar(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Deer : Combatant
    {
        public override void CharacterDie(Combatant target)
        {

        }

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.DeerHorn);
        }

        public Deer(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class ItalianWolf : Combatant
    {
        public override void CharacterDie(Combatant target)
        {

        }

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.ItalianWolfBite);
        }
        
        public ItalianWolf(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class BrownBear : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            int weapon = RollDice(1, 5);
            if (weapon <= 2)
            {
                Weapons.Add(ActionType.BrownBearBite);
            }
            else if (weapon <= 4)
            {
                Weapons.Add(ActionType.BrownBearCrawl);
                DamageType = DamageType.SlashingDamage;
            }
            else
            {
                Weapons.Add(ActionType.BrownBearBite);
                Weapons.Add(ActionType.BrownBearCrawl);
            }
        }
        
        public BrownBear(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class BigBoar : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.BoarCharge);
        }

        public BigBoar(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class BigDeer : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
               
        }

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.BigDeerHorn);
        }
        
        public BigDeer(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Hippopotamus : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
                  
        }
        
        public override void ChooseWeapon()
        {
            int var = rng.Next(1, 6);
        }

        public Hippopotamus(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Unicorn : Combatant
    {
        public override void CharacterDie(Combatant target)
        {

        }
        
        public override void ChooseWeapon()
        {
            int weapon = rng.Next(1, 21);
            if (weapon <= 10) 
            {
                Weapons.Add(ActionType.UnicornKick);
            }
            else if (weapon <= 16)
            {
                
            }
        }
        
        public Unicorn(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }
}