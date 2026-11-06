using PlayerFile;
using AllyFile;
using EnemyFile;

namespace ObjectFile
{
    public class Objects
    {
        // Player and Allies
        public Player player = new(
            levelXp: 1,
            name: "Jogador",
            health: 10,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 10,
            initiativeBonus: 0,
            attackBonus: 0,
            xp: 10
        );

        public Cleric cleric = new(
            levelXp: 1,
            name: "Clérigo",
            health: 8,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 13,
            initiativeBonus: 1,
            attackBonus: 13,
            xp: 0
        );

        public Archer archer = new(
            levelXp: 1,
            name: "Arqueiro",
            health: 8,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 12,
            initiativeBonus: 2,
            attackBonus: 12,
            xp: 0
        );

        public Wizard wizard = new(
            levelXp: 1,
            name: "Mago",
            health: 6,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 10,
            initiativeBonus: 0,
            attackBonus: 10,
            xp: 0
        );

        // Enemies
        public Plebeian plebeian = new(
            name: "Plebeu",
            health: 4,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 10,
            initiativeBonus: 1,
            attackBonus: 2,
            xp: 10
        );

        public TribalWarrior tribalWarrior = new(
            name: "Guerreiro Tribal",
            health: 11,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 12,
            initiativeBonus: 0,
            attackBonus: 3,
            xp: 25
        );

        public BanditCaptain banditCaptain = new(
            name: "Capitão dos Bandidos",
            health: 65,
            resistanceBonus: 0,
            damageBonus: 2,
            armor: 15,
            initiativeBonus: 3,
            attackBonus: 3,
            xp: 450
        );

        // Animals
        public BigRat rat = new(
            name: "Rato",
            health: 7,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 13,
            initiativeBonus: 3,
            attackBonus: 5,
            xp: 25
        );

        public Boar boar = new(
            name: "Javali",
            health: 13,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 11,
            initiativeBonus: 0,
            attackBonus: 3,
            xp: 50
        );

        public Deer deer = new(
            name: "Cervo",
            health: 11,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 10,
            initiativeBonus: 0,
            attackBonus: 5,
            xp: 50
        );

        public ItalianWolf wolfOne = new(
            name: "Lobo-Itálico um",
            health: 22,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 14,
            initiativeBonus: 2,
            attackBonus: 5,
            xp: 200
        );

        public ItalianWolf wolfTwo = new(
            name: "Lobo-Itálico dois",
            health: 22,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 14,
            initiativeBonus: 2,
            attackBonus: 5,
            xp: 200
        );

        public BrownBear bear = new(
            name: "Urso-Pardo-Misciano",
            health: 22,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 11,
            initiativeBonus: 1,
            attackBonus: 5,
            xp: 200
        );

        public BigBoar bigBoar = new(
            name: "Javali Grande",
            health: 42,
            resistanceBonus: 0,
            damageBonus: 3,
            armor: 13,
            initiativeBonus: 0,
            attackBonus: 5,
            xp: 450
        );

        public BigDeer bigDeer = new(
            name: "Grande Cervo",
            health: 42,
            resistanceBonus: 0,
            damageBonus: 4,
            armor: 14,
            initiativeBonus: 6,
            attackBonus: 6,
            xp: 450
        );

        public Hippopotamus hippopotamus = new(
            name: "Hipopótamos",
            health: 82,
            resistanceBonus: 0,
            damageBonus: 5,
            armor: 14,
            initiativeBonus: -2,
            attackBonus: 0,
            xp: 1100
        );

        public Unicorn unicorn = new(
            name: "Unicórnio",
            health: 97,
            resistanceBonus: 0,
            damageBonus: 0,
            armor: 10,
            initiativeBonus: 0,
            attackBonus: 5,
            xp: 10
        );
    }
}

