using Godot;
using CharacterFile;
using System.Linq;
using System;
using MagicSystem;

namespace PhyAttack {
    public enum DamageKind {
        Piercing,
        Slashing,
        Bludgeoning,
        Magic
    }

    public class Attack(
        int dam,
        DamageKind kind,
        ActRange ran
    ) {
        public int Damage { get; set; } = dam;
        public DamageKind Kind { get; set; } = kind;
        public ActRange Range { get; set; } = ran;

        public bool VerifyArmourClass(Combatant target) {
            return Damage >= target.Armour;
        }

        public void ApplyResistance(Combatant target) {
            GD.Print($"ApplyResistance called for {target.Name}");
            GD.Print($"Effects: {target.ActiveEffects.Count}");
            foreach (Effect eff in target.ActiveEffects) {
                GD.Print("Entered the foreach");
                if (eff is Resistance res) {
                    GD.Print($"{eff} is Resistance");
                    if (res.ResKind.Contains(this.Kind)) {
                        GD.Print("Got to the inner if");
                        this.Damage -= (int)res.Power();
                    }
                }
            }
        }

        public void ApplyThorns(Combatant target, Combatant attacker) {
            if (target.ActiveEffects.Any(eff => eff.Kind == EffectKind.Thorns)) {
                target.CastSpell(PreMadeSpells.GlobalMagic.THORNS_BASE_DAMAGE, attacker);
            }
        }

        public void ApplyBoost(Combatant attacker) {
            foreach (Effect eff in attacker.ActiveEffects)
                if (eff.Kind == EffectKind.DamageBoost)
                    this.Damage += (int)eff.Power();
        }
    }
}

