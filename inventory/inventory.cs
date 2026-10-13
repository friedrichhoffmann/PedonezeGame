using System;
using System.Collections.Generic;
using SimpleItems;
using ComplexItems;
using System.Text.Json;
using System.IO;
using Tomlyn;

class Inventory {
	public List<Item> Items { get; set; } = new();

	private static readonly TomlSerializerOptions TomlOptions = new() {
		PolymorphismOptions = new TomlPolymorphismOptions {
			TypeDiscriminatorPropertyName = "kind",
			DerivedTypeMappings = new Dictionary<Type, IReadOnlyList<TomlDerivedType>> {
				[typeof(Item)] = new TomlDerivedType[] {
					new(typeof(Dagger), "dagger"), // for the program to recover the childclasses of 
					new(typeof(Sword), "sword"), // it is necessary to declare them here 
					new(typeof(DefenseDagger), "defense_dagger"),
					new(typeof(StaticItem), "static_item"),
				}
			}
		}
	};

	public void LoadTOML(String path) {
		string toml = File.ReadAllText(path);
		Items = TomlSerializer.Deserialize<Inventory>(toml, TomlOptions).Items;
	}

	public void AddItemFromTOML(String path) {
		string toml = File.ReadAllText(path);
		var addedItem = TomlSerializer.Deserialize<Item>(toml, TomlOptions);
		Items.Add(addedItem);
	}

	public Item GetItemById(int id) {
		return Items.Find(i => i.Id == id);
	}

	public Item GetItemByName(string nm) {
		return Items.Find(i => i.Name == nm);
	}

	public void UpdateTOML(string path) {
	   string toml = TomlSerializer.Serialize(this, TomlOptions);
	   // Console.WriteLine($"toml to write: {toml}");
	   File.WriteAllText(path, toml);
	}

}




