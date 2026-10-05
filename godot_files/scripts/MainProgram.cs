using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using CharacterFile;
using AllyFile;
using EnemyFile;
using AnimalFile;

namespace Game_Development
{
    public class MainProgram
    {
        internal static void Main(string[] args)
        {
            List<Combatant> hero = new List<Combatant>();
            List<Combatant> enemy = new List<Combatant>();
            List<Combatant> allCombatants = new List<Combatant>();
            bool HeroWin = false;
            bool EnemyWin = false;
            
            hero.Add(new Player(1, "Jogador", 10, DamageType.None,  DamageResistance.None, 0, 0, 0, 0, 0, 10, true));
            hero.Add(new Cleric(1, "Clérigo",8, DamageType.None, DamageResistance.None, 0, 0, 13, 1, 13, 0, true));
            hero.Add(new Archer(1, "Arqueiro", 8, DamageType.None, DamageResistance.None,0, 0, 12, 2, 12, 0, true));
            hero.Add(new Wizard(1, "Mago", 6, DamageType.None, DamageResistance.None,0 , 0, 10, 0, 10, 0, true));
            
            enemy.Add(new Bandit("Bandido 1",11, DamageType.None, DamageResistance.None,0, 3, 12, 1, 1, 25, false));
            enemy.Add(new Bandit("Bandido 2", 11, DamageType.None, DamageResistance.None,0, 3, 12, 1, 1, 25, false));
            
            allCombatants.AddRange(hero);
            allCombatants.AddRange(enemy);

            

            Console.WriteLine("Seus aliados são: \n");
            foreach (Combatant element in hero)
            {
                Console.WriteLine($"{element.CharacterName}.");
            }
            Console.WriteLine("\nSeus inimigos são: \n");
            foreach (Combatant element in enemy)
            {
                Console.WriteLine($"{element.CharacterName}.");
            }
            
            Console.WriteLine("Realize um teste de iniciativa: \n");
            Console.ReadKey();

            foreach (Combatant element in allCombatants)
            {
                element.CharacterIniciativeBonus += element.RollD20();
                Console.WriteLine($"{element.CharacterName} tirou {element.CharacterIniciativeBonus}.");
            }

            static int OrganizeList(Combatant one, Combatant two)
            {
                return two.CharacterIniciativeBonus.CompareTo(one.CharacterIniciativeBonus);
            }
            allCombatants.Sort(OrganizeList);
            
            Console.WriteLine("\nA ordem será: \n");
            Console.ReadKey();
            
            foreach (Combatant element in allCombatants)
            {
                Console.WriteLine($"{element.CharacterName}.");
            }
            
            Console.ReadKey();

            Console.WriteLine($"\nComeçando:\n");
            Thread.Sleep(1000);
            
            foreach (Combatant element in allCombatants)
            {
                element.Stats();
                Thread.Sleep(1000);
            }
            
            while (HeroWin == false && EnemyWin == false) // Algoritmo dos turnos
            {
                foreach (Combatant element in allCombatants)
                {
                    if (element.CharacterGood && element.CharacterHealth != 0)
                    {
                        Console.WriteLine($"\nTurno do {element.CharacterName}");
                        Console.WriteLine($"Quais ações você quer tomar com seu {element.CharacterName}?");
                        if (element is Player)
                        {
                            Console.ReadKey();
                        }
                        else if (element is Cleric)
                        {
                            Console.ReadKey();
                        }
                        else if (element is Archer)
                        {
                            Console.ReadKey();
                        }
                        else if (element is Wizard)
                        {
                            Console.ReadKey();
                        }
                    }
                    
                    else if (element.CharacterHealth != 0)
                    {
                        element.ChooseWeapon();
                        switch (element)
                        {
                            case Bandit:
                            case TribalWarrior:
                            case Plebeian:
                            case Deer:
                            case BigRat:
                            case Boar:
                            case ItalianWolf:
                            case BigDeer:
                            case BigBoar:
                                if (element.CharacterWeapons.Count > 1)
                                {
                                    element.CharacterWeapons.RemoveAt(element.CharacterWeapons.Count - 1);
                                }

                                break;
                            default:
                                element.CharacterWeapons.Clear();
                                break;
                        }
                        
                        Console.WriteLine($"{element.CharacterName} possui {element.CharacterWeaponType}");
                        
                        int enemyChoice = 0;
                        while (true)
                        {
                            enemyChoice = element.rng.Next(0, allCombatants.Count);
                            if (allCombatants[enemyChoice].CharacterGood && allCombatants[enemyChoice].CharacterHealth > 0)
                            {
                                    Console.WriteLine($"\n{element.CharacterName} escolheu {allCombatants[enemyChoice].CharacterName}!");
                                    Thread.Sleep(1000);
                                    int tryAttack = element.RollD20() + element.CharacterAttackBonus;
                                    if (tryAttack >= allCombatants[enemyChoice].CharacterArmor)
                                    {
                                        for (int i = 0; i < element.CharacterWeapons.Count; i++)
                                        {
                                            Console.WriteLine($"{allCombatants[enemyChoice].CharacterName} foi atacado!");
                                            int damage = element.CharacterGiveDamage(0, allCombatants[enemyChoice], element);
                                            Console.WriteLine($"{allCombatants[enemyChoice].CharacterName} perdeu {damage} de vida!");
                                        }

                                        allCombatants[enemyChoice].Stats();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\n{element.CharacterName} errou o golpe contra {allCombatants[enemyChoice].CharacterName}!\n");
                                        Thread.Sleep(1000);
                                    }

                                    break;
                            }
                        }
                    }
                }
                
                foreach (Combatant element in allCombatants)
                {
                    if (element.CharacterHealth != 0)
                    {
                        element.Stats();
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Console.WriteLine($"{element.CharacterName} está morto.");
                    }
                }
                
                for (int i = 0; i < allCombatants.Count - 1; i--)
                {
                    if (allCombatants[i].CharacterHealth == 0) 
                    {
                        if (allCombatants[i].CharacterGood)
                        {
                            hero.Remove(allCombatants[i]);
                        }
                        else if (allCombatants[i].CharacterGood == false)
                        {
                            enemy.Remove(allCombatants[i]);
                        }
                        allCombatants.Remove(allCombatants[i]);
                    }
                }
                
                if (hero.Count == 0)
                {
                    Console.WriteLine("Inimigos venceram!");
                    HeroWin = true;
                } 
                else if (enemy.Count == 0)
                {
                    Console.WriteLine("Heróis venceram!");
                    EnemyWin = true;
                }
            } 
        }
    }   
}
/* public enum CharacterState
{
    None,
    Incapacitated,
    Poisoned,
    Paralysed,
    Feared
}
public enum MagicSkill
{
    GuiedDance,
    LightBall,
    DivineHelp,
    ControlledBlaze,
    FaithShield,
    HoldCharacter
}
*/