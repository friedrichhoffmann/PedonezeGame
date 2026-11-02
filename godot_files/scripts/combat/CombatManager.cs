using Godot;
using System.Threading.Tasks;
using System.Collections.Generic;

using MainCharacterFile;
using CharacterFile;
using GenericEnemy;
using PhyAttack;
using EnemyFile;

using System.Linq;

using CombatAPI;

using InventoryAPIFile;
using SimpleItems;

namespace CombatManagerFile {

    public partial class CombatManager : Control
    {
        Battle battle = new Battle();
        public static ItemList menu;
        public static int? ActivatedOption;
        private static TaskCompletionSource<int> _clickSource;

        
        private static void ListToItemList<T>(List<T> list) {
            menu.Clear();
            ActivatedOption = null;
            bool skipBack = false;

            if (list.Last().ToString() == "SkipBack") { 
                skipBack = true;
                list.RemoveAt(list.Count -1);
            }
			foreach (T element in list) {
                menu.AddItem(element.ToString(),null,true);
            }
            if (!skipBack) {
                menu.AddItem("Back",null,true);
            }
            
		}
        
        // Called when the node enters the scene tree for the first time.
		public override void _Ready() {
            menu = GetNode<ItemList>("Menu/MenuBg/MenuList");

		    // battle.HeroJoinBattle(Player.player);
		    battle.EnemyJoinBattle(Bandit.banditOne);

		    Combatant.All.AddRange(Combatant.Enemy);
		    Combatant.All.AddRange(Combatant.Hero);
		    battle.InitiativeTest();

            menu.ItemActivated += OnItemActivated;
		    battle.StartBattle();
        }

        private static Task<int> WaitForActiveAsync()
        {
            _clickSource = new TaskCompletionSource<int>(
                TaskCreationOptions.RunContinuationsAsynchronously);

            return _clickSource.Task;
        }

        private void OnItemActivated(long index)
        {
            int selectedIndex = (int)index;
            string selectedText = menu.GetItemText(selectedIndex);

            ActivatedOption = selectedIndex;
            GD.Print($"Activated item {selectedIndex}: {selectedText}");

            _clickSource?.TrySetResult(selectedIndex);
        }


        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta) {}

        public static async Task TakeAction(Combatant _hero) {
            var hero = _hero as MainCharacter;

            ListToItemList(["Attack", "Hability", "Inventory", "SkipBack"]);
            menu.GrabFocus();

            int opt = await WaitForActiveAsync();
            switch (opt) {
                case 0:
                    ListToItemList(Combatant.All
                            .Where(enm => enm.Health > 0)
                            .Where(enm => enm is Enemy)
                            .Select(enm => enm.Name).ToList());

                    int targetIndex = await WaitForActiveAsync();

                    hero.AttackTarget(
                            hero.RightHandWeapon.WeaponDamage.ToAttack(),
                            Combatant.All.Where(enm => enm.Health > 0)
                            .Where(enm => enm is Enemy)
                            .ToList()[targetIndex]
                        );
                    return;
                case 1:
                    return;
                case 2: {
                    InventoryAPI _inventory = new InventoryAPI();
                   
                    _inventory.OpenInventory();
                    
                    List<Item> itemsInInventory = _inventory.GetItemsAsList();
                    ListToItemList(itemsInInventory.Select(i => $"{i.Name}    {i.Quantity}x").ToList());

                    int selectedItem = await WaitForActiveAsync();
                    Item toEquipItem = itemsInInventory[selectedItem];
                    
                    if (toEquipItem is Armour) {
                        GD.Print($"{toEquipItem.Name} is Armour");
                         
                        ListToItemList(["Equip Armour"]);
                        int selectedEquipArmour = await WaitForActiveAsync();
                        switch (selectedEquipArmour) {
                            case 0: {
                                if (hero.ArmourSlot != null) {
                                    hero.Unequip(MainCharacter.ARMOUR_SLOT);
                                }
                                hero.Equip(toEquipItem, MainCharacter.ARMOUR_SLOT);
                                _inventory.CloseInventory();
                                return;
                            }
                            default: {
                                _inventory.CloseInventory();
                                return;
                                //todo
                            }
                        }
                    } else if (toEquipItem is Weapon) {
                        GD.Print($"{toEquipItem.Name} is Weapon");
                        ListToItemList(["Equip to right hand", "Equip to left hand"]);
                        int selectedEquipHand = await WaitForActiveAsync();

                        switch (selectedEquipHand) {
                            case MainCharacter.RIGHT_HAND_SLOT: {
                                if (hero.RightHandWeapon != null) {
                                    hero.Unequip(MainCharacter.RIGHT_HAND_SLOT);
                                }
                                hero.Equip(toEquipItem, MainCharacter.RIGHT_HAND_SLOT);
                                _inventory.CloseInventory();
                                return;
                            }
                                                            
                            case MainCharacter.LEFT_HAND_SLOT: {
                                if (hero.LeftHandWeapon != null) {
                                    hero.Unequip(MainCharacter.LEFT_HAND_SLOT);
                                }
                                hero.Equip(toEquipItem, MainCharacter.LEFT_HAND_SLOT);
                                _inventory.CloseInventory();
                                return;
                            }

                            default: {
                                _inventory.CloseInventory();
                                return;
                                // todo
                            }
                        }
                    } else {
                        GD.Print($"{toEquipItem.Name} is not armour nor weapon");
                        _inventory.CloseInventory();
                        return;
                        // todo
                    }
                }
            }
        }
    }
}
