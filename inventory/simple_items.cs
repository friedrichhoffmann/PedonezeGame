// using System.Text.Json.Serialization;

namespace SimpleItems {

	interface IItem {
		string Name { get; set; }
		int Id { get; set; }
		int MaxDur { get; set; } // max durability
		int CurDur { get; set; } // current durability
		bool Stackable { get; set; }
		int Quantity { get; set; }
	}

	abstract class Item : IItem {
		public string Name { get; set; }
		public int Id { get; set; }
		public int MaxDur { get; set; } // max durability
		public int CurDur { get; set; } // current durability
		public bool Stackable { get; set; }
		private int quantity;

		public int Quantity {
			get { return quantity; }
			set {
				if (Stackable) {
					quantity = value;
				} else if (quantity == 0) {
					quantity = Math.Clamp(value, 0, 1);
				} else {
					Console.WriteLine($"Cannot Stack {Name}");
				}
			}
		}
	}

	abstract class Weapon : Item {
		public int Damage { get; set; }
	}

	class Defense : Item {
		public int ArmourClass { get; set; }			
	}
}
