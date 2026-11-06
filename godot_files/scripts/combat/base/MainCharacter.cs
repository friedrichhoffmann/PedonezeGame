using System;
using System.Linq;
using System.Threading.Tasks;

using CharacterFile;
using CombatManagerFile;

using SimpleItems;
using InventoryAPIFile;

using MagicSystem;

namespace MainCharacterFile {
    public abstract class MainCharacter : Combatant
    {
        
        public const int RIGHT_HAND_SLOT = 0;
        public const int LEFT_HAND_SLOT = 1;
        public const int ARMOUR_SLOT = 2;

        private int mana;
        private int characterLevel;

        private int manaMax;

        #nullable enable
        public Weapon? RightHandWeapon { get; set; }
        public Weapon? LeftHandWeapon { get; set; }
        public Armour? ArmourSlot { get; set; }
        #nullable disable

        public long SpellSetKey { get; set; }
        
        public int Level {
            get => characterLevel;
            set => characterLevel = Math.Clamp(value, 1, 11);
        }

        public int Mana { 
            get => mana;
            set => mana = Math.Clamp(value, 0, manaMax);
        }

        public bool CanPlay(Combatant combatant) {
            return combatant.Health > 0 || combatant.ActiveEffects.Any(effect => effect.Kind == EffectKind.Numbness);
        }

        public async Task HeroTurn() {
            if (CanPlay(this)) await CombatManager.TakeAction(this);
            return;
        }

        public void IncreaseManaMax(int newValue) => manaMax = newValue;
        public void IncreaseHealthMax(int newValue) => healthMax = newValue;
          
        public override void CastSpell(Spell spell) {
            switch (spell.Range) {
                case ActRange.Broad:
                    foreach (Effect effect in spell.SpellEffect) {
                        foreach (Combatant enemy in Combatant.Enemy) enemy.ActiveEffects.Add(effect);
                    }
                    break;
                case ActRange.Self:
                    foreach (Effect effect in spell.SpellEffect) this.ActiveEffects.Add(effect);
                    break;
            }
            this.Mana -= spell.ManaCost;
        }

        public override void CastSpell(Spell spell, Combatant target) {
            if (spell.Range == ActRange.Focused) {
                int targetIndex = Combatant.All.IndexOf(target);

                foreach (Effect effect in spell.SpellEffect) {
                    Combatant.All[targetIndex].ActiveEffects.Add(effect);
                }
            }
            this.Mana -= spell.ManaCost;
        } 

        public void Equip(Item toEquipItem, int slot) {
            InventoryAPI inventoryAPI = new();
            inventoryAPI.OpenInventory();

            Item itemFromInventory = inventoryAPI.GetItem(toEquipItem);
            inventoryAPI.RemoveItem(itemFromInventory);

            inventoryAPI.CloseInventory();

            switch (slot) {
                case RIGHT_HAND_SLOT:
                    if (itemFromInventory is Weapon rightWeapon) {
                        RightHandWeapon = rightWeapon;
                    }
                    break;

                case LEFT_HAND_SLOT:
                    if (itemFromInventory is Weapon leftWeapon) {
                        LeftHandWeapon = leftWeapon;
                    }
                    break;

                case ARMOUR_SLOT:
                    if (itemFromInventory is Armour armour) {
                        ArmourSlot = armour;
                    }
                    break;
            }
        }

        public void Unequip(int slot) {
            InventoryAPI inventoryAPI = new();
            inventoryAPI.OpenInventory();

            switch (slot) {
                case RIGHT_HAND_SLOT:
                    if (RightHandWeapon is not null) {
                        inventoryAPI.AddItem(RightHandWeapon);
                        RightHandWeapon = null;
                    }
                    break;

                case LEFT_HAND_SLOT:
                    if (LeftHandWeapon is not null) {
                        inventoryAPI.AddItem(LeftHandWeapon);
                        LeftHandWeapon = null;
                    }
                    break;

                case ARMOUR_SLOT:
                    if (ArmourSlot is not null) {
                        inventoryAPI.AddItem(ArmourSlot);
                        ArmourSlot = null;
                    }
                    break;
            }

            inventoryAPI.CloseInventory();
        }

        public MainCharacter(
            int levelXp,
            string name,
            int health,
            int resistanceBonus,
            int damageBonus,
            int armor,
            int initiativeBonus,
            int attackBonus,
            int xp)
            : base(
                name,
                health,
                resistanceBonus,
                damageBonus,
                armor,
                initiativeBonus,
                attackBonus,
                xp)
        {
            Level = levelXp;
        }
    }
}
