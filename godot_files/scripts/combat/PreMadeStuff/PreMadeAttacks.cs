
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

		public static Attack BanditScimitar()        => new Attack(Die.Roll(2, Die.d4),                       SLASHING,    FOCUSED);
		public static Attack BanditCrossbow()        => new Attack(Die.Roll(1, Die.d4) + Die.Roll(1, Die.d6), PIERCING,    FOCUSED);
		public static Attack TribalWarriorSpear()    => new Attack(Die.Roll(2, Die.d4),                       PIERCING,    FOCUSED);
		public static Attack PlebeianStick()         => new Attack(Die.Roll(1, Die.d4),                       BLUDGEONING, FOCUSED);
		public static Attack RatBite()               => new Attack(Die.Roll(4, Die.d2),                       PIERCING,    FOCUSED);
		public static Attack BoarCharge()            => new Attack(Die.Roll(2, Die.d4),                       BLUDGEONING, FOCUSED);
		public static Attack DeerHorn()              => new Attack(Die.Roll(3, Die.d2) + Die.Roll(1, Die.d4), PIERCING,    FOCUSED);
		public static Attack ItalianWolfBite()       => new Attack(Die.Roll(4, Die.d4),                       PIERCING,    FOCUSED);
		public static Attack BrownBearBite()         => new Attack(Die.Roll(3, Die.d4) + Die.Roll(1, Die.d2), PIERCING,    FOCUSED);
		public static Attack BrownBearCrawl()        => new Attack(Die.Roll(4, Die.d2),                       SLASHING,    FOCUSED);
		public static Attack BanditCaptainScimitar() => new Attack(Die.Roll(1, Die.d8),                       SLASHING,    FOCUSED);
		public static Attack BanditCaptainDagger()   => new Attack(Die.Roll(1, Die.d6),                       SLASHING,    FOCUSED);
		public static Attack CentaurSpear()          => new Attack(Die.Roll(1, Die.d8),                       PIERCING,    FOCUSED);
		public static Attack CentaurLongBow()        => new Attack(Die.Roll(1, Die.d8),                       PIERCING,    FOCUSED);
		public static Attack BigBoarCharge()         => new Attack(Die.Roll(2, Die.d6),                       BLUDGEONING, FOCUSED);
		public static Attack BigDeerHorn()           => new Attack(Die.Roll(2, Die.d6),                       PIERCING,    FOCUSED);
		public static Attack HippopotamusBite()      => new Attack(Die.Roll(2, Die.d12),                      PIERCING,    FOCUSED);
		public static Attack UnicornKick()           => new Attack(Die.Roll(2, Die.d6),                       BLUDGEONING, FOCUSED);
		public static Attack UnicornHorn()           => new Attack(Die.Roll(1, Die.d12),                      PIERCING,    FOCUSED);
	}
}
