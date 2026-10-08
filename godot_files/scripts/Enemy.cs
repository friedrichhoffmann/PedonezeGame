using System;
using System.Security.Cryptography;
using CharacterFile;

namespace EnemyFile
{ 
	public class Bandit : Combatant
	{
		public override void ChooseWeapon()
		{
			int weapon = rng.Next(1, 6);
			if (weapon <= 2)
			{
				CharacterWeapons.Add(WeaponType.BanditScimitar);
				CharacterDamageType = DamageType.SlashingDamage;
			}
			else
			{
				CharacterWeapons.Add(WeaponType.BanditCrossbow);
				CharacterDamageType = DamageType.PiercingDamage;
			}
		}
		
		public override void CharacterDie(Combatant target)
		{
			   
		}
		
		public Bandit(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
			: base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
	}

	public class Plebeian : Combatant
	{
		public override void CharacterDie(Combatant target)
		{
			
		}

		public override void ChooseWeapon()
		{
			CharacterWeapons.Add(WeaponType.PlebeianStick);
			CharacterDamageType = DamageType.BludgeoningDamage;
		}

		public Plebeian(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
			: base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
	}

	public class TribalWarrior : Combatant
	{
		public override void CharacterDie(Combatant target)
		{
			
		}

		public override void ChooseWeapon()
		{
			CharacterWeapons.Add(WeaponType.TribalWarriorSpear);
			CharacterDamageType = DamageType.PiercingDamage;
		}
		
		
		public TribalWarrior(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
			: base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
	}

	public class BanditCaptain : Combatant
	{
		int weapon;
		
		public override void CharacterDie(Combatant target)
		{
			
		}
		
		public override void ChooseWeapon()
		{
			weapon = rng.Next(1, 6);
			if (weapon <= 2)
			{
				CharacterWeapons.Add(WeaponType.BanditCaptainScimitar);
				CharacterDamageType = DamageType.SlashingDamage;
			}
			else if (weapon <= 4)
			{
				CharacterWeapons.Add(WeaponType.BanditCaptainDagger);
				CharacterDamageType = DamageType.PiercingDamage;
			}
			else
			{
				CharacterWeapons.Add(WeaponType.BanditCaptainScimitar);
				CharacterDamageType = DamageType.SlashingDamage;
				CharacterWeapons.Add(WeaponType.BanditCaptainScimitar);
				CharacterDamageType = DamageType.SlashingDamage;
				CharacterWeapons.Add(WeaponType.BanditCaptainDagger);
				CharacterDamageType = DamageType.PiercingDamage;
			}
		}
		
		public BanditCaptain(string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor,int iniciativeBonus, int attackBonus, int xp, bool state)
			: base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, iniciativeBonus, attackBonus, xp, state) {}
	}
}
