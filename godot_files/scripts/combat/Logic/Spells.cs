using Dice;
using System;
using PhyAttack;
using MagicSystem;
using System.Linq;
using CharacterFile;

namespace MagicSystem {
    public enum EffectKind {
        RemoveCondition,
        DamageBoost,
        Resistance,
        Numbness,
        Immunity,
        Healing,
        Defense,
        Unluck,
        Thorns,
        Damage,
        Replay
    }

    public enum ActRange {
        Focused,
        Broad,
        Self,
    }

    public class Effect(
        EffectKind effKind,
        int duration,
        string name,
        string[] power
    ) {
        public EffectKind Kind { get; set; } = effKind;
        public int DurationInTurns { get; set; } = duration;
        public int Lasting { get; set; } = duration;
        public string Name { get; set; } = name;
        public string[] StringPower { get; set; } = power;

        public int? Power() {
            int? totalPower = 0;

            foreach (string power in StringPower) {
                if (power.Length == 0) {
                    totalPower += 0; 
                    continue;
                }
                else if (power.Contains('d')) {
                    totalPower += Die.RollString(power); 
                    continue;
                }
                else if (int.TryParse(power, out int intPower)) {
                    totalPower += intPower; 
                    continue;
                }
                else {
                    throw new FormatException($"Invalid power value: '{power}'.");
                }
            }

            return totalPower;
        }

        public static int? Power(string[] StringPower) {
            int? totalPower = 0;

            foreach (string power in StringPower) {
                if (power.Length == 0) {
                    totalPower += 0; 
                    continue;
                }
                else if (power.Contains('d')) {
                    totalPower += Die.RollString(power); 
                    continue;
                }
                else if (int.TryParse(power, out int intPower)) {
                    totalPower += intPower; 
                    continue;
                }
                else {
                    throw new FormatException($"Invalid power value: '{power}'.");
                }
            }
            return totalPower;
        }


        public void ApplyEffect(Combatant target) {
            foreach (Effect eff in target.ActiveEffects)
                if(eff is Immunity imm)
                    if(imm.HarmlessEffects.Contains(this))
                        return;

            switch(Kind) {
                case EffectKind.Healing:
                    if (Lasting > 0) target.Health+=(int)Power();
                    break;

                case EffectKind.Defense: 
                    if (Lasting == DurationInTurns) {
                        target.PastState=target.Armour;
                        target.Armour+=(int)Power();
                    } else if (Lasting == 0) {
                        target.Armour=target.PastState;
                    }
                    break;

                case EffectKind.Damage:
                    if (Lasting == DurationInTurns)
                        if (this is MagicAttack atk)
                            target.AttackTarget(atk.attack(), target);
                    break;

                case EffectKind.RemoveCondition:
                    if (this is ConditionRemover rem)
                        target.ActiveEffects.RemoveAll(eff => eff.Kind == rem.Kind);
                    break;
            }
        }
    }

    public class MagicAttack(
        ActRange range,
        int duration,
        string name,
        string[] power
    ) : Effect(EffectKind.Damage, duration, name, power) {
        public Func<Attack> attack { get; set; } = delegate() { 
            return new Attack((int)Power(power), DamageKind.Magic, range); 
        };
    }

    public class ConditionRemover(
        EffectKind kindRemove,
        int duration,
        string name,
        string[] power
    ) : Effect(EffectKind.RemoveCondition, duration, name, power) {
        public EffectKind ToRemoveCondition { get; set; } = kindRemove;
    }


    public class Resistance(
        DamageKind[] kind,
        int duration,
        string name,
        string[] power
    ) : Effect(EffectKind.Resistance, duration, name, power) {
        public DamageKind[] ResKind { get; set; } = kind;
    }

    public class Immunity(
        Effect[] eff,
        int duration,
        string name,
        string[] power
    ) : Effect(EffectKind.Resistance, duration, name, power) {
        public Effect[] HarmlessEffects { get; set; } = eff;
    }


    public class Spell {
        public Effect[] SpellEffect { get; set; }
        public ActRange Range { get; set; }
        public int RequiredLevel { get; set; }
        public int ManaCost { get; set; }

        public Spell(
            Effect[] spellEffect,
            ActRange range,
            int requiredLevel,
            int manaCost
        ) {
             SpellEffect = spellEffect;
             Range = range;
             RequiredLevel = requiredLevel;
             ManaCost = manaCost;
        }

        public Spell( 
            Effect[] spellEffect,
            int requiredLevel, 
            int manaCost) 
        {
            var atk = spellEffect[1] as MagicAttack;
            SpellEffect = spellEffect;
            Range = atk.attack().Range;
            RequiredLevel = requiredLevel;
            ManaCost = manaCost;
        }
    }
}

namespace CharacterFile {
    public abstract partial class Combatant {
        public virtual void CastSpell(Spell spell) {
            if (spell.Range == ActRange.Broad) {
                foreach (Effect effect in spell.SpellEffect) {
                    foreach (Combatant enemy in Combatant.Enemy) {
                        enemy.ActiveEffects.Add(effect);
                    }
                }
            } else if (spell.Range == ActRange.Self) {
                foreach (Effect effect in spell.SpellEffect) {
                    this.ActiveEffects.Add(effect);
                }
            }
        }

        public virtual void CastSpell(Spell spell, Combatant target) {
            if (spell.Range == ActRange.Focused) {
                
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
