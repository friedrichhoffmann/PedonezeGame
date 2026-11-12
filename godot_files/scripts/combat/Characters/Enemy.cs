using GenericEnemy;
using static PreMadeAttacks.AttackLibrary;

namespace EnemyFile
{
    public class Bandit(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(BanditScimitar), BanditScimitar),
                (nameof(BanditCrossbow), BanditCrossbow)
            };
        }
    }

    public class Plebeian(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(PlebeianStick), PlebeianStick)
            };
        }
    }

    public class TribalWarrior(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(TribalWarriorSpear), TribalWarriorSpear)
            };
        }
    }

    public class BanditCaptain(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(BanditCaptainScimitar), BanditCaptainScimitar),
                (nameof(BanditCaptainDagger), BanditCaptainDagger)
            };
        }
    }

    public class BigRat(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(RatBite), RatBite)
            };
        }
    }

    public class Boar(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(BoarCharge), BoarCharge)
            };
        }
    }

    public class Deer(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(DeerHorn), DeerHorn)
            };
        }
    }

    public class ItalianWolf(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(ItalianWolfBite), ItalianWolfBite)
            };
        }
    }

    public class BrownBear(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(BrownBearBite), BrownBearBite),
                (nameof(BrownBearCrawl), BrownBearCrawl)
            };
        }
    }

    public class BigBoar(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(BigBoarCharge), BigBoarCharge)
            };
        }
    }

    public class BigDeer(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(BigDeerHorn), BigDeerHorn)
            };
        }
    }

    public class Hippopotamus(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(HippopotamusBite), HippopotamusBite)
            };
        }
    }

    public class Unicorn(
        string name,
        int health,
        int armor,
        int initiativeBonus,
        int xp
    ) : Enemy(name, health, armor, initiativeBonus, xp) {
        public void SetAttacksUp() {
            AttackSet = new() {
                (nameof(UnicornKick), UnicornKick),
                (nameof(UnicornHorn), UnicornHorn)
            };
        }
    }
}

