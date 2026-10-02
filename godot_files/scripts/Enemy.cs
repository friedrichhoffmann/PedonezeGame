using System;
using MainProgram;
using System.Security.Cryptography;
using CharacterFile;

namespace EnemyFile
{ 
    public class Bandit : Combatant, IChooseWeapon
    {
        public int ChooseWeapon()
        {
            int chooseMethod = rng.Next(1, 6);
            return chooseMethod;
        }

        public override void CharacterDie()
        {

        }

        public override void CharacterAttack(int victimHealth)
        {
            int weapon = ChooseWeapon();
            if (weapon <= 2)
            {
                victimHealth -= rng.Next(2, 8);
            }
            else
            {
                victimHealth -= rng.Next(2, 10);
            }
        }

        public override void CharacterDefend()
        {

        }

        public Bandit(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }

    public class Plebeian : Combatant
    {
        public override void CharacterDie()
        {

        }

        public override void CharacterAttack(int victimHealth)
        {

        }

        public override void CharacterDefend()
        {

        }

        public Plebeian(int health, int damage, int dropXp)
            : base(health, damage, dropXp) {}
    }

    public class TribalWarrior : Combatant
    {
        public override void CharacterDie()
        {

        }

        public override void CharacterAttack(int victimHealth)
        {
            victimHealth -= rng.Next(2, 8);
        }

        public override void CharacterDefend()
        {

        }
        
        public TribalWarrior(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }
}