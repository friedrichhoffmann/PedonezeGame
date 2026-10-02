using System;

namespace CharacterFile
{
    public abstract class Character2D
    {
        public int CharacterHealth;
        public int CharacterDropXp;

        public int HealthPropertie
        {
            get => CharacterHealth;
            set => CharacterHealth = Math.Clamp(value, 0, 11);
        }
        public int XpPropertie
        {
            get => CharacterDropXp;
            set => CharacterDropXp = Math.Clamp(value, 1, 30);
        }

        public abstract void CharacterDie();
    }
    public abstract class Npc : Character2D
    {
        public abstract void CharacterQuote();
        public abstract void CharacterRoutine();

        protected Npc(int health, int dropXp)
        {
            this.CharacterHealth = health;
            this.CharacterDropXp = dropXp;
        }
    }
    
    public abstract class Combatant : Character2D
    {
        public enum WeaponType
        {
            None,
            SmallCrossbow,
            Scimitar,
            Bow,
            Stick,
        } 
        
        public int CharacterDamage;

        public Random rng  = new Random();
        public int DamagePropertie
        {
            get => CharacterDamage;
            set => CharacterDamage = Math.Clamp(value, 1, 11);
        }
        
        public abstract void CharacterAttack(int victimHealth);
        public abstract void CharacterDefend();

        protected Combatant(int health, int damage, int dropXp)
        {
            this.CharacterHealth = health;
            this.CharacterDamage = damage;
            this.CharacterDropXp = dropXp;
        }
    }
    public interface IChooseWeapon
    {
        public abstract int ChooseWeapon();
    }
}