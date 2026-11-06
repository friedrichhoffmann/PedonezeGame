using Godot;
using System.Threading.Tasks;
using System.Collections.Generic;

using MainCharacterFile;
using CharacterFile;
using GenericEnemy;
using EnemyFile;

using System;
using System.Linq;

using CombatAPI;
using SpellApiFile;

using InventoryAPIFile;
using MagicSystem;
using SimpleItems;

namespace CombatManagerFile {

    public partial class CombatManager : Control
    {
        private const int ATTACK_OPTION = 0;
        private const int SPELL_OPTION = 1;
        private const int INVENTORY_OPTION = 2;

        private const bool TWO_HANDED = true;

        Battle battle = new Battle();
        public static ItemList menu;
        public static int? ActivatedOption;
        private static TaskCompletionSource<int> _clickSource;
    
        private static long RandomDigits(long digitsAmount) {
            long output = 0;

            for (int i=0; i < digitsAmount; i++) {
                int number = Random.Shared.Next(1, 10);
                output += (long)(number * Math.Pow(10,i));
            }

            return output;
        }

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

        private static void ListToItemList(List<Spell> list, Func<Spell, bool> state) {
            menu.Clear();
            ActivatedOption = null;
            bool skipBack = false;

            if (list.Last().ToString() == "SkipBack") { 
                skipBack = true;
                list.RemoveAt(list.Count -1);
            }
			foreach (Spell element in list) {
                menu.AddItem(element.ToString(),null,state(element));
            }
            if (!skipBack) {
                menu.AddItem("Back",null,true);
            }
		}

        private static void GenerateSpellSetKey(List<Combatant> all) {
            foreach (Combatant combatant in all) {
                if (combatant is MainCharacter mainCharacter) {
                    mainCharacter.SpellSetKey = RandomDigits(5);
                }
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
		    _ = battle.StartBattle();
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


        private static async Task equipWeapon(MainCharacter hero, Weapon toActOnItem) {
            ListToItemList(["Equip"]);
            int selectedEquipWeapon = await WaitForActiveAsync();
            if (selectedEquipWeapon != 0) return; 

            bool isTwoHanded = (toActOnItem.Hand == TWO_HANDED);

            if (isTwoHanded) {
                if (hero.RightHandWeapon != null) hero.Unequip(MainCharacter.RIGHT_HAND_SLOT);
                if (hero.LeftHandWeapon != null) hero.Unequip(MainCharacter.LEFT_HAND_SLOT);
                hero.Equip(toActOnItem, MainCharacter.RIGHT_HAND_SLOT);
            } else {
                if (hero.RightHandWeapon != null) hero.Unequip(MainCharacter.RIGHT_HAND_SLOT);
                hero.Equip(toActOnItem, MainCharacter.RIGHT_HAND_SLOT);
            }
        }

        private static async Task equipWeapon(MainCharacter hero, Shield toActOnItem) {
            ListToItemList(["Equip"]);
            int selectedEquipWeapon = await WaitForActiveAsync();
            if (selectedEquipWeapon != 0) return;

            if (hero.RightHandWeapon.Hand == TWO_HANDED) {
                // todo: tell the player to unequip the weapon first
            } else {
                if (hero.LeftHandWeapon != null) hero.Unequip(MainCharacter.LEFT_HAND_SLOT);
                hero.Equip(toActOnItem, MainCharacter.LEFT_HAND_SLOT);
            }
        }

        private static async Task equipArmour(MainCharacter hero, Item toActOnItem) {
            ListToItemList(["Equip"]);
            int selectedEquipArmour = await WaitForActiveAsync();
            if (selectedEquipArmour != 0) return;

            if (hero.ArmourSlot != null) hero.Unequip(MainCharacter.ARMOUR_SLOT);
            hero.Equip(toActOnItem, MainCharacter.ARMOUR_SLOT);
        }

        private static async Task consumeItem(MainCharacter hero, Edible edible) {
            ListToItemList(["Consume"]);
            int selectedEquipArmour = await WaitForActiveAsync();
            if (selectedEquipArmour != 0) return;

            hero.ActiveEffects.Add(edible.HealingRate);
        }

        private static async Task inventoryOpt(MainCharacter hero) {
            InventoryAPI _inventory = new InventoryAPI();

            _inventory.OpenInventory();

            List<Item> itemsInInventory = _inventory.GetItemsAsList();
            ListToItemList(itemsInInventory.Select(i => $"{i.Name}    {i.Quantity}x").ToList());

            int selectedItem = await WaitForActiveAsync();
            Item toActOnItem = itemsInInventory[selectedItem];

            if      (toActOnItem is Armour armour) await equipArmour(hero, armour);
            else if (toActOnItem is Weapon weapon) await equipWeapon(hero, weapon);
            else if (toActOnItem is Shield shield) await equipWeapon(hero, shield);
            else if (toActOnItem is Edible edible) await consumeItem(hero, edible);

            _inventory.CloseInventory();
        }

        private static async Task spellOpt<T>(MainCharacter hero) {
            SpellApi spellApi = new();
            spellApi.OpenSpellInventory();

            List<Spell> spellSet = spellApi.GetSpellsAsList(hero.SpellSetKey);


            Func<Spell, bool> func = (t) => t.ManaCost <= hero.Mana;
            ListToItemList(spellSet, func);
            menu.GrabFocus();

            int opt = await WaitForActiveAsync();
            Spell spell = spellSet[opt];

            switch(spell.Range) {
                case ActRange.Self:
                    hero.CastSpell(spell);
                    break;
                case ActRange.Broad:
                    hero.CastSpell(spell);
                    break;
                case ActRange.Focused:
                    var targets = Combatant.All .OfType<Enemy>() .Where(enemy => enemy.Health > 0) .ToList();

                    ListToItemList(targets.Select(enemy => enemy.Name).ToList());

                    int targetIndex = await WaitForActiveAsync();
                    var target = targets[targetIndex];


                    hero.CastSpell(spell, target);
                    break;
            }
            return;
        }    

        private static async Task attackOpt(MainCharacter hero) {
            var targets = Combatant.All
                .OfType<Enemy>()
                .Where(enemy => enemy.Health > 0)
                .ToList();

            ListToItemList(targets.Select(enemy => enemy.Name).ToList());

            int targetIndex = await WaitForActiveAsync();

            var target = targets[targetIndex];
            var attack = hero.RightHandWeapon.WeaponDamage.ToAttack();

            hero.AttackTarget(attack, target);
            return;
        }

        public static async Task TakeAction(MainCharacter hero) {
            ListToItemList(["Attack", "Hability", "Inventory", "SkipBack"]);
            menu.GrabFocus();

            int opt = await WaitForActiveAsync();
            switch (opt) {
                case ATTACK_OPTION:
                    await attackOpt(hero);
                    break;
                case SPELL_OPTION:
                    break;
                case INVENTORY_OPTION:
                    await inventoryOpt(hero);
                    break;
            }
        }
    }
}
