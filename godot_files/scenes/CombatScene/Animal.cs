using System;
using CharacterFile;
using EnumFile;

namespace AnimalFile
{
    public class BigRat : Combatant
    {
        public static BigRat rat = new("Rato",  7, DamageType.None, DamageResistance.None, 0, 0, 13, 3, 5, 25, false);

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.RatBite);
            DamageType = DamageType.PiercingDamage;
        }

        public BigRat(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor, int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Boar : Combatant
    {
        public static Boar boar = new("Javali", 13,  DamageType.None, DamageResistance.None, 0, 0, 11, 0, 3, 50, false);
        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.BoarCharge);
        }
        
        public Boar(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Deer : Combatant
    {
        public static Deer deer = new("Cervo", 11, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 5, 50, false);
        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.DeerHorn);
        }

        public Deer(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class ItalianWolf : Combatant
    {
        public static ItalianWolf wolfOne = new("Lobo-Itálico um", 22,  DamageType.None, DamageResistance.None, 0, 0, 14, 2, 5, 200, false);
        public static ItalianWolf wolfTwo = new("Lobo-Itálico dois", 22,  DamageType.None, DamageResistance.None, 0, 0, 14, 2, 5, 200, false);

        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.ItalianWolfBite);
        }
        
        public ItalianWolf(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class BrownBear : Combatant
    {
        public static BrownBear bear = new("Urso-Pardo-Misciano", 22, DamageType.None, DamageResistance.None, 0, 0, 11, 1, 5, 200, false);

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
        public static BigBoar bigBoar = new("Javali Grande", 42, DamageType.None, DamageResistance.None, 0, 3, 13, 0, 5, 450, false);
        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.BoarCharge);
        }

        public BigBoar(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class BigDeer : Combatant
    {
        public static BigDeer bidDeer = new("Grande Cervo", 42, DamageType.None, DamageResistance.None, 0, 4, 14, 6, 6, 450, false);
        public override void ChooseWeapon()
        {
            Weapons.Add(ActionType.BigDeerHorn);
        }
        
        public BigDeer(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Hippopotamus : Combatant
    {
        public static Hippopotamus hippopotamus = new("Hipopótamos", 82, DamageType.None, DamageResistance.None, 0, 5, 14, -2, 0, 1100, false);
        public override void ChooseWeapon()
        {
            int var = rng.Next(1, 6);
        }

        public Hippopotamus(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }

    public class Unicorn : Combatant
    {
        public static Unicorn unicorn = new("Unicórnio", 97, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 5, 10, false);   
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