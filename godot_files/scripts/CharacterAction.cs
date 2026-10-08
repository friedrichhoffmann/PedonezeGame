using CharacterFile;
using EnumFile;
using PlayerFile;
using AllyFile;
using EnemyFile;
using AnimalFile;

namespace CharacterFile
{
    public abstract partial class Combatant
    {
        public int CharacterGiveDamage(int damage, Combatant target)
        {
            switch (CharacterActionType)
            {
                case ActionType.BanditScimitar:
                    DamageType = DamageType.SlashingDamage;
                    damage = RollDice(2, 8);
                    break;
                case ActionType.BanditCrossbow:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(2, 10);
                    break;
                case ActionType.TribalWarriorSpear:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(2, 8);
                    break;
                case ActionType.PlebeianStick:
                    DamageType = DamageType.BludgeoningDamage;
                    damage = RollDice(1, 5);
                    break;
                case ActionType.RatBite:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(4, 8);
                    break;
                case ActionType.BoarCharge:
                    DamageType = DamageType.BludgeoningDamage;
                    damage = RollDice(2, 8);
                    break;
                case ActionType.DeerHorn:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(4, 10);
                    break;
                case ActionType.ItalianWolfBite:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(4, 15);
                    break;
                case ActionType.BrownBearBite:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(4, 14);
                    break;
                case ActionType.BrownBearCrawl:
                    DamageType = DamageType.SlashingDamage;
                    damage = RollDice(4, 8);
                    break;
                case ActionType.BanditCaptainScimitar:
                    DamageType = DamageType.SlashingDamage;
                    damage = RollDice(1, 7);
                    break;
                case ActionType.BanditCaptainDagger:
                    DamageType = DamageType.SlashingDamage;
                    damage = RollDice(1, 5);
                    break;
                case ActionType.CentaurSpear:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(1, 9);
                    break;
                case ActionType.CentaurLongBow:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(1, 9);
                    break;
                case ActionType.BigBoarCharge:
                    DamageType = DamageType.BludgeoningDamage;
                    damage = RollMultipleDice(2, 1, 7);
                    break;
                case ActionType.BigDeerHorn:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollMultipleDice(2, 1, 7);
                    break;
                case ActionType.HippopotamusBite:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollMultipleDice(2, 1, 11);
                    break;
                case ActionType.UnicornKick:
                    DamageType = DamageType.BludgeoningDamage;
                    damage = RollMultipleDice(2, 1, 7);
                    break;
                case ActionType.UnicornHorn:
                    DamageType = DamageType.PiercingDamage;
                    damage = RollDice(1, 11);
                    break;
            }
            if (target.Effect.Contains(EffectType.Resistance))
            {
                damage -= target.RollDice(1, 5);
            }
            return damage;
        }

        public int CalculateMagicDamage(int damage, List<Combatant> list, Combatant target)
        {
            int var = 0;
            int bonus = 0;
            switch (CharacterActionType)
            {
                case ActionType.TrueStrike:
                    damage = RollDice(1, 7);
                    break;
                case ActionType.AuraOfLife:
                    
                    break;
                case ActionType.Fireball:
                    for (int a = 0; a < list.Count; a++)
                    {
                        damage = RollMultipleDice(9, 1, 7);
                        list[a].Health -= damage;
                    }

                    break;
                case ActionType.MindSpike:
                    damage = target.RollMultipleDice(3, 1, 9);
                    break;
                case ActionType.ControlFlames:
                    damage = RollDice(1, 9);
                    break;
                case ActionType.Guidance:
                    target.DamageBonus += RollDice(1, 5);
                    break;
                case ActionType.Entangle:
                    damage = RollDice(1, 7);
                    target.Effect.Add(EffectType.Baned);
                    target.EffectTurns.Add(1);
                    break;
                case ActionType.AcidArrow:
                    damage = RollMultipleDice(6, 1, 5);
                    break;
                case ActionType.FlameArrows:
                    damage = RollDice(1, 7);
                    break;
                case ActionType.SacredFlame:
                    damage += target.RollDice(1, 9);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 9);
                    break;
                case ActionType.Immolation:
                    damage = RollMultipleDice(12, 1, 7);
                    break;
                case ActionType.Investiture:
                     damage += RollMultipleDice(3, 1, 11);
                     damage += RollMultipleDice(4, 1, 9);
                     break;
                case ActionType.AcidSplash:
                    damage = RollDice(1, 7);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 7);
                    break;
                case ActionType.FireBolt:
                    damage = RollDice(1, 11);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 11);
                    break;
                case ActionType.CrusadersMantle:
                    foreach (Combatant combatant in list)
                    {
                        combatant.DamageBonus += RollDice(1, 5);
                    }
                    break;
                case ActionType.MelfsMinuteMeteors:
                    damage = RollMultipleDice(2, 1, 7);
                    break;
                case ActionType.MagicMissile:
                    damage = RollMultipleDice(3, 1, 5);
                    break;
                case ActionType.Frostbite:
                    target.Effect.Add(EffectType.Disadvantage);
                    target.EffectTurns.Add(1000);
                    damage = RollDice(1, 7);
                    break;
                case ActionType.IceStorm:
                    for (int i = 0; i < list.Count; i++)
                    {
                         damage += RollMultipleDice(4, 1, 7);
                         damage += RollMultipleDice(2, 1, 11);
                    }
                    break;
                case ActionType.CloudOfDaggers:
                    damage = RollMultipleDice(4, 1, 5);
                    break;
                case ActionType.ThunderWaves:
                    damage = RollMultipleDice(2, 1, 9);
                    break;
                case ActionType.DimensionDoor:
                    damage = RollMultipleDice(4, 1, 7);
                    break;
                case ActionType.Shatter:
                    damage = target.RollMultipleDice(3, 1, 9); 
                    break;
                case ActionType.RayOfSickness:
                    var = target.rng.Next(1, 21);
                    if (var < 10) break;
                    damage = target.rng.Next(1, 9);
                    break;
                case ActionType.RayOfFrost:
                    damage = RollDice(1, 9);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 9);
                    break;
                case ActionType.Dawn:
                    damage = RollMultipleDice(8, 1, 7);
                    break;
                case ActionType.ScorchingRay:
                    damage = RollMultipleDice(2, 1, 7);
                    break;
                case ActionType.EldritchBlast:
                    damage = target.rng.Next(1, 11);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 11);
                    break;
                case ActionType.HellishRebuke:
                    damage = RollMultipleDice(2, 1, 11);
                    break;
                case ActionType.PoisonSpray:
                    damage = RollDice(1, 13);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 13);
                    break;
                case ActionType.BeaconOfHope:

                    break;
                case ActionType.ShockingGrasp:
                    damage = RollDice(1, 9);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 9);
                    break;
                case ActionType.EarthTremor:
                    var = RollDice(1, 21);
                    if (var <= 10)
                    {
                        target.State.Add(StateType.Paralysed);
                    }
                    damage = RollDice(1, 7);
                    break;
                case ActionType.Thunderclap:
                    damage = RollDice(1, 7);
                    break;
                case ActionType.ThunderStep:
                    target.Health += RollMultipleDice(3, 1, 11);
                    break;
                case ActionType.ViciousMockery:
                    damage = RollDice(1, 7);
                    bonus = RollMultipleDice(MainCharacter.Level / 5, 1, 9);
                    target.Effect.Add(EffectType.Disadvantage);
                    target.EffectTurns.Add(1000);
                    break;
            }

            return damage + bonus;
        }

        public int CalculateMagicHeal(int health, List<Combatant> list, Combatant target)
        {
            switch (CharacterActionType)
            {
                case ActionType.Aid:
                    foreach (Combatant combatant in list)
                    {
                        if (combatant is not Cleric)
                        {
                            combatant.Health += 5;
                        }
                    }
                    break;
                case ActionType.GoodBerry:
                    target.Health += 1;
                    break;
                case ActionType.AuraOfVitality:
                    foreach (Combatant combatant in list)
                    {
                        health += RollMultipleDice(2, 1, 7);
                    }
                    break;
                case ActionType.HealingWord:
                    target.Health += RollDice(1, 7);
                    break;
                case ActionType.CureWounds:
                    target.Health += RollDice(1, 9) + 3;
                    break;
                case ActionType.Cure:
                    target.Health += 70;
                    target.State.Clear();
                    break;
                case ActionType.MassCureWounds:
                    target.Health += RollMultipleDice(2, 1, 9) + 3;
                    break;
                case ActionType.Heroism:
                    target.Health += 3;
                    break;
                case ActionType.MassHealingWord:
                    foreach (Combatant combatant in list)
                    {
                        combatant.Health += RollMultipleDice(5, 1, 9);
                    }
                    break;
                case ActionType.MassCureWord:
                    foreach (Combatant combatant in list)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            combatant.Health += RollDice(1, 9);
                        }
                    }
                    break;
                case ActionType.TrueSeeing:
                    target.Health += RollMultipleDice(2, 1, 11);
                    break;
            }
            return  target.Health;
        }

        public void GiveMagicEffect(List<Combatant> list, Combatant target)
        {
            switch (CharacterActionType)
            {
                case ActionType.TashasHideousLaugh:
                    for (int i = 0; i < target.State.Count; i++)
                    {
                        if (target.State[i] == StateType.Incapacitated)
                        {
                            target.State.RemoveAt(i);
                            target.StateTurns.RemoveAt(i);
                        }
                    }
                    break;
                case ActionType.MageArmor:
                    target.Effect.Add(EffectType.MagicArmor);
                    target.EffectTurns.Add(1000);
                    break;
                case ActionType.EnsnaringStrike:
                    target.Effect.Add(EffectType.Baned);
                    target.EffectTurns.Add(1);
                    break;
                case ActionType.Banishment:
                    target.Effect.Add(EffectType.Baned);
                    target.EffectTurns.Add(1);
                    break;
                case ActionType.Sleep:
                    int var = target.RollDice(1, 21);
                    if (var >= 10) break;
                    target.Effect.Add(EffectType.Baned);
                    target.EffectTurns.Add(2);
                    break;
                case ActionType.ShieldOfFaith:
                    target.Effect.Add(EffectType.FaithShield);
                    target.EffectTurns.Add(1000);
                    break;
                case ActionType.FireShield:
                    target.Effect.Add(EffectType.FireShield);
                    target.EffectTurns.Add(1000);
                    break;
                case ActionType.GreaterRestoration:
                    target.Effect.Clear();
                    target.EffectTurns.Clear();
                    break;
                case ActionType.Barkskin:
                    target.Effect.Add(EffectType.Barkskin);
                    target.EffectTurns.Add(1);
                    break;
                case ActionType.LesserRestoration:
                    for (int i = 0; i < target.State.Count; i++)
                    {
                        if (target.State[i] == StateType.Poisoned)
                        {
                            target.State.RemoveAt(i);
                            target.StateTurns.RemoveAt(i);
                        }

                        if (target.State[i] == StateType.Paralysed)
                        {
                            target.State.RemoveAt(i);
                            target.StateTurns.RemoveAt(i);
                        }
                    }
                    break;
                case ActionType.Poison:
                    for (int i = 0; i < target.State.Count; i++)
                    {
                        if (target.State[i] == StateType.Poisoned)
                        {
                            target.State.RemoveAt(i);
                            target.StateTurns.RemoveAt(i);
                        }
                    }
                    break;
                case ActionType.CharmMonster:
                    ///////
                    break;
                case ActionType.DeathWard:
                    foreach (Combatant element in list)
                    {
                        element.Effect.Add(EffectType.DeathAward);
                        element.EffectTurns.Add(1000);
                    }
                    break;
                case ActionType.BladeWard:
                    Effect.Add(EffectType.AllResistance);
                    EffectTurns.Add(1);
                    break;
                case ActionType.ProtectionFromWind:
                    for (int i = 0; i < target.State.Count - 1; i++)
                    {
                        if (target.State[i] == StateType.Poisoned)
                        {
                            target.State.RemoveAt(i);
                            target.Effect.RemoveAt(i);
                        }
                    }
                    break;
                case ActionType.Resistance:
                    target.Effect.Add(EffectType.Resistance);
                    target.EffectTurns.Add(1000);
                    break;
                case ActionType.HoldPerson:
                    if (target.RollDice(1, 21) > 10) break;
                    target.State.Add(StateType.Paralysed);
                    target.StateTurns.Add(1000);
                    break;
                case ActionType.Catnap:
                    target.State.Add(StateType.Paralysed);
                    target.StateTurns.Add(1000);
                    break;
            }
        }

        /* public void Habilities()
        {
            switch (CharacterActionType)
            {
                case 

                    break;
            }
        }*/
    }
}