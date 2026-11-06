using System;
using System.Collections.Generic;
using MainCharacterFile;
using MagicSystem;
using SimpleItems;
using System.IO;
using Tomlyn;

namespace InventoryFile {
	class Inventory {
		public List<Item> StoredItems { get; set; }
        public List<MainCharacter> TeamStatus { get; set; }
        public Dictionary<long, List<Spell>> PlayerSpellSet { get; set; }

        internal static readonly TomlSerializerOptions TomlOptions = new() {
			PolymorphismOptions = new TomlPolymorphismOptions {
				TypeDiscriminatorPropertyName = "kind",
                DerivedTypeMappings = new Dictionary<Type, IReadOnlyList<TomlDerivedType>> {
                    [typeof(Item)] = [
                        new(typeof(Weapon), "Weapon"),
                        new(typeof(Item), "StdItem"),
                        new(typeof(Shield), "Shield"),
                        new(typeof(Armour), "Armour"),
                        new(typeof(Spell), "Spell"),
                        new(typeof(Effect), "Effect"),
                        new(typeof(MainCharacter), "MainCharacter"),
                    ]
                }
			}
	    };

        public static Inventory LoadTOML(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    throw new FileNotFoundException(
                        $"Inventory file was not found: {Path.GetFullPath(path)}",
                        path
                    );
                }
                
                Inventory inv;
                string toml = File.ReadAllText(path);
                if (toml.Length != 0) { 
                    inv = TomlSerializer.Deserialize<Inventory>(toml, TomlOptions);
                } else {
                    inv = new();
                }


                if (inv == null)
                {
                    throw new InvalidDataException(
                        $"Deserializing the inventory returned null. File: {Path.GetFullPath(path)}"
                    );
                }

                return inv;
            }
            catch (Exception e)
            {
                // Log the full exception in Godot, including its stack trace.
                Godot.GD.PrintErr($"Failed to load inventory from '{path}':\n{e}");
                throw; // Re-throw so the caller doesn't continue with an invalid inventory.
            }
        }


		public void UpdateTOML(string path) {
		   string toml = TomlSerializer.Serialize(this, TomlOptions);
		   // Console.WriteLine($"toml to write: {toml}");
		   File.WriteAllText(path, toml);
		}

	}
}



