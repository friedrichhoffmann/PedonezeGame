using SpellApiFile;
using MainCharacterFile;
using static PreMadeSpells.GlobalMagic;

namespace AllyFile
{
    
    public class Gladiator(
        int levelXp,
        string name,
        int healthMax,
        int manaMax,
        int health,
        int mana,
        int armor,
        int initiativeBonus,
        int xp
    ) : MainCharacter(
        levelXp,
        name,
        healthMax,
        manaMax,
        health,
        mana,
        armor,
        initiativeBonus,
        xp
    ) {
        public static Gladiator gladiator = new(
            levelXp: 1,
            name: "Gladiator",
            healthMax: 8,
            manaMax: 16,
            health: 8,
            mana: 16,
            armor: 13,
            initiativeBonus: 1,
            xp: 0
        );

        public void SetSpellsUp() {
            SpellApi splAPI = new();
            splAPI.OpenSpellInventory();

            string key = "Gladiator";

            if (!splAPI.HasSpellList(key))
                if (!splAPI.HasSpells(key))
                    splAPI.AddSpells(key,[]);

            splAPI.CloseSpellInventory();
        }
    }

    public class Cleric(
        int levelXp,
        string name,
        int healthMax,
        int manaMax,
        int health,
        int mana,
        int armor,
        int initiativeBonus,
        int xp
    ) : MainCharacter(
        levelXp,
        name,
        healthMax,
        manaMax,
        health,
        mana,
        armor,
        initiativeBonus,
        xp
    ) {
        public static Cleric cleric = new(
            levelXp: 1,
            name: "Clérigo",
            healthMax: 8,
            manaMax: 16,
            health: 8,
            mana: 16,
            armor: 13,
            initiativeBonus: 1,
            xp: 0
        );

        // Cleric
        public void SetSpellsUp() {
            SpellApi splAPI = new();
            splAPI.OpenSpellInventory();

            string key = "Cleric";

            if (!splAPI.HasSpellList(key))
                if (!splAPI.HasSpells(key))
                    splAPI.AddSpells(key,[
                            GUIDED_DANCE,
                            SACRED_FLAME,
                            CONTROLLED_FLAMES,
                            MASS_CURE,
                            HEALING_THE_WORLD,
                            PHYSICAL_PROTECTION,
                            BANISHMENT,
                            MASS_HEALING_SOUND,
                            HEAL
                    ]);
            splAPI.CloseSpellInventory();
        }

    }

    public class Archer(
        int levelXp,
        string name,
        int healthMax,
        int manaMax,
        int health,
        int mana,
        int armor,
        int initiativeBonus,
        int xp
    ) : MainCharacter(
        levelXp,
        name,
        healthMax,
        manaMax,
        health,
        mana,
        armor,
        initiativeBonus,
        xp
    ) {
        public static Archer archer = new(
            levelXp: 1,
            name: "Arqueiro",
            healthMax: 8,
            manaMax: 16,
            health: 8,
            mana: 16,
            armor: 12,
            initiativeBonus: 2,
            xp: 0
        );
        // Archer
        public void SetSpellsUp() {
            SpellApi splAPI = new();
            splAPI.OpenSpellInventory();

            string key = "Archer";

            if (!splAPI.HasSpellList(key))
                if (!splAPI.HasSpells(key))
                    splAPI.AddSpells(key,[
                            GOODBERRY,
                            HAIL_OF_THORNS,
                            ENTANGLE,
                            PROTECTION_FROM_ENERGY
                    ]);
            splAPI.CloseSpellInventory();
        }

    }

    public class Wizard(
        int levelXp,
        string name,
        int healthMax,
        int manaMax,
        int health,
        int mana,
        int armor,
        int initiativeBonus,
        int xp
    ) : MainCharacter(
        levelXp,
        name,
        healthMax,
        manaMax,
        health,
        mana,
        armor,
        initiativeBonus,
        xp
    ) {
        public static Wizard wizard = new(
            levelXp: 1,
            name: "Mago",
            healthMax: 8,
            manaMax: 16,
            health: 6,
            mana: 16,
            armor: 10,
            initiativeBonus: 0,
            xp :0
        );
        // Wizard
        public void SetSpellsUp() {
            SpellApi splAPI = new();
            splAPI.OpenSpellInventory();

            string key = "Wizard";

            if (!splAPI.HasSpellList(key))
                if (!splAPI.HasSpells(key))
                    splAPI.AddSpells(key,[
                            RAY_OF_FROST,
                            ACID_SPLASH,
                            FIRE_BOLT,
                            MAGIC_MISSILE,
                            EARTH_TREMOR,
                            SHATTER,
                            SCORCHING_RAY,
                            FIREBALL,
                            MELFS_MINUTE_METEORS,
                            POISON_SPRAY,
                            IMMOLATION
                    ]);
            splAPI.CloseSpellInventory();
        }

    }
}

