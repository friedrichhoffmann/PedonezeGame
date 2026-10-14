using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;
using System.Data;
using System.Security.Cryptography;
using CharacterFile;
using EnumFile;
using PlayerFile;
using AllyFile;
using EnemyFile;
using AnimalFile;

namespace ObjectFile
{
    public class Objects
    {
        public Player player = new(1, "Jogador", 10, DamageType.None,  DamageResistance.None, 0, 0, 10, 0, 0, 10, true); 
        public Cleric cleric = new(1, "Clérigo",8, DamageType.None, DamageResistance.None, 0, 0, 13, 1, 13, 0, true);
        public Archer archer = new(1, "Arqueiro", 8, DamageType.None, DamageResistance.None, 0, 0, 12, 2, 12, 0, true);
        public Wizard wizard = new(1, "Mago", 6, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 10, 0, true);

        
        public Plebeian plebeian = new("Plebeu", 4, DamageType.None, DamageResistance.None, 0, 0, 10, 1, 2, 10, false);
        public TribalWarrior tribalWarrior = new("Guerreiro Tribal", 11, DamageType.None, DamageResistance.None, 0, 0, 12, 0, 3, 25, false);
        public BanditCaptain banditCaptain = new("Capitão dos Bandidos", 65, DamageType.None, DamageResistance.None, 0, 2, 15, 3, 3, 450, false);    
            
            
        public BigRat rat = new("Rato",  7, DamageType.None, DamageResistance.None, 0, 0, 13, 3, 5, 25, false);
        public Boar boar = new("Javali", 13,  DamageType.None, DamageResistance.None, 0, 0, 11, 0, 3, 50, false);
        public Deer deer = new("Cervo", 11, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 5, 50, false);
        public ItalianWolf wolfOne = new("Lobo-Itálico um", 22,  DamageType.None, DamageResistance.None, 0, 0, 14, 2, 5, 200, false);
        public ItalianWolf wolfTwo = new("Lobo-Itálico dois", 22,  DamageType.None, DamageResistance.None, 0, 0, 14, 2, 5, 200, false);
        public BrownBear bear = new("Urso-Pardo-Misciano", 22, DamageType.None, DamageResistance.None, 0, 0, 11, 1, 5, 200, false);
        public BigBoar bigBoar = new("Javali Grande", 42, DamageType.None, DamageResistance.None, 0, 3, 13, 0, 5, 450, false);
        public BigDeer bidDeer = new("Grande Cervo", 42, DamageType.None, DamageResistance.None, 0, 4, 14, 6, 6, 450, false);
        public Hippopotamus hippopotamus = new("Hipopótamos", 82, DamageType.None, DamageResistance.None, 0, 5, 14, -2, 0, 1100, false);
        public Unicorn unicorn = new("Unicórnio", 97, DamageType.None, DamageResistance.None, 0, 0, 10, 0, 5, 10, false);
    }
}