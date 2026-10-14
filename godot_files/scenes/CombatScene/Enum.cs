namespace EnumFile
{
      public enum ActionType
    {
        // Magias que ajudam o conjurador
        MageArmor,
        FireShield,
        GoodBerry,
        ProtectionFromWind,
        TrueSeeing,
        BladeWard,
        Barkskin,
        Resistance,
        Guidance,
        FlameArrows,
        // Magias que ajudam um aliado
        DeathWard,
        HealingWord,
        CureWounds,
        Cure,
        ShieldOfFaith,
        GreaterRestoration,
        Heroism,
        LesserRestoration,
        // Magias que ajudam todos
        Aid,
        AuraOfLife,
        AuraOfVitality,
        MassCureWord,
        MassCureWounds,
        CrusadersMantle,
        MassHealingWord,
        BeaconOfHope,
        // Magias que prejudicam um inimigo
        TashasHideousLaugh,
        TrueStrike,
        EnsnaringStrike,
        Banishment,
        MindSpike,
        ControlFlames,
        Sleep,
        Entangle,
        ThunderWaves,
        DimensionDoor,
        AcidArrow,
        SacredFlame,
        Immolation,
        Investiture,
        AcidSplash,
        FireBolt,
        MelfsMinuteMeteors,
        MagicMissile,
        Frostbite,
        CloudOfDaggers,
        Poison,
        Shatter,
        RayOfSickness,
        RayOfFrost,
        ScorchingRay,
        EldritchBlast,
        HellishRebuke,
        HoldPerson,
        Catnap,
        PoisonSpray,
        ShockingGrasp,
        EarthTremor,
        Thunderclap,
        ViciousMockery,
        ThunderStep,
        // Magias que afetam todos os inimigos
        Fireball,
        IceStorm,
        Dawn,
        // Ataques físicos
        BanditScimitar,
        BanditCrossbow,
        TribalWarriorSpear,
        PlebeianStick,
        RatBite,
        BoarCharge,
        DeerHorn,
        ItalianWolfBite,
        BrownBearBite,
        BrownBearCrawl,
        BanditCaptainScimitar,
        BanditCaptainDagger,
        CentaurSpear,
        CentaurLongBow,
        BigBoarCharge,
        BigDeerHorn,
        HippopotamusBite,
        UnicornKick,
        UnicornHorn,
        // Habilidades
        MysticArcanum,
        BrutalStrike,
        DivineFavor,
        ExtraAttack,
        ImprovedDivineSmite,
        ProtectionAura,
        BardicDie,
        ChannelDivinity,
        DivineCall,
        Foresight,
        CantStop,
        ReviveTheDeath,
        WeaponNMastery,
        LayOnHands,
        Rage,
        RelentlessRage,
        SecondWind,
        ActionSurge,
    }

    public enum ActionResult
    {
        InvalidTarget,
        NoSpellSlot,
    }
    
    public enum StateType
    {
        Incapacitated, // feito
        Poisoned, // feito
        Paralysed, // feito
        Feared
    }

    public enum EffectType
    {
        Baned, // feito
        MagicArmor,
        Disadvantage, // feito
        FaithShield, // feito 
        FireShield, // feito
        Barkskin, // efeito
        Resistance, // feito 
        AllResistance, // feito
        DeathAward
    }
    public enum DamageType
    {
        None,
        PiercingDamage,
        SlashingDamage,
        BludgeoningDamage
    }
        
    public enum DamageResistance
    {
        None,
        PiercingResistance,
        SlashingResistance,
        BludgeoningResistance
    }
}