using PhyAttack;
using MagicSystem;
using System;
using Dice;

namespace SimpleItems
{
    interface IItem
    {
        string Name { get; set; }
        int Id { get; set; }
        int MaxDurability { get; set; }
        int CurrentDurability { get; set; }
        bool Stackable { get; set; }
        int Quantity { get; set; }
    }

    public class Item(
        string name,
        int id,
        int maxDurability,
        bool stackable
    ) : IItem
    {
        private int quantity;

        public string Name { get; set; } = name;
        public int Id { get; set; } = id;
        public int MaxDurability { get; set; } = maxDurability;
        public int CurrentDurability { get; set; } = maxDurability;
        public bool Stackable { get; set; } = stackable;

        public int Quantity
        {
            get { return quantity; }
            set {
                if (Stackable)
                    quantity = value;
                else if (quantity == 0)
                    quantity = Math.Clamp(value, 0, 1);
            }
        }
    }

    public class Weapon(
        string name,
        int id,
        int maxDurability,
        bool hand,
        WeaponDamage weaponDamage
    ) : Item(name, id, maxDurability, false)
    {
        public bool Hand { get; set; } = hand;
        public WeaponDamage WeaponDamage { get; set; } = weaponDamage;
    }

    public class WeaponDamage(
        int dieAmount,
        int dieFaces,
        AttackRange attackRange,
        DamageKind damageKind
    )
    {
        private readonly int dieAmount = dieAmount;
        private readonly int dieFaces = dieFaces;

        public string StringDice { get; set; } = $"{dieAmount}d{dieFaces}";
        public AttackRange Range { get; set; } = attackRange;
        public DamageKind Kind { get; set; } = damageKind;

        public Func<int> RollDamage { get; set; } = () => Die.Roll(dieAmount, dieFaces);

        private void RestoreDiceDamage()
        {
            if (string.IsNullOrWhiteSpace(StringDice))
                throw new ArgumentNullException(nameof(StringDice), "StringDice cannot be null or empty.");

            string[] parts = StringDice.Split('d');
            if (parts.Length != 2 || !int.TryParse(parts[0], out int parsedDieAmount) || !int.TryParse(parts[1], out int parsedDieFaces))
                throw new FormatException($"StringDice format is invalid: {StringDice}. Expected format: '{{dieAmount}}d{{dieFaces}}'");

            RollDamage = () => Die.Roll(parsedDieAmount, parsedDieFaces);
        }

        public Attack ToAttack() {
            RestoreDiceDamage();
            return new Attack {
                Damage = RollDamage(),
                Kind = Kind,
                Range = Range,
            };
        }
    }

    public abstract class Defense(
        string name,
        int id,
        int maxDurability,
        Effect defense
    ) : Item(name, id, maxDurability, false)
    {
        private Effect armourClass = ValidateDefense(defense);

        public Effect ArmourClass
        {
            get => armourClass;
            set => armourClass = ValidateDefense(value);
        }

        private static Effect ValidateDefense(Effect value) =>
            value.Kind == EffectKind.Defense
            ? value
            : throw new ArgumentException(
                "Effect must be a defense effect.",
                nameof(value)
            );
    }

    public class Shield(
        string name,
        int id,
        int maxDurability,
        Effect defense
    ) : Defense(name, id, maxDurability, defense) {}

    public class Armour(
            string name,
            int id,
            int maxDurability,
            Effect defense
            ) : Defense(name, id, maxDurability, defense) {}

}

