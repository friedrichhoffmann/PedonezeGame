using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Formats.Tar;
using EnumFile;
using PlayerFile;
using AllyFile;
using Godot;

namespace CharacterFile
{
    public abstract class Character2D
    {
        public string Name;
        public int Health;
        public int DropXp;
        
        public string NamePropertie
        {
            get => Name;
            set => Name = value;
        }

        public int HealthProperty
        {
            get => Health;
            set => Health = Math.Clamp(value, 0, 100);
        }

        public int XpProperty
        {
            get => DropXp;
            set => DropXp = Math.Clamp(value, 1, 100);
        }
        public abstract void CharacterDie(Combatant target);
    }

    public abstract partial class Combatant : Character2D
    {
        
        public Random rng = new();

        protected Combatant(string name, int health, DamageType damage, DamageResistance resistance, 
            int resistanceBonus, int damageBonus, int armor, int iniciativeBonus, int attackBonus, int XP, bool state)
        {
            State = new();
            Effect = new();
            Weapons = new();
            Name = name;
            Health = health;
            DamageType = damage;
            ResistanceDamageType = resistance;
            ResistanceBonus = resistanceBonus;
            DamageBonus = damageBonus;
            Armor = armor;
            IniciativeBonus = iniciativeBonus;
            AttackBonus = attackBonus;
            DropXp = XP;
            Good = state;
        }
    }

    public abstract partial class Combatant : Character2D // Variáveis
    {
        public event Action? EnemyAttacked;
        
        public int CharacterLevel;
        public List<StateType> State { get; set; }
        public List<EffectType> Effect { get; set; }
        public List<ActionType> Weapons { get; set; }
        public List<int> StateTurns { get; set; }
        public List<int> EffectTurns { get; set; }
        public ActionType CharacterActionType;
        public DamageType DamageType;
        public DamageResistance ResistanceDamageType;
        public bool Good;
        public int Armor;
        public int IniciativeBonus;
        public int AttackBonus;
        public int DamageBonus;
        public int ResistanceBonus;

        public int HealthCopy;
        public int ArmorCopy;
        
        public static List<Combatant> Hero = new();
        public static List<Combatant> Enemy = new();
        public static List<Combatant> All = new();
    }

    public abstract partial class Combatant : Character2D // Métodos
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
        
        public void HeroTurn() // Turno aliado
        {
            if (this is MainCharacter a)
            {
                a.LevelStats();
            }
            if (Health <= 0)
            {
                GD.Print($"{Name} não pode tomar ação. Está morto.");
                return;
            }
            
            if (State.Contains(StateType.Paralysed))
            {
                GD.Print("Não pode tomar ação. Está paralisado.");
                return;
            }

            if (Effect.Contains(EffectType.Baned))
            {
                GD.Print("Não pode tomar ação. Está sob efeito.");
                return;
            }
            
            GD.Print($"\nTurno do {Name}");
            GD.Print($"Quais ações você quer tomar com seu {Name}?");
            if (Effect.Contains(EffectType.Barkskin))
            {
                ArmorCopy = Armor;
                Armor = 17;
            }
            
            GD.Print("1 - Ação física.");
            GD.Print("2 - Ação mágica.");

            int input = 0;
            
            if (this is Player)
            {
                input = int.Parse(Console.ReadLine());
                if (input == 2)
                {
                    Weapons.ForEach(Weapons => GD.Print(Weapons));
                }
            }
            
            else if (this is Cleric)
            {
                input = int.Parse(Console.ReadLine());
                if (input == 2)
                {
                    Weapons.ForEach(Weapons => GD.Print(Weapons));
                }
            }
            
            else if (this is Archer)
            {
                input = int.Parse(Console.ReadLine());
                if (input == 2)
                {
                    Weapons.ForEach(Weapons => GD.Print(Weapons));
                }
            }
            
            else if (this is Wizard)
            {
                input = int.Parse(Console.ReadLine());
                if (input == 2)
                {
                    Weapons.ForEach(Weapons => GD.Print(Weapons));
                }
            }
            
            if (Effect.Contains(EffectType.Disadvantage))
            {
                int i = Effect.IndexOf(EffectType.Disadvantage);
                State.RemoveAt(i);
                StateTurns.RemoveAt(i);
            }
        }
        
        public void EnemyTurn() // Turno inimigo
        {
            Weapons.Clear();
            ChooseWeapon();
            
            GD.Print($"Vez do {Name}");
            
            for (int a = 0; a < Weapons.Count; a++)
            {
                if (Weapons[a] >= ActionType.MageArmor && Weapons[a] <= ActionType.FlameArrows)
                {
                    GD.Print("Magia auto-beneficiadora.");
                    CalculateMagicHeal(Enemy, this);
                    GiveMagicEffect(Enemy, this);
                }
                
                else if (Weapons[a] >= ActionType.DeathWard && Weapons[a] <= ActionType.LesserRestoration)
                {
                    GD.Print("Magia beneficiadora para aliado.");
                    static int OrganizeList(Combatant one, Combatant two)
                    {
                        return one.Health.CompareTo(two.Health);
                    }
                    Enemy.Sort(OrganizeList);
                    CalculateMagicHeal(Enemy, Enemy[0]);
                    GiveMagicEffect(Enemy, Enemy[0]);
                }
                
                else if (Weapons[a] >= ActionType.Aid && Weapons[a] <= ActionType.BeaconOfHope)
                {
                    GD.Print("Magia beneficiadora para todo o grupo.");
                    CalculateMagicHeal(Enemy, this);
                    GiveMagicEffect(Enemy, this);
                }
                
                else
                {
                    int i = rng.Next(0, Hero.Count);
                    if (Hero[i].Health > 0)
                    {
                        if (Hero[i].Effect.Contains(EffectType.FireShield))
                        {
                            GD.Print($"{Hero[i].Name} possui Escudo de Fogo.");
                            Health -= RollMultipleDice(2, 1, 9);
                            return;
                        }
                        GD.Print($"\n{Name} escolheu {Hero[i].Name}!");
                        int rollAttack = RollDice(1, 21) + AttackBonus;
                        GD.Print($"{Name} tirou {rollAttack} de teste de ataque.");
                        if (rollAttack >= Hero[i].Armor)
                        {
                            EnemyAttack(a, i);
                        }
                            
                        else
                        {
                            GD.Print($"\n{Name} errou o golpe contra {Hero[i].Name}!\n");
                        } 
                    }
                }
            }
        }

        public void EnemyAttack(int a, int i)
        {
            CharacterActionType = Weapons[a];
            EnemyAttacked?.Invoke();
            int damage = CharacterGiveDamage(0, Hero[i]);
            damage = CharacterCalculateResistance(damage, Hero[i], this);
            Hero[i].Health -= damage + DamageBonus;

            Hero[i].Stats();
        }

        public void RemoveCHaracter()
        {
            if (Health == 0) 
            {
                if (Hero.Contains(this))
                {
                    Hero.Remove(this);
                }
                else if (Enemy.Contains(this))
                {
                    Enemy.Remove(this);
                }
            }
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

    public abstract partial class Combatant : Character2D // Propriedades
    {
        public int LevelProperty
        {
            get => CharacterLevel;
            set => CharacterLevel = Math.Clamp(value, 1, 11);
        }
        
        public int ArmorProperty
        {
            get => Armor;
            set => Armor = Math.Clamp(value, 0, 20);
        }

        public int InitiativeProperty
        {
            get => IniciativeBonus;
            set => IniciativeBonus = Math.Clamp(value, 0, 20);
        }

        public int AttackProperty
        {
            get => AttackBonus;
            set => AttackBonus = Math.Clamp(value, 0, 20);
        }

        public int DamageProperty
        {
            get => DamageBonus;
            set => DamageBonus = Math.Clamp(value, 0, 20);
        }

        public int ResistanceProperty
        {
            get => ResistanceBonus;
            set => ResistanceBonus = Math.Clamp(value, 0, 20);
        }
    }
    
    public abstract class Npc : Character2D
    {
        public abstract void CharacterQuote();
        public abstract void CharacterRoutine();

        protected Npc(int health, int dropXp)
        {
            Health = health;
            DropXp = dropXp;
        }
    }

    public abstract class MainCharacter : Combatant
    {   
        
        public static int Level;
        public int SpellSlot;

        public int LevelPropertie
        {
            get => CharacterLevel;
            set => CharacterLevel = Math.Clamp(value, 1, 11);
        }

        public int SpellSlotPropertie
        {
            get => SpellSlot;
            set => SpellSlot = Math.Clamp(value, 0, 30);
        }

        public abstract void LevelStats();

        public MainCharacter(int levelXp, string name, int health, DamageType damage, DamageResistance resistance, int resistanceBonus, int damageBonus, int armor, int initiativeBonus, int attackBonus, int xp, bool state)
            : base(name, health, damage, resistance, resistanceBonus, damageBonus, armor, initiativeBonus, attackBonus, xp, state)
        {
            Level = levelXp;
        }
    }
}