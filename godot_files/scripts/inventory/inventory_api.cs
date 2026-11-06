using Godot;
using Tomlyn;
using System.IO;
using SimpleItems;
using InventoryFile;
using System.Collections.Generic;

namespace InventoryAPIFile {
	class InventoryAPI {
		private Inventory inventory;
		public static string InventoryFilePath = ProjectSettings.GlobalizePath("res://scripts/inventory/inventory.toml");

        public void OpenInventory() => inventory = Inventory.LoadTOML(InventoryFilePath);
        public void CloseInventory() => inventory.UpdateTOML(InventoryFilePath);
        public List<Item> GetItemsAsList() => inventory.StoredItems;


		public void AddItem(Item toAddItem) {
			int foundItemIndex = inventory.StoredItems.FindIndex(iterItem => iterItem.Id == toAddItem.Id);
			if (foundItemIndex >= 0 && inventory.StoredItems[foundItemIndex].Stackable) {
				inventory.StoredItems[foundItemIndex].Quantity++;
			} else {
				inventory.StoredItems.Add(toAddItem);
			}
		}

		public void AddItemFromTOML(string itemPath) {
			string toml = File.ReadAllText(itemPath);
			Item toAddItem = TomlSerializer.Deserialize<Item>(toml, Inventory.TomlOptions);
            
			int foundItemIndex = inventory.StoredItems.FindIndex(iterItem => iterItem.Id == toAddItem.Id);
			if (foundItemIndex >= 0 && inventory.StoredItems[foundItemIndex].Stackable) { 
				inventory.StoredItems[foundItemIndex].Quantity++; 
			} else {
				inventory.StoredItems.Add(toAddItem);
			}
		}

		public Item GetItem(int itemId) {
			int foundItemIndex = inventory.StoredItems.FindIndex(iterItem => iterItem.Id == itemId);
			if (foundItemIndex >= 0) {
				return inventory.StoredItems[foundItemIndex];
			} else {
				return null;
			}
		}

		public Item GetItem(string itemName) {
			int foundItemIndex = inventory.StoredItems.FindIndex(iterItem => iterItem.Name == itemName);
			if (foundItemIndex >= 0) {
				return inventory.StoredItems[foundItemIndex];
			} else {
				return null;
			}
		}


		public Item GetItem(Item item) {
			int foundItemIndex = inventory.StoredItems.FindIndex(iterItem => iterItem.Id == item.Id);
			if (foundItemIndex >= 0) {
				return inventory.StoredItems[foundItemIndex];
			} else {
				return null;
			}
		}

		public void RemoveItem(Item item) {
			int foundItemIndex = inventory.StoredItems.FindIndex(iterItem => iterItem.Id == item.Id);
			if (foundItemIndex >= 0) {
				if ( inventory.StoredItems[foundItemIndex].Quantity-- > 1) {
					inventory.StoredItems[foundItemIndex].Quantity--;
				} else {
					inventory.StoredItems.RemoveAt(foundItemIndex);
				}
			}
		}
	}
}
