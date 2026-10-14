using Godot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using CharacterFile;
using EnumFile;
using PlayerFile;
using AllyFile;
using EnemyFile;
using AnimalFile;

namespace Game_Development
{
    public class MainProgram
    {
        internal static void Main(string[] args)
        {
            
            Combatant.Hero.Add(Player.player);
            
            Combatant.Enemy.Add(Bandit.banditOne);
            Combatant.Enemy.Add(Bandit.banditTwo);
            
            Combatant.All.AddRange(Combatant.Hero);
            Combatant.All.AddRange(Combatant.Enemy);

            GD.Print("Seus aliados são: \n");
            foreach (Combatant element in Combatant.Hero)
            {
                GD.Print($"{element.Name}.");
            }
            GD.Print("\nSeus inimigos são: \n");
            foreach (Combatant element in Combatant.Enemy)
            {
                GD.Print($"{element.Name}.");
            }
            
            GD.Print("Realize um teste de iniciativa: \n");
            Console.ReadKey();

            foreach (Combatant element in Combatant.All)
            {
                element.IniciativeBonus += element.RollDice(1, 21);
                GD.Print($"{element.Name} tirou {element.IniciativeBonus}.");
            }

            static int OrganizeList(Combatant one, Combatant two)
            {
                return two.IniciativeBonus.CompareTo(one.IniciativeBonus);
            }
            Combatant.All.Sort(OrganizeList);
            
            GD.Print("\nA ordem será: \n");
            Console.ReadKey();
            
            foreach (Combatant element in Combatant.All)
            {
                GD.Print($"{element.Name}.");
            }
            
            Console.ReadKey();

            GD.Print($"\nComeçando:\n");
            Console.ReadKey();
            
            foreach (Combatant element in Combatant.All)
            {
                element.Stats();
                Thread.Sleep(1000);
            }
            
            while (true) // Algoritmo dos turnos
            {
                Player.player.HealthCopy = Player.player.Health;
                foreach (Combatant element in Combatant.All)
                {
                    if (Combatant.Hero.Contains(element))
                    {
                        element.HeroTurn();
                    }
                    
                    else if (Combatant.Enemy.Contains(element) && element.Health > 0 && !element.State.Contains(StateType.Paralysed))
                    {
                        element.EnemyTurn();
                    }
                    
                    element.DealState();
                    element.DealEffect();
                }
                
                
                
                foreach (Combatant element in Combatant.All)
                {
                    element.RemoveCHaracter();
                    element.Stats();
                }

                if (Combatant.Hero.Count == 0)
                {
                    GD.Print("Inimigos venceram!");
                    break;
                } 
                if (Combatant.Enemy.Count == 0)
                {
                    GD.Print("Heróis venceram!");
                    break;
                }
            }
        }
    }   
}

/* 
            public static Boar boar = new("Javali", 13,  DamageType.None, DamageResistance.None, 0, 0, 11, 0, 3, 50, false);
            public static Deer deer = new("Cervo", 11, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 5, 50, false);
            public static ItalianWolf wolfOne = new("Lobo-Itálico um", 22,  DamageType.None, DamageResistance.None, 0, 0, 14, 2, 5, 200, false);
            public static ItalianWolf wolfTwo = new("Lobo-Itálico dois", 22,  DamageType.None, DamageResistance.None, 0, 0, 14, 2, 5, 200, false);
            public static BrownBear bear = new("Urso-Pardo-Misciano", 22, DamageType.None, DamageResistance.None, 0, 0, 11, 1, 5, 200, false);
            public static BigBoar bigBoar = new("Javali Grande", 42, DamageType.None, DamageResistance.None, 0, 3, 13, 0, 5, 450, false);
            public static BigDeer bidDeer = new("Grande Cervo", 42, DamageType.None, DamageResistance.None, 0, 4, 14, 6, 6, 450, false);
            public static Hippopotamus hippopotamus = new("Hipopótamos", 82, DamageType.None, DamageResistance.None, 0, 5, 14, -2, 0, 1100, false);
            public static Unicorn unicorn = new("Unicórnio", 97, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 5, 10, false);
            */