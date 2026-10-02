using System;
using CharacterFile;

namespace AnimalFile
{
    public class BigRat : Combatant
    {
        public override void CharacterDie()
        {

        }

        public override void CharacterAttack(int victimHealth)
        {
            victimHealth -= rng.Next(4, 8);
        }

        public override void CharacterDefend()
        {

        }
        
        public BigRat(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }

    public class Boar : Combatant
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
        
        public Boar(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }

    public class Deer : Combatant
    {
        public override void CharacterDie()
        {

        }

        public override void CharacterAttack(int victimHealth)
        {
            victimHealth -= rng.Next(4, 10);
        }

        public override void CharacterDefend()
        {

        }
        
        public Deer(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }

    public class ItalianWolf : Combatant
    {
        public override void CharacterDie()
        {

        }

        public override void CharacterAttack(int victimHealth)
        {
            victimHealth -= rng.Next(4, 14);
        }

        public override void CharacterDefend()
        {

        }
        
        public ItalianWolf(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }

    public class BrownBear : Combatant
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
        
        public BrownBear(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }
}
