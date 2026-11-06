using PhyAttack;
using MagicSystem;
using SimpleItems;

namespace PreMadeItems {
    class PreMadeWeapons {
        // verbosity consts

        private const bool SINGLE_HANDED = false;
        private const bool DOUBLE_HANDED = true;

        private const DamageKind BLUDGEONING = DamageKind.Bludgeoning;
        private const DamageKind PIERCING = DamageKind.Piercing;
        private const DamageKind SLASHING = DamageKind.Slashing;

        private const ActRange FOCUSED = ActRange.Focused;
        private const ActRange BROAD = ActRange.Broad;

        static WeaponDamage DAMAGE(string dice, ActRange range, DamageKind kind) {
            string[] diceAmountDieFaces = dice.Split('d');
            int diceAmount = int.Parse(diceAmountDieFaces[0]);
            int dieFaces = int.Parse(diceAmountDieFaces[1]);

            return new WeaponDamage(diceAmount, dieFaces, range, kind);
        }

                                                        //  Name            id     dur  Hands          Damage  Dice    Range     Kind

        public static Weapon Club()          => new Weapon("Club",          100,   045, SINGLE_HANDED, DAMAGE( "1d04", FOCUSED , BLUDGEONING));
        public static Weapon Dagger()        => new Weapon("Dagger",        101,   090, SINGLE_HANDED, DAMAGE( "1d04", FOCUSED , PIERCING));
        public static Weapon Greatclub()     => new Weapon("Greatclub",     102,   030, DOUBLE_HANDED, DAMAGE( "1d08", FOCUSED , BLUDGEONING));
        public static Weapon Handaxe()       => new Weapon("Handaxe",       103,   060, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , SLASHING));
        public static Weapon Javelin()       => new Weapon("Javelin",       104,   060, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , PIERCING));
        public static Weapon LightHammer()   => new Weapon("Light Hammer",  105,   060, SINGLE_HANDED, DAMAGE( "1d04", FOCUSED , BLUDGEONING));
        public static Weapon Mace()          => new Weapon("Mace",          106,   090, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , BLUDGEONING));
        public static Weapon Quarterstaff()  => new Weapon("Quarterstaff",  107,   075, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , BLUDGEONING));
        public static Weapon Sickle()        => new Weapon("Sickle",        108,   060, SINGLE_HANDED, DAMAGE( "1d04", FOCUSED , SLASHING));
        public static Weapon Spear()         => new Weapon("Spear",         109,   075, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , PIERCING));

        public static Weapon Dart()          => new Weapon("Dart",          110,   030, SINGLE_HANDED, DAMAGE( "1d04", FOCUSED , PIERCING));
        public static Weapon LightCrossbow() => new Weapon("Light Crossbow",111,   120, DOUBLE_HANDED, DAMAGE( "1d08", FOCUSED , PIERCING));
        public static Weapon Shortbow()      => new Weapon("Shortbow",      112,   090, DOUBLE_HANDED, DAMAGE( "1d06", FOCUSED , PIERCING));
        public static Weapon Sling()         => new Weapon("Sling",         113,   045, SINGLE_HANDED, DAMAGE( "1d04", FOCUSED , BLUDGEONING));

        public static Weapon Battleaxe()     => new Weapon("Battleaxe",     114,   150, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , SLASHING));
        public static Weapon Flail()         => new Weapon("Flail",         115,   150, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , BLUDGEONING));
        public static Weapon Glaive()        => new Weapon("Glaive",        116,   210, DOUBLE_HANDED, DAMAGE( "1d10", FOCUSED , SLASHING));
        public static Weapon Greataxe()      => new Weapon("Greataxe",      117,   240, DOUBLE_HANDED, DAMAGE( "1d12", FOCUSED , SLASHING));
        public static Weapon Greatsword()    => new Weapon("Greatsword",    118,   240, DOUBLE_HANDED, DAMAGE( "2d06", FOCUSED , SLASHING));
        public static Weapon Halberd()       => new Weapon("Halberd",       119,   210, DOUBLE_HANDED, DAMAGE( "1d10", FOCUSED , SLASHING));
        public static Weapon Lance()         => new Weapon("Lance",         120,   150, SINGLE_HANDED, DAMAGE( "1d10", FOCUSED , PIERCING));
        public static Weapon Longsword()     => new Weapon("Longsword",     121,   180, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , SLASHING));
        public static Weapon Maul()          => new Weapon("Maul",          122,   210, DOUBLE_HANDED, DAMAGE( "2d06", FOCUSED , BLUDGEONING));
        public static Weapon Morningstar()   => new Weapon("Morningstar",   123,   180, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , PIERCING));
        public static Weapon Pike()          => new Weapon("Pike",          124,   240, DOUBLE_HANDED, DAMAGE( "1d10", FOCUSED , PIERCING));
        public static Weapon Rapier()        => new Weapon("Rapier",        125,   180, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , PIERCING));
        public static Weapon Scimitar()      => new Weapon("Scimitar",      126,   150, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , SLASHING));
        public static Weapon Shortsword()    => new Weapon("Shortsword",    127,   150, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , PIERCING));
        public static Weapon Trident()       => new Weapon("Trident",       128,   180, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , PIERCING));
        public static Weapon Warhammer()     => new Weapon("Warhammer",     129,   180, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , BLUDGEONING));
        public static Weapon WarPick()       => new Weapon("War pick",      130,   120, SINGLE_HANDED, DAMAGE( "1d08", FOCUSED , PIERCING));
        public static Weapon Whip()          => new Weapon("Whip",          131,   090, SINGLE_HANDED, DAMAGE( "1d04", FOCUSED , SLASHING));

        public static Weapon Blowgun()       => new Weapon("Blowgun",       132,   150, SINGLE_HANDED, DAMAGE( "1d02", FOCUSED , PIERCING));
        public static Weapon HandCrossbow()  => new Weapon("Hand Crossbow", 133,   210, SINGLE_HANDED, DAMAGE( "1d06", FOCUSED , PIERCING));
        public static Weapon HeavyCrossbow() => new Weapon("Heavy Crossbow",134,   240, DOUBLE_HANDED, DAMAGE( "1d10", FOCUSED , PIERCING));
        public static Weapon Longbow()       => new Weapon("Longbow",       135,   210, DOUBLE_HANDED, DAMAGE( "1d08", FOCUSED , PIERCING));
    }
}

