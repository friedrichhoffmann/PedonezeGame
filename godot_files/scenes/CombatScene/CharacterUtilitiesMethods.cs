using System;
using System.Collections.Generic;
using EnumFile;
using PlayerFile;
using Godot;

namespace CharacterFile
{
    public abstract partial class Combatant
    {
        public abstract void ChooseWeapon();
        

        public void Stats()
        {
            GD.Print($"\n{Name}: {Health} HP, {DamageType} DT, {ResistanceDamageType} RD, {ResistanceBonus} RB, {DamageBonus} DB, {Armor} AC, {IniciativeBonus} IB, {DropXp} XP");
        }
        
        public int RollDice(int numberOne, int numberTwo)
        {
            int first = rng.Next(numberOne, numberTwo);
            int second = rng.Next(numberOne, numberTwo);
            if (Effect.Contains(EffectType.Disadvantage))
            {
                return Math.Min(first, second);
            }
            return Math.Max(first, second);
        }

        public int RollMultipleDice(int timesRolled, int numberOne, int numberTwo)
        {
            int result = 0;
            for (int i = 0; i < timesRolled; i++)
            {
                result +=  RollDice(numberOne, numberTwo);
            }
            return result;
        }
        
        public void DealDeadCharacter(List<Combatant> list)
        {
            foreach (Combatant element in list)
            {
                if (element.Health <= 0)
                { 
                    element.CharacterDied?.Invoke();
                }
            }
        }
        
        public void CharacterDie()
        {
            if (Enemy.Contains(this))
            {
                Enemy.Remove(this);
                Player.player.DropXp += DropXp;
            }
            
            else if (Hero.Contains(this))
            {
                Hero.Remove(this);
            }
            All.Remove(this);
        }
        public void DealState()
        {
            for (int i = 0; i < State.Count - 1; i++)
            {
                switch (State[i])
                {
                    case StateType.Paralysed:
                    case StateType.Poisoned: 
                    case StateType.Incapacitated:
                        if (StateTurns[i] > 0)
                        {
                            StateTurns[i]--;
                            if (StateTurns[i] == 0)
                            {
                                State.RemoveAt(i);
                                StateTurns.RemoveAt(i);
                            } 
                        }
                        break;
                } 
            }
        }
        public void DealEffect()
        {
            for (int i = 0; i < Effect.Count - 1; i++)
            {
                switch (Effect[i])
                {
                    case EffectType.Baned:
                    case EffectType.AllResistance:
                        if (EffectTurns[i] > 0)
                        {
                            EffectTurns[i]--;
                            if (EffectTurns[i] == 0)
                            {
                                Effect.RemoveAt(i);
                                EffectTurns.RemoveAt(i);
                            }
                        } 
                        break;
                    case EffectType.Barkskin:
                        if (EffectTurns[i] > 0)
                        {
                            EffectTurns[i]--;
                            if (EffectTurns[i] == 0)
                            {
                                Effect.RemoveAt(i);
                                EffectTurns.RemoveAt(i);
                                Armor = ArmorCopy;
                            }
                        }
                        break;
                    case EffectType.FaithShield:
                        if (this is Player)
                        {
                            if (HealthCopy < Health)
                            {
                                Effect.RemoveAt(i);
                                EffectTurns.RemoveAt(i);
                            }
                        }
                        break;
                }
            }
        }
    }
}