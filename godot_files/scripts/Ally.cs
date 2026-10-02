using System;
using CharacterFile;


namespace AllyFile
{
    public class Cleric : Combatant
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

        public Cleric(int health, int damage, int dropXp)
            : base(health, damage, dropXp) {}    
    }

    public class Archer : Combatant
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

        public Archer(int health, int damage, int dropXp)
            : base(health, damage, dropXp)
        {
            
        }
    }

    public class Wizard : Combatant
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

        public Wizard(int health, int damage, int dropXp)
            : base(health, damage, dropXp) {}
    }
}