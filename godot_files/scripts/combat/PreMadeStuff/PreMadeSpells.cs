using Dice;
using MagicSystem;
using CharacterFile;

namespace PreMadeSpells {
    public static class GlobalConditions {
        public static Effect Poisoned = new Effect {
            Kind = EffectKind.Unluck,
            DurationInTurns = 3,
            Lasting = 3,
            Name = "Poisoned"
        };
        
        public static Effect Paralyzed = new Effect {
            Kind = EffectKind.Numbness,
            DurationInTurns = 2,
            Lasting = 2,
            Name = "Paralyzed"
        };

        public static Effect Feared = new Effect {
            Kind = EffectKind.Unluck,
            DurationInTurns = 3,
            Lasting = 3,
            Name = "Feared"
        };

        public static Effect Unabled = new Effect {
            Kind = EffectKind.Numbness,
            DurationInTurns = 2,
            Lasting = 2,
            Name = "Unabled"
        };
    }

    public static class GlobalEffects {
        // Healing Degrees
        public static Effect HealI = new Effect {
            Name = "Heal I",
            Lasting = 1,
            DurationInTurns = 1,
            Kind = EffectKind.Healing,
            Power=Die.Roll(1, Die.d6),
        };

        public static Effect HealII = new Effect {
            Name = "Heal II",
            Lasting = 1,
            DurationInTurns = 1,
            Kind = EffectKind.Healing,
            Power=Die.Roll(1, Die.d8),
        };

        public static Effect HealIII = new Effect {
            Name = "Heal III",
            Lasting = 1,
            DurationInTurns = 1,
            Kind = EffectKind.Healing,
            Power=Die.Roll(2, Die.d4),
        };
        
        public static Effect HealIV = new Effect {
            Name = "Heal IV",
            Kind = EffectKind.Healing,
            Lasting = 1,
            DurationInTurns = 1,
            Power=3+Die.Roll(2, Die.d8),
        };

        public static Effect HealV = new Effect {
            Name = "Heal V",
            Lasting = Combatant.All.Count-1,
            DurationInTurns = Combatant.All.Count-1,
            Kind = EffectKind.Healing,
            Power=Die.Roll(5, Die.d8),
        };

        public static Effect ActionMadnessI = new Effect {
            Name = "Action Madness I",
            Lasting = 2+1,
            DurationInTurns = 2+1,
            Kind = EffectKind.Replay,
        }; 
    }

    public static class GlobalSpells {
        
    }
}

