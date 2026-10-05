using System;
using CharacterFile;

namespace AnimalFile
{
    public class BigRat : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            CharacterWeapons.Add(WeaponType.RatBite);
            CharacterDamageType = DamageType.PiercingDamage;
        }

        public BigRat(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
    }

    public class Boar : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }
        
        public override void ChooseWeapon()
        {
            CharacterWeapons.Add(WeaponType.BoarCharge);
            CharacterDamageType = DamageType.BludgeoningDamage;
        }
        
        public Boar(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
    }

    public class Deer : Combatant
    {
        public override void CharacterDie(Combatant target)
        {

        }

        public override void ChooseWeapon()
        {
            CharacterWeapons.Add(WeaponType.DeerHorn);
            CharacterDamageType = DamageType.PiercingDamage;
        }

        public Deer(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
    }

    public class ItalianWolf : Combatant
    {
        public override void CharacterDie(Combatant target)
        {

        }

        public override void ChooseWeapon()
        {
            CharacterWeapons.Add(WeaponType.ItalianWolfBite);
            CharacterDamageType = DamageType.PiercingDamage;
        }
        
        public ItalianWolf(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
    }

    public class BrownBear : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            int weapon = rng.Next(1, 6);
            if (weapon <= 2)
            {
                CharacterWeapons.Add(WeaponType.BrownBearBite);
                CharacterDamageType = DamageType.PiercingDamage;
            }
            else if (weapon <= 4)
            {
                CharacterWeapons.Add(WeaponType.BrownBearCrawl);
                CharacterDamageType = DamageType.SlashingDamage;
            }
            else
            {
                CharacterWeapons.Add(WeaponType.BrownBearBite);
                CharacterDamageType = DamageType.PiercingDamage;
                CharacterWeapons.Add(WeaponType.BrownBearCrawl);
                CharacterDamageType = DamageType.SlashingDamage;
            }
        }
        
        public BrownBear(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
    }

    public class BigBoar : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            CharacterWeapons.Add(WeaponType.BoarCharge);
            CharacterDamageType = DamageType.BludgeoningDamage;
        }

        public BigBoar(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
    }

    public class BigDeer : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
               
        }

        public override void ChooseWeapon()
        {
            CharacterWeapons.Add(WeaponType.BigDeerHorn);
            CharacterDamageType = DamageType.PiercingDamage;
        }
        
        public BigDeer(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
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

        public Hippopotamus(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
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
                CharacterWeapons.Add(WeaponType.UnicornKick);
                CharacterDamageType = DamageType.BludgeoningDamage;
            }
            else if (weapon <= 16)
            {
                
            }
        }
        
        public Unicorn(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
    }
}
