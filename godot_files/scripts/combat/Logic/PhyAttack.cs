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
            foreach (Effect eff in target.ActiveEffects)
                if (eff is Resistance res)
                    if (res.ResKind.Contains(this.Kind))
                         this.Damage -= (int)res.Power();
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

