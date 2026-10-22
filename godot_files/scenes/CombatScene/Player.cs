using System;
using System.Threading;
using System.ComponentModel;
using System.Security.Cryptography;
using CharacterFile;
using EnumFile;
using AllyFile;
using EnemyFile;
using AnimalFile;

namespace PlayerFile
{
    public class Player : MainCharacter
    {
        public static Player player = new(1, "Jogador", 10, DamageType.None,  DamageResistance.None, 0, 0, 10, 0, 0, 10,false);
        
        public new static int Armor = 13;
        public new static int StateTurns = 0;
        public new static int EffectTurns = 0;

        public override void LevelStats()
        {
            Health += Level * 3;
            
        }

        public override void ChooseWeapon()
        {
            
        }

        public Player(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor, int initiativeBonus, int attackBonus, int xp, bool state)
            : base(levelXp, name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state) {}
    }
}