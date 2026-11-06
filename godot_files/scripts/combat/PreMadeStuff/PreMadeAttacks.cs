using Dice;
using MagicSystem;
using PhyAttack;

namespace PreMadeAttacks {
    public static class AttackLibrary {

        const ActRange FOCUSED = ActRange.Focused;
        const ActRange BROAD = ActRange.Broad;

        const DamageKind SLASHING = DamageKind.Slashing;
        const DamageKind PIERCING = DamageKind.Piercing;
        const DamageKind BLUDGEONING = DamageKind.Bludgeoning;

        public static Attack BanditScimitar()        => new Attack { Damage = Die.Roll(2, Die.d4),                       Kind = SLASHING,    Range = FOCUSED, };
        public static Attack BanditCrossbow()        => new Attack { Damage = Die.Roll(1, Die.d4) + Die.Roll(1, Die.d6), Kind = PIERCING,    Range = FOCUSED, };
        public static Attack TribalWarriorSpear()    => new Attack { Damage = Die.Roll(2, Die.d4),                       Kind = PIERCING,    Range = FOCUSED, };
        public static Attack PlebeianStick()         => new Attack { Damage = Die.Roll(1, Die.d4),                       Kind = BLUDGEONING, Range = FOCUSED, };
        public static Attack RatBite()               => new Attack { Damage = Die.Roll(4, Die.d2),                       Kind = PIERCING,    Range = FOCUSED, };
        public static Attack BoarCharge()            => new Attack { Damage = Die.Roll(2, Die.d4),                       Kind = BLUDGEONING, Range = FOCUSED, };
        public static Attack DeerHorn()              => new Attack { Damage = Die.Roll(3, Die.d2) + Die.Roll(1, Die.d4), Kind = PIERCING,    Range = FOCUSED, };
        public static Attack ItalianWolfBite()       => new Attack { Damage = Die.Roll(4, Die.d4),                       Kind = PIERCING,    Range = FOCUSED, };
        public static Attack BrownBearBite()         => new Attack { Damage = Die.Roll(3, Die.d4) + Die.Roll(1, Die.d2), Kind = PIERCING,    Range = FOCUSED, };
        public static Attack BrownBearCrawl()        => new Attack { Damage = Die.Roll(4, Die.d2),                       Kind = SLASHING,    Range = FOCUSED, };
        public static Attack BanditCaptainScimitar() => new Attack { Damage = Die.Roll(1, Die.d8),                       Kind = SLASHING,    Range = FOCUSED, };
        public static Attack BanditCaptainDagger()   => new Attack { Damage = Die.Roll(1, Die.d6),                       Kind = SLASHING,    Range = FOCUSED, };
        public static Attack CentaurSpear()          => new Attack { Damage = Die.Roll(1, Die.d8),                       Kind = PIERCING,    Range = FOCUSED, };
        public static Attack CentaurLongBow()        => new Attack { Damage = Die.Roll(1, Die.d8),                       Kind = PIERCING,    Range = FOCUSED, };
        public static Attack BigBoarCharge()         => new Attack { Damage = Die.Roll(2, Die.d6),                       Kind = BLUDGEONING, Range = FOCUSED, };
        public static Attack BigDeerHorn()           => new Attack { Damage = Die.Roll(2, Die.d6),                       Kind = PIERCING,    Range = FOCUSED, };
        public static Attack HippopotamusBite()      => new Attack { Damage = Die.Roll(2, Die.d12),                      Kind = PIERCING,    Range = FOCUSED, };
        public static Attack UnicornKick()           => new Attack { Damage = Die.Roll(2, Die.d6),                       Kind = BLUDGEONING, Range = FOCUSED, };
        public static Attack UnicornHorn()           => new Attack { Damage = Die.Roll(1, Die.d12),                      Kind = PIERCING,    Range = FOCUSED, };
    }
}

