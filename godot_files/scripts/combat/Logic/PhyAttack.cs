using ResistanceFile;
using CharacterFile;
using System.Linq;
using SimpleItems;
using System;

namespace PhyAttack {
    public enum DamageKind {
        Piercing,
        Slashing,
        Bludgeoning
    }

    public enum AttackRange {
        Broad,
        Focused,
    }

    public class Attack {
        public int Damage { get; set; }
        public DamageKind Kind { get; set; }
        public AttackRange Range { get; set; }

        public bool VerifyArmourClass(Combatant target) {
            return Damage >= target.Armour;
        }

        public Attack ApplyResistance(Combatant target) {
            var resistances = target.Resistances;
            bool resistsToThis = resistances.Any(res => res.Kind == ToResistanceKind());

            // i know i could use Where and Select but i fount it to be more verbose this way
            if (resistsToThis) { 
                foreach (Resistance res in resistances) {
                    if (res.Kind == ToResistanceKind()) {
                        Damage -= res.Degree;
                    }
                }
            }
            
            return this;
        }

        private ResistanceKind ToResistanceKind() {
            return Kind switch {
                DamageKind.Bludgeoning => ResistanceKind.Bludgeoning,
                DamageKind.Piercing    => ResistanceKind.Piercing,
                DamageKind.Slashing    => ResistanceKind.Slashing,
                _ => throw new ArgumentOutOfRangeException(nameof(Kind), Kind, null),
            };
        } 
    }
}

