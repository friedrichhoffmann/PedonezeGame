using System;
using MagicSystem;
using CharacterFile;
using PhyAttack;

namespace PreMadeSpells {

    public abstract class GlobalMagic {
        private const ActRange BROAD   = ActRange.Broad;
        private const ActRange FOCUSED = ActRange.Focused;
        private const ActRange SELF    = ActRange.Self;

        private const EffectKind REMOVE_CONDITION = EffectKind.RemoveCondition;
        private const EffectKind DAMAGE_BOOST     = EffectKind.DamageBoost;
        private const EffectKind IMMUNITY         = EffectKind.Immunity;
        private const EffectKind NUMBNESS         = EffectKind.Numbness;
        private const EffectKind HEALING          = EffectKind.Healing;
        private const EffectKind DEFENSE          = EffectKind.Defense;
        private const EffectKind UNLUCK           = EffectKind.Unluck;
        private const EffectKind THORNS           = EffectKind.Thorns;

        private const DamageKind MAGIC            = DamageKind.Magic;

        private static readonly DamageKind[] MAGIC_RES       = [DamageKind.Magic];
        private static readonly DamageKind[] SLASHING_RES    = [DamageKind.Slashing];
        private static readonly DamageKind[] PIERCING_RES    = [DamageKind.Piercing];
        private static readonly DamageKind[] BLUDGEONING_RES = [DamageKind.Bludgeoning];

        private static readonly DamageKind[] PHYSICAL_RES    = [SLASHING_RES[0],PIERCING_RES[0],BLUDGEONING_RES[0]];
        private static readonly DamageKind[] FULL_RES        = [MAGIC_RES[0], SLASHING_RES[0],PIERCING_RES[0],BLUDGEONING_RES[0]];

        private static int FullCycle = (Combatant.All.Count -1);    

        public static Func<string[], Effect> Poisoned                = (pwr) => new Effect(UNLUCK , 3, "Poisoned", pwr);
        public static Func<string[], Effect> Paralyzed               = (pwr) => new Effect(NUMBNESS , 2, "Paralyzed", pwr);
        public static Func<string[], Effect> Feared                  = (pwr) => new Effect(UNLUCK , 3, "Feared", pwr);
        public static Func<string[], Effect> Unabled                 = (pwr) => new Effect(NUMBNESS , 2, "Unabled", pwr);
        public static Func<string[], Effect> Heal                    = (pwr) => new Effect(HEALING, 1, "Heal", pwr);
        public static Func<string[], Effect> DamageBoost             = (pwr) => new Effect(DAMAGE_BOOST, FullCycle, "Damage Boost", pwr);

        public static Func<string[], Effect[],Effect>             Immunity        = (pwr,eff) => new Immunity(eff, 3, "Damage Boost", pwr);
        public static Func<string[], ActRange,MagicAttack>        Damage          = (pwr,ran) => new MagicAttack(ran,1,"Magic Damage", pwr);
        public static Func<string[], DamageKind[],Effect>         Resistance      = (pwr,res) => new Resistance(res, FullCycle-1, "Resistance", pwr);
        public static Func<string[], EffectKind,ConditionRemover> RemoveCondition = (pwr,con) => new ConditionRemover(con,1,"ConditionRemover", pwr);

        // Spells
        public static Spell MASS_HEALING_WORD      = new Spell([Heal(["2d04"])],        BROAD,                5 , 6);
        public static Spell MASS_HEALING_SOUND     = new Spell([Heal(["5d08"])],        BROAD,                10, 10);
        public static Spell HELP                   = new Spell([Heal(["1d06"])],        BROAD,                3 , 4);
        public static Spell AURA_OF_VITALITY       = new Spell([Heal(["2d06"])],        BROAD,                11, 6);
        public static Spell HEALING_THE_WORLD      = new Spell([Heal(["1d06"])],        FOCUSED,              4 , 0);
        public static Spell HEAL                   = new Spell([Heal(["20d4"])],        FOCUSED,              11, 12);
        public static Spell HEROISM                = new Spell([Heal(["1d04"])],        FOCUSED,              1 , 2);
        public static Spell GOODBERRY              = new Spell([Heal(["1d04"])],        FOCUSED,              0 , 2);
        public static Spell CURE_WOUNDS            = new Spell([Heal(["1d08","3"])],    FOCUSED,              1 , 2);
        public static Spell MASS_CURE              = new Spell([Heal(["2d08","3"])],    FOCUSED,              3 , 4);

        public static Spell HELLISH_REBUKE         = new Spell([Damage(["2d10"],        BROAD)],              1 , 2);
        public static Spell FIREBALL               = new Spell([Damage(["8d06"],        BROAD)],              5 , 6);
        public static Spell LIGHT_BALL             = new Spell([Damage(["8d06"],        BROAD)],              1 , 2);
        public static Spell THUNDER_STEP           = new Spell([Damage(["3d10"],        FOCUSED)],            11, 6);
        public static Spell BANE                   = new Spell([Damage(["1d04"],        FOCUSED)],            3 , 2);
        public static Spell TRUE_STRIKE            = new Spell([Damage(["1d06"],        FOCUSED)],            1 , 0);
        public static Spell MIND_BOMB              = new Spell([Damage(["3d08"],        FOCUSED)],            3 , 4);
        public static Spell CONTROLLED_FLAMES      = new Spell([Damage(["1d08"],        FOCUSED)],            2 , 2);
        public static Spell HAIL_OF_THORNS         = new Spell([Damage(["2d04"],        FOCUSED)],            3 , 2);
        public static Spell ACID_ARROWS            = new Spell([Damage(["6d04"],        FOCUSED)],            3 , 4);
        public static Spell SACRED_FLAME           = new Spell([Damage(["1d08"],        FOCUSED)],            1 , 0);
        public static Spell IMMOLATION             = new Spell([Damage(["12d6"],        FOCUSED)],            10, 10);
        public static Spell ACID_SPLASH            = new Spell([Damage(["1d06"],        FOCUSED)],            1 , 0);
        public static Spell FIRE_BOLT              = new Spell([Damage(["1d10"],        FOCUSED)],            1 , 0);
        public static Spell MELFS_MINUTE_METEORS   = new Spell([Damage(["2d06"],        FOCUSED)],            6 , 6);
        public static Spell SHATTER                = new Spell([Damage(["3d08"],        FOCUSED)],            3 , 4);
        public static Spell RAY_OF_SICKNESS        = new Spell([Damage(["1d08"],        FOCUSED)],            3 , 4);
        public static Spell RAY_OF_FROST           = new Spell([Damage(["1d08"],        FOCUSED)],            1 , 0);
        public static Spell RAY_OF_LIGHT           = new Spell([Damage(["8d06"],        FOCUSED)],            5 , 6);
        public static Spell SCORCHING_RAY          = new Spell([Damage(["2d06"],        FOCUSED)],            9 , 4);
        public static Spell ELDRITCH_BLAST         = new Spell([Damage(["1d10"],        FOCUSED)],            4 , 0);
        public static Spell POISON_SPRAY           = new Spell([Damage(["1d12"],        FOCUSED)],            1 , 0);
        public static Spell SHOCKING_GRASP         = new Spell([Damage(["1d08"],        FOCUSED)],            4 , 0);
        public static Spell EARTH_TREMOR           = new Spell([Damage(["1d06"],        FOCUSED)],            1 , 2);
        public static Spell THUNDERCLAP            = new Spell([Damage(["1d06"],        FOCUSED)],            10, 0);
        public static Spell DIMENSIONAL_PORTAL     = new Spell([Damage(["4d06"],        FOCUSED)],            7 , 8);
        public static Spell CLOUD_OF_DAGGERS       = new Spell([Damage(["4d04"],        FOCUSED)],            5 , 4);
        public static Spell ENTANGLE               = new Spell([Damage(["1d06"],        FOCUSED)],            5 , 2);
        public static Spell MAGIC_MISSILE          = new Spell([Damage(["3d04","3"],    FOCUSED)],            1 , 2);
        public static Spell INVESTIDURE            = new Spell([Damage(["3d10","4d08"], FOCUSED)],            7, 14);
        public static Spell BLIZZARD               = new Spell([Damage(["2d10","4d06"], BROAD)],              7 , 8);

        public static Spell THORNS_BASE_DAMAGE     = new Spell([Damage(["2d08"],        FOCUSED)],            0 , 0);

        public static Spell CRUSADERS_MANTLE       = new Spell([DamageBoost(["1d04"])], BROAD,                9 , 6);
        public static Spell GUIDED_DANCE           = new Spell([DamageBoost(["1d04"])], FOCUSED,              1 , 0);
        public static Spell FIRE_ARROWS            = new Spell([DamageBoost(["1d06"])], FOCUSED,              9 , 6);
        public static Spell BLIND_STRIKE           = new Spell([DamageBoost(["3d08"])], FOCUSED,              9 , 6);
        public static Spell MARKING_STRIKE         = new Spell([DamageBoost(["2d06"])], FOCUSED,              5 , 4);
        public static Spell DIVINE_SMITE           = new Spell([DamageBoost(["2d08"])], FOCUSED,              3 , 2);
        public static Spell BLAZING_STRIKE         = new Spell([DamageBoost(["2d06"])], FOCUSED,              5 , 2);

        public static Spell SLEEP                  = new Spell([Unabled([""])],         FOCUSED,              1 , 2);
        public static Spell BANISHMENT             = new Spell([Unabled([""])],         FOCUSED,              8 , 8);
        public static Spell HOLD_PERSON            = new Spell([Paralyzed([""])],       FOCUSED,              3 , 4);
        public static Spell HOLD_MONSTER           = new Spell([Paralyzed([""])],       FOCUSED,              10, 10);
        public static Spell MONSTER_ENCHANTER      = new Spell([Unabled([""])],         FOCUSED,              9 , 8);
        public static Spell NAP                    = new Spell([Unabled([""])],         FOCUSED,              5 , 6);

        public static Spell PROTECTION_FROM_ENERGY = new Spell([Resistance(["1"],       MAGIC_RES)],    SELF, 9 , 4);
        public static Spell PROTECTION_FROM_BLADES = new Spell([Resistance(["1"],       SLASHING_RES)], SELF, 4 , 0);
        public static Spell PHYSICAL_PROTECTION    = new Spell([Resistance(["1"],       PHYSICAL_RES)], SELF, 6 , 6);
        public static Spell FULL_RESISTANCE        = new Spell([Resistance(["1d4"],     FULL_RES)],  FOCUSED, 1 , 0);

        public static Spell POISON_CLOUD           = new Spell([RemoveCondition([""],   NUMBNESS)], FOCUSED,  6 , 6);
        public static Spell TRUE_SENSES = new Spell(
                [Heal(["2d10"]), 
                    Immunity(
                        [""], 
                        [Poisoned([""])]
                    )
                ], SELF, 11, 10
        );

        public static Spell VISCIOUS_MOCKERY = new Spell(
            [ Damage(["1d06"],FOCUSED), 
              Feared([""])
            ], 2, 0
        );
        public static Spell COLD_BITE = new Spell(
            [ Damage(["1d06"],FOCUSED), 
              Feared([""])
            ], 1, 0
        );
    }
}

