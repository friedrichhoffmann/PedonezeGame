using System;

namespace CharacterFile
{
    public enum WeaponType
    {
        BanditScimitar,
        BanditCrossbow,
        TribalWarriorSpear,
        PlebeianStick,
        RatBite,
        BoarCharge,
        DeerHorn,
        ItalianWolfBite,
        BrownBearBite,
        BrownBearCrawl,
        BanditCaptainScimitar,
        BanditCaptainDagger,
        CentaurSpear,
        CentaurLongBow,
        BigBoarCharge,
        BigDeerHorn,
        HippopotamusBite,
        UnicornKick,
        UnicornHorn,
    }
    
    public enum DamageType
    {
        None,
        PiercingDamage,
        SlashingDamage,
        BludgeoningDamage
    }
        
    public enum DamageResistance
    {
        None,
        PiercingResistance,
        SlashingResistance,
        BludgeoningResistance
    }

    public abstract class Character2D
    {
        public string CharacterName;
        public int CharacterHealth;
        public int CharacterDropXp;
        
        public string NamePropertie
        {
            get => CharacterName;
            set => CharacterName = value;
        }

        public int HealthPropertie
        {
            get => CharacterHealth;
            set => CharacterHealth = Math.Clamp(value, 0, 11);
        }

        public int XpPropertie
        {
            get => CharacterDropXp;
            set => CharacterDropXp = Math.Clamp(value, 1, 100);
        }

        public int CharacterGiveDamage(int damage, Combatant target, Combatant attacker)
        {
            switch (attacker.CharacterWeaponType)
            {
                case WeaponType.BanditScimitar:
                    damage = attacker.CharacterRollDamage(2, 8);
                    break;
                case WeaponType.BanditCrossbow:
                    damage = attacker.CharacterRollDamage(2, 10);
                    break;
                case WeaponType.TribalWarriorSpear:
                    damage = attacker.CharacterRollDamage(2, 8);
                    break;
                case WeaponType.PlebeianStick:
                    damage = attacker.CharacterRollDamage(1, 5);
                    break;
                case WeaponType.RatBite:
                    damage = attacker.CharacterRollDamage(4, 8);
                    break;
                case WeaponType.BoarCharge:
                    damage = attacker.CharacterRollDamage(2, 8);
                    break;
                case WeaponType.DeerHorn:
                    damage = attacker.CharacterRollDamage(4, 10);
                    break; 
                case WeaponType.ItalianWolfBite:
                    damage = attacker.CharacterRollDamage(4, 15);
                    break;
                case WeaponType.BrownBearBite:
                    damage = attacker.CharacterRollDamage(4, 14);
                    break;
                case WeaponType.BrownBearCrawl:
                    damage = attacker.CharacterRollDamage(4, 8);
                    break;
                case WeaponType.BanditCaptainScimitar:
                    damage = attacker.CharacterRollDamage(1, 7);
                    break; 
                case WeaponType.BanditCaptainDagger:
                    damage = attacker.CharacterRollDamage(1, 5);
                    break;
                case WeaponType.CentaurSpear:
                    damage = attacker.CharacterRollDamage(1, 9);
                    break; 
                case WeaponType.CentaurLongBow:
                    damage = attacker.CharacterRollDamage(1, 9);
                    break;
                case WeaponType.BigBoarCharge:
                    damage = attacker.CharacterRollDamage(1, 7) + attacker.CharacterRollDamage(1, 7);
                    break;
                case WeaponType.BigDeerHorn:
                    damage = attacker.CharacterRollDamage(1, 7) + attacker.CharacterRollDamage(1, 7);;
                    break;
                case WeaponType.HippopotamusBite:
                    damage = attacker.CharacterRollDamage(1, 11) + attacker.CharacterRollDamage(1, 11);
                    break;
                case WeaponType.UnicornKick:
                    damage = attacker.CharacterRollDamage(1, 7) + attacker.CharacterRollDamage(1, 7);
                    break;
                case WeaponType.UnicornHorn:
                    damage = attacker.CharacterRollDamage(1, 11);
                    break;
            }
            
            if (attacker.CharacterDamageType == DamageType.SlashingDamage && target.CharacterResistanceDamageType == DamageResistance.SlashingResistance)
            {
                target.CharacterHealth -= (damage - target.CharacterResistanceBonus);
            }

            else if (attacker.CharacterDamageType == DamageType.PiercingDamage && target.CharacterResistanceDamageType == DamageResistance.PiercingResistance)
            {
                target.CharacterHealth -= (damage - target.CharacterResistanceBonus);
            }

            else if (attacker.CharacterDamageType == DamageType.BludgeoningDamage && target.CharacterResistanceDamageType == DamageResistance.BludgeoningResistance)
            {
                target.CharacterHealth -= (damage - target.CharacterResistanceBonus);
            }
            
            else
            {
                target.CharacterHealth -= damage;
            }

            if (target.CharacterHealth <= 0)
            {
                target.CharacterHealth = Math.Max(0, target.CharacterHealth);
            }

            return damage;
        }

        public abstract void CharacterDie(Combatant target);
    }

    public abstract class Npc : Character2D
    {
        public int CharacterLevel;
        public abstract void CharacterQuote();
        public abstract void CharacterRoutine();

        protected Npc(int health, int dropXp)
        {
            CharacterHealth = health;
            CharacterDropXp = dropXp;
        }
    }

    public abstract partial class Combatant : Character2D
    {
        public int CharacterLevel;
        public WeaponType CharacterWeaponType;
        public DamageType CharacterDamageType;
        public DamageResistance CharacterResistanceDamageType;
        public bool CharacterGood;

        public List<WeaponType> CharacterWeapons { get; set; }
        
        public Random rng = new Random();

        public int CharacterArmor;
        public int CharacterIniciativeBonus;
        public int CharacterAttackBonus;
        public int CharacterDamageBonus;
        public int CharacterResistanceBonus;

        protected Combatant(string name, int health, DamageType damage, DamageResistance resistance, 
            int resistanceBonus, int damageBonus, int armor, int iniciativeBonus, int attackBonus, int XP, bool state)
        {
            CharacterName = name;
            CharacterHealth = health;
            CharacterDamageType = damage;
            CharacterWeapons = new List<WeaponType>();
            CharacterResistanceDamageType = resistance;
            CharacterResistanceBonus = resistanceBonus;
            CharacterDamageBonus = damageBonus;
            CharacterArmor = armor;
            CharacterIniciativeBonus = iniciativeBonus;
            CharacterAttackBonus = attackBonus;
            CharacterDropXp = XP;
            CharacterGood = state;
        }
    }

    public abstract partial class Combatant : Character2D // Métodos
    {
        public abstract void ChooseWeapon();

        public void Stats()
        {
            Console.WriteLine($"\n{CharacterName} possui {CharacterHealth} pontos de vida e {CharacterArmor} pontos de armadura.");
        }

        public int RollD20()
        {
            int var = rng.Next(1, 21);
            return var;
        }

        public int CharacterRollDamage(int numberOne, int numberTwo)
        {
            int var = rng.Next(numberOne, numberTwo);
            return var;
        }
    }

    public abstract partial class Combatant : Character2D // Propriedades
    {
        public int LevelPropertie
        {
            get => CharacterLevel;
            set => CharacterLevel = Math.Clamp(value, 1, 11);
        }
        
        public int ArmorPropertie
        {
            get => CharacterArmor;
            set => CharacterArmor = Math.Clamp(value, 0, 20);
        }

        public int IniciativePropertie
        {
            get => CharacterIniciativeBonus;
            set => CharacterIniciativeBonus = Math.Clamp(value, 0, 20);
        }

        public int AttackPropertie
        {
            get => CharacterAttackBonus;
            set => CharacterAttackBonus = Math.Clamp(value, 0, 20);
        }

        public int DamagePropertie
        {
            get => CharacterAttackBonus;
            set => CharacterAttackBonus = Math.Clamp(value, 0, 20);
        }

        public int ResistancePropertie
        {
            get => CharacterResistanceBonus;
            set => CharacterResistanceBonus = Math.Clamp(value, 0, 20);
        }
    }
}
