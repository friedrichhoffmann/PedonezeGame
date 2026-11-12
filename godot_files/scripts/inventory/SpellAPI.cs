using Godot;
using MagicSystem;
using InventoryFile;
using PreMadeSpells;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SpellApiFile
{
    class SpellApi
    {
        private Inventory inventory = new();

        public static string InventoryFilePath =
            ProjectSettings.GlobalizePath("res://scripts/inventory/inventory.toml");

        private static readonly Dictionary<string, Spell> SpellsById = BuildSpellMap();
        private static readonly Dictionary<Spell, string> IdBySpell =
            BuildReverseSpellMap(SpellsById);

        private static Dictionary<string, Spell> BuildSpellMap() {
            var map = new Dictionary<string, Spell>();

            foreach (FieldInfo field in typeof(GlobalMagic).GetFields(
                BindingFlags.Public | BindingFlags.Static)) {
                if (field.FieldType == typeof(Spell) &&
                    field.GetValue(null) is Spell spell) {
                    map.Add(field.Name, spell);
                }
            }

            return map;
        }

        private static Dictionary<Spell, string> BuildReverseSpellMap(
            Dictionary<string, Spell> spellsById) {
            var map = new Dictionary<Spell, string>();

            foreach (var entry in spellsById)
                map.Add(entry.Value, entry.Key);

            return map;
        }

        public bool HasSpellList(string key) {
            return inventory.PlayerSpellSet.ContainsKey(key);
        }


        public bool HasSpells(string key) {
            return inventory.PlayerSpellSet.TryGetValue(key, out var spellIds)
                && spellIds.Count > 0;
        }

        public void OpenSpellInventory() {
            inventory = Inventory.LoadTOML(InventoryFilePath);
        }

        public void CloseSpellInventory() {
            inventory.UpdateTOML(InventoryFilePath);
        }

        public List<string> GetSpellsAsList(string key) {
            if (!inventory.PlayerSpellSet.TryGetValue(key, out List<string> spellIds))
                throw new KeyNotFoundException($"No spell list exists for '{key}'.");

            return new List<string>(spellIds);
        }

        public Spell GetSpellById(string spellId) {
            if (!SpellsById.TryGetValue(spellId, out Spell spell))
                throw new KeyNotFoundException($"Unknown spell ID '{spellId}'.");

            return spell;
        }


        public void AddSpell(string key, Spell spell) {
            if (!IdBySpell.TryGetValue(spell, out string spellId))
                throw new ArgumentException(
                    "The spell is not a predefined GlobalMagic spell.",
                    nameof(spell));

            if (!inventory.PlayerSpellSet.TryGetValue(key, out List<string> spellIds)) {
                spellIds = new List<string>();
                inventory.PlayerSpellSet[key] = spellIds;
            }

            spellIds.Add(spellId);
        }

        public void AddSpells(string key, Spell[] spells) {
            foreach (Spell spell in spells)
                AddSpell(key, spell);
        }
    }
}

