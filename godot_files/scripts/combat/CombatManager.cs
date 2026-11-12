using Godot;
using System.Threading.Tasks;
using System.Collections.Generic;

using MainCharacterFile;
using CharacterFile;
using GenericEnemy;
using EnemyFile;
using AllyFile;

using System;
using System.Linq;

using static PreMadeEnemies.Enemies;

using CombatAPI;
using SpellApiFile;
using InventoryAPIFile;

using MagicSystem;
using SimpleItems;

using SpawnCardsFile;

namespace CombatManagerFile {

    public partial class CombatManager : Control
    {
        private const int ATTACK_OPTION = 0;
        private const int SPELL_OPTION = 1;
        private const int INVENTORY_OPTION = 2;

        private const bool TWO_HANDED = true;

        public static ItemList menu;
        public static AnimationPlayer animationManager;

        public static int? ActivatedOption;
        private static TaskCompletionSource<int> _clickSource;
    
        Battle battle = new Battle();

        private static void ListToItemList<T>(List<T> list) {
            menu.Clear();
            ActivatedOption = null;
            bool skipBack = false;
            if (list.Count() != 0) {
                if (list.Last().ToString() == "SkipBack") { 
                    skipBack = true;
                    list.RemoveAt(list.Count -1);
                }
                foreach (T element in list) {
                    menu.AddItem(element.ToString(),null,true);
                }
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

        // Called when the node enters the scene tree for the first time.
		public override void _Ready() {
            menu = GetNode<ItemList>("UI/Menu/MenuBg/MenuList");

            menu.ItemActivated -= OnItemActivated;
            menu.ItemActivated += OnItemActivated;

            animationManager = GetNode<AnimationPlayer>("/root/Control/AnimationPlayer");
            InventoryAPI inv = new();
            inv.Init();

		    // battle.HeroJoinBattle(Player.player);
            
            Combatant.All = new List<Combatant>();

            Combatant.All.Add(banditOne());
            Combatant.All.Add(banditTwo());
            Combatant.All.Add(Cleric.cleric);
            Combatant.All.Add(Gladiator.gladiator);
            Combatant.All.Add(Wizard.wizard);
            Combatant.All.Add(Archer.archer);

		    battle.InitiativeTest();

            foreach (Combatant comb in Combatant.All) {
                if (comb is Enemy enm) {
                    switch (enm) {
                        case Bandit        bandit:               bandit.SetAttacksUp(); break;
                        case Plebeian      plebeian:           plebeian.SetAttacksUp(); break;
                        case TribalWarrior tribalWarrior: tribalWarrior.SetAttacksUp(); break;
                        case BanditCaptain banditCaptain: banditCaptain.SetAttacksUp(); break;
                        case BigRat        bigRat:               bigRat.SetAttacksUp(); break;
                        case Boar          boar:                   boar.SetAttacksUp(); break;
                        case Deer          deer:                   deer.SetAttacksUp(); break;
                        case ItalianWolf   italianWolf:     italianWolf.SetAttacksUp(); break;
                        case BrownBear     brownBear:         brownBear.SetAttacksUp(); break;
                        case BigBoar       bigBoar:             bigBoar.SetAttacksUp(); break;
                        case BigDeer       bigDeer:             bigDeer.SetAttacksUp(); break;
                        case Hippopotamus  hippopotamus:   hippopotamus.SetAttacksUp(); break;
                        case Unicorn       unicorn:             unicorn.SetAttacksUp(); break;

                        default:
                            GD.PrintErr($"No attack setup is defined for {enm.GetType().Name} ({enm.Name}).");
                            break;
                    }
                }
                else if (comb is MainCharacter mainCharacter) {
                    switch (mainCharacter) {
                        case Gladiator gladiator: gladiator.SetSpellsUp(); break;
                        case Cleric    cleric:       cleric.SetSpellsUp(); break;
                        case Archer    archer:       archer.SetSpellsUp(); break;
                        case Wizard    wizard:       wizard.SetSpellsUp(); break;

                        default:
                            GD.PrintErr($"No spell setup is defined for {mainCharacter.GetType().Name} ({mainCharacter.Name}).");
                            break;
                    }
                }
            }


            SpawnCards.SpawnPlayerCards(
                    Combatant.All.Where(comb => comb is MainCharacter)
                    .Cast<MainCharacter>().ToArray()
            );

            SpawnCards.SpawnEnemyCards(
                    Combatant.All.Where(comb => comb is Enemy)
                    .Cast<Enemy>().ToArray()
            );

            menu.ItemActivated += OnItemActivated;
		    _ = battle.StartBattle();
        }

        private static Task<int> WaitForActiveAsync() {
            _clickSource = new TaskCompletionSource<int>(
            TaskCreationOptions.RunContinuationsAsynchronously);

            return _clickSource.Task;
        }

        private void OnItemActivated(long index) {
            int selectedIndex = (int)index;
            string selectedText = menu.GetItemText(selectedIndex);

            ActivatedOption = selectedIndex;
            GD.Print($"Activated item {selectedIndex}: {selectedText}");

            _clickSource?.TrySetResult(selectedIndex);
        }


        private static async Task equipWeapon(MainCharacter hero, Weapon toActOnItem) {
            ListToItemList(["Equip"]);
            int selectedEquipWeapon = await WaitForActiveAsync();
            if (selectedEquipWeapon != 0) return; bool isTwoHanded = (toActOnItem.Hand == TWO_HANDED);
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

        private static async Task<bool> inventoryOpt(MainCharacter hero) {
            InventoryAPI inventory = new();
            inventory.OpenInventory();

            try
            {
                List<Item> items = inventory.GetItemsAsList();
                ListToItemList(items.Select(i => $"{i.Name}    {i.Quantity}x").ToList());
                menu.GrabFocus();

                int selected = await WaitForActiveAsync();

                if (selected < 0 || selected >= items.Count)
                    return false; // Back

                Item item = items[selected];

                if      (item is Armour armour) await equipArmour(hero, armour);
                else if (item is Weapon weapon) await equipWeapon(hero, weapon);
                else if (item is Shield shield) await equipWeapon(hero, shield);
                else if (item is Edible edible) await consumeItem(hero, edible);

                return true; // Item selected
            }
            finally
            {
                inventory.CloseInventory();
            }
        }
        private static async Task<bool> spellOpt(MainCharacter hero) {
            GD.Print($"spellOpt started for {hero.Name}");
            
            string key = hero switch {
                Cleric _ => typeof(Cleric).Name,
                Archer _ => typeof(Archer).Name,
                Wizard _ => typeof(Wizard).Name,
                Gladiator _ => typeof(Gladiator).Name,
                _ => "Unknown"
            }; 

            SpellApi spellApi = new();
            spellApi.OpenSpellInventory();

            List<string> availableSpells = new();
            foreach (string spellName in spellApi.GetSpellsAsList(key)) {
                var spl = spellApi.GetSpellById(spellName);
                if (spl.ManaCost <= hero.Mana)
                    availableSpells.Add(spellName.Capitalize());
            }

            ListToItemList(availableSpells);
            menu.GrabFocus();

            int selected = await WaitForActiveAsync();
            if (selected >= availableSpells.Count) return false; // back

            var spellId = availableSpells[selected].ToUpper().Replace(' ', '_');
            Spell spell = spellApi.GetSpellById(spellId);

            spellApi.CloseSpellInventory();

            switch (spell.Range) {
                case ActRange.Self:
                case ActRange.Broad:
                    hero.CastSpell(spell);
                    return true;

                case ActRange.Focused:
                    var targets = Combatant.All
                        .OfType<Enemy>()
                        .Where(enemy => enemy.Health > 0)
                        .ToList();

                    if (targets.Count == 0)
                        return true; // Spell selected, but there is no valid target.

                    ListToItemList(targets.Select(enemy => enemy.Name).ToList());
                    menu.GrabFocus();

                    int targetIndex = await WaitForActiveAsync();

                    // Back from target selection: return to the spell list.
                    if (targetIndex < 0 || targetIndex >= targets.Count)
                        return false;

                    hero.CastSpell(spell, targets[targetIndex]);
                    return true;

                default:
                    return true;
            }
        }


        private static async Task<bool> attackOpt(MainCharacter hero) {
            var targets = Combatant.All
                .OfType<Enemy>()
                .Where(enemy => enemy.Health > 0)
                .ToList();

            ListToItemList(targets.Select(enemy => enemy.Name).ToList());
            menu.GrabFocus();

            int targetIndex = await WaitForActiveAsync();

            // Back was selected, or there are no targets.
            if (targetIndex < 0 || targetIndex >= targets.Count)
                return false;

            var target = targets[targetIndex];
            var attack = hero.RightHandWeapon != null
                ? hero.RightHandWeapon.WeaponDamage.ToAttack()
                : PreMadeItems.PreMadeWeapons.BareHands().WeaponDamage.ToAttack();

            hero.AttackTarget(attack, target);
            return true;
        }

        public static async Task TakeAction(MainCharacter hero) {
            GD.Print("TakeAction started");
            while (true) {
                ListToItemList(["Attack", "Hability", "Inventory", "SkipBack"]);
                menu.GrabFocus();

                int opt = await WaitForActiveAsync();

                switch (opt) {
                    case ATTACK_OPTION:
                        if (await attackOpt(hero))
                            return; // Attack was made; finish this action.
                        break;      // Back: show the main action menu again.
                    case SPELL_OPTION:
                        if (await spellOpt(hero))
                            return;
                        break; // Back: reopen main menu

                    case INVENTORY_OPTION:
                        if (await inventoryOpt(hero))
                            return;
                        break; // Back: reopen main menu

                    default:
                        return;
                }
            }
        }
    }
}
