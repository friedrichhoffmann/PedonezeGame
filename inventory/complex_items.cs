using SimpleItems;

namespace ComplexItems {
	
	abstract class Blade : Weapon {
		private float sharpness;

		public enum BladeWeight {
			Heavy,
			Light
		}

		public BladeWeight Weight { get; set; }
		public float Sharpness {
			get { return sharpness; }
			set { sharpness = Math.Clamp(value, 0, 1); }
		}
	}
	

	class Sword : Blade {
		public SwordKind Kind { get; set; } 
		public enum SwordKind {
			Gladius,
			Rapier,
			Saber,
			BatardSword,
			LongSword,
			Zweihander,
		}

		public Sword(String name, SwordKind kind, int m_dur, int dam, float sharpness) {
			Name = name;
			Id = 20 + (int)kind;
			MaxDur = m_dur;
			CurDur = m_dur;
			Stackable = false;
			Quantity = 1;

			Damage = dam;
			Sharpness = sharpness;

			Kind = kind;

			switch (kind) {
				case SwordKind.Zweihander: {
					Weight = BladeWeight.Heavy;
					break;
				}
				case SwordKind.LongSword: {
					Weight = BladeWeight.Heavy;
					break;
				}
				default: {
					Weight = BladeWeight.Light;
					break;
				}
			}
		}
	}

	class StaticItem : Item {
		public StaticItem(String name, int id, int m_dur) {
			Name = name;
			Id = id;
			MaxDur = m_dur;
			CurDur = m_dur;
			Stackable = true;
			Quantity = 1;
		}
	}

	class Dagger : Blade {
		public DaggerKind Kind { get; set; } 
		public DefenseDagger Def { get; set; } // some daggers, like the one used in fencing are used to block attack rather as just offensive,
						// so these would have a defense dagger attached to it <not physically but in this code>
		public enum DaggerKind {
			Mercy,
			Ballock,
			Stiletto,
			Rondel,
			MainGauche,
			SwordBreaker,
		}

		public Dagger(String name, int m_dur, int dam, float sharpness, DaggerKind kind, int ac) {
			Name = name;
			Id = 30 + (int)kind;
			MaxDur = m_dur;
			CurDur = m_dur;
			Stackable = false;
			Quantity = 1;

			Damage = dam;
			Sharpness = sharpness;

			Kind = kind;
			Weight = BladeWeight.Light;

			switch (kind) {
				case DaggerKind.MainGauche: {
					Def = new DefenseDagger(ac);
					break;
				}
				case DaggerKind.SwordBreaker: {
					Def = new DefenseDagger(ac);
					break;
				}
			}
		}
	}

	class DefenseDagger : Defense {
		public DefenseDagger(int ac) {
			ArmourClass = ac;
		}
	}

	class Shield : Defense {		
		public Shield(String name, int id, int m_dur, int ac) {
			Name = name;
			Id = 30 + id;
			MaxDur = m_dur;
			CurDur = m_dur;
			Stackable = false;
			Quantity = 1;

			ArmourClass = ac;
		}
	}

	class Armour : Defense {
		public Armour(String name, int id, int m_dur, int ac) {
			Name = name;
			Id = id;
			MaxDur = m_dur;
			CurDur = m_dur;
			Stackable = false;
			Quantity = 1;

			ArmourClass = ac;
		}
	}
}
