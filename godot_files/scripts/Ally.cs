using System;
using CharacterFile;

namespace AllyFile
{
    public class Cleric : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            
        }

        public Cleric(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state)
        {
            this.CharacterLevel = levelXp;
        }
    }

    public class Archer : Combatant
    {
        public override void CharacterDie(Combatant target)
        {

        }

        public override void ChooseWeapon()
        {
            
        }

        public Archer(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state)
        {
            this.CharacterLevel = levelXp;
        } 
    }

    public class Wizard : Combatant
    {
        public override void CharacterDie(Combatant target)
        {
            
        }

        public override void ChooseWeapon()
        {
            
        }

        public Wizard(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state)
        {
            this.CharacterLevel = levelXp;
        } 
    }
}
