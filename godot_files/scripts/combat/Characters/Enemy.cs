using PreMadeAttacks;
using GenericEnemy;

namespace EnemyFile
{
    public class Bandit : Enemy
    {
        public static Bandit banditOne = new("Bandido 1", 11, 0, 1, 12, 1, 3, 25);
        public static Bandit banditTwo = new("Bandido 2", 11, 0, 1, 12, 1, 3, 25);

        public Bandit(
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.BanditScimitar(),
                () => AttackLibrary.BanditCrossbow()
            };
        }
    }

    public class Plebeian : Enemy
    {
        public static Plebeian plebeian = new("Plebeu", 4, 0, 0, 10, 1, 2, 10);

        public Plebeian(
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.PlebeianStick()
            };
        }
    }

    public class TribalWarrior : Enemy
    {
        public static TribalWarrior tribalWarrior = new("Guerreiro Tribal", 11, 0, 0, 12, 0, 3, 25);

        public TribalWarrior(
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.TribalWarriorSpear()
            };
        }
    }

    public class BanditCaptain : Enemy
    {
        public static BanditCaptain banditCaptain = new("Capitão dos Bandidos", 65, 0, 2, 15, 3, 3, 450);

        public BanditCaptain(
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.BanditCaptainScimitar(),
                () => AttackLibrary.BanditCaptainDagger()
            };
        }
    }

    public class BigRat : Enemy
    {
        public BigRat(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.RatBite()
            };
        }
    }

    public class Boar : Enemy
    {
        public Boar(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.BoarCharge()
            };
        }
    }

    public class Deer : Enemy
    {
        public Deer(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.DeerHorn()
            };
        }
    }

    public class ItalianWolf : Enemy
    {
        public ItalianWolf(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.ItalianWolfBite()
            };
        }
    }

    public class BrownBear : Enemy
    {
        public BrownBear(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.BrownBearBite(),
                () => AttackLibrary.BrownBearCrawl()
            };
        }
    }

    public class BigBoar : Enemy
    {
        public BigBoar(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.BigBoarCharge()
            };
        }
    }

    public class BigDeer : Enemy
    {
        public BigDeer(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.BigDeerHorn()
            };
        }
    }

    public class Hippopotamus : Enemy
    {
        public Hippopotamus(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.HippopotamusBite()
            };
        }
    }

    public class Unicorn : Enemy
    {
        public Unicorn(
                string name, int health, int resistanceBonus, int damageBonus,
                int armor, int initiativeBonus, int attackBonus, int xp)
            : base(name, health, resistanceBonus, damageBonus, armor,
                    initiativeBonus, attackBonus, xp)
        {
            ActiveEffects = new();
            Resistances = new();
            StateTurns = new();
        }

        public void SetupAttacks()
        {
            AttackSet = new()
            {
                () => AttackLibrary.UnicornKick(),
                () => AttackLibrary.UnicornHorn()
            };
        }
    }

}

