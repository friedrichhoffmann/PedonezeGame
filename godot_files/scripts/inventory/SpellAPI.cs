using Godot;
using MagicSystem;
using InventoryFile;
using System.Collections.Generic;

namespace SpellApiFile {
    class SpellApi {
        private Inventory inventory;
		public static string InventoryFilePath = ProjectSettings.GlobalizePath("res://scripts/inventory/inventory.toml");

        public void OpenSpellInventory() {
            inventory = Inventory.LoadTOML(InventoryFilePath);
        }

        public void CloseSpellInventory() {
            inventory.UpdateTOML(InventoryFilePath);
        }

        public List<Spell> GetSpellsAsList(long key) {
            return inventory.PlayerSpellSet[key];
        }

        public void AddSpell(long key, Spell spell) {
            inventory.PlayerSpellSet[key].Add(spell); 
        }
    }
}


