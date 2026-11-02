using MagicSystem;
using CharacterFile;
using System.Collections.Generic;

namespace MagicSystem {
    public enum EffectKind {
        Resistance,
        Numbness,
        Healing,
        Defense,
        Unluck,
        Replay
    }

    public enum EffectRange {
        Focused,
        Broad,
        Self,
    }

    public class Effect {
        public EffectKind Kind { get; set; }
        public int DurationInTurns { get; set; }
        public int Lasting { get; set; }
        public string Name { get; set; }
        public int Power { get; set; }

        public void ApplyEffect(Combatant target) {
            switch(Kind) {
                case EffectKind.Healing: {
                    if (Lasting > 0)
                        target.Health+=Power;
                } break;

                case EffectKind.Defense: {
                    if (Lasting == DurationInTurns) {
                        target.PastState=target.Armour;
                        target.Armour+=Power;
                    } else if (Lasting == 0) {
                        target.Armour=target.PastState;
                    }
                } break;
            }
        }
    }

    public class Spell {
        public EffectRange Range { get; set; }
        public int RequiredLevel { get; set; }
        public List<Effect> SpellEffect { get; set; }
        public int SpellPower { get; set; }
    }
}

namespace CharacterFile {
    public abstract partial class Combatant {
        public void CastSpell(Spell spell) {
            if (spell.Range == EffectRange.Broad) {
                foreach (Effect effect in spell.SpellEffect) {
                    foreach (Combatant enemy in Combatant.Enemy) {
                        enemy.ActiveEffects.Add(effect);
                    }
                }
            } else if (spell.Range == EffectRange.Self) {
                foreach (Effect effect in spell.SpellEffect) {
                    this.ActiveEffects.Add(effect);
                }
            }
        }

        public void CastSpell(Spell spell, Combatant target) {
            if (spell.Range == EffectRange.Focused) {
                
                int targetIndex = Combatant.All.IndexOf(target);

                foreach (Effect effect in spell.SpellEffect) {
                    Combatant.All[targetIndex].ActiveEffects.Add(effect);
                }
            }
        }

        public static void DealEffect() {
            foreach (Combatant combatant in Combatant.All) {
                foreach (Effect effect in combatant.ActiveEffects) {
                    if (effect.Lasting == 0) {
                        combatant.ActiveEffects.Remove(effect);
                    } else {
                        effect.ApplyEffect(combatant);
                        effect.Lasting--;
                    }
                }
            }
        }
    }
}
