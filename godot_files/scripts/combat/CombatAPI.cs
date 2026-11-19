using Godot;
using System.Linq;
using CharacterFile;
using System.Threading.Tasks;
using MainCharacterFile;
using GenericEnemy;
using EventSystem;
using MagicSystem;
using Dice;


using static CombatManagerFile.CombatManager;
using static System.Diagnostics.Debug;
using static EventSystem.EventHandler;

namespace CombatAPI {
    enum Actions {
        PhyAttack,
        MgcAttack,
        Inventory,    		
    }

    public class Battle {
        private int OrganizeList(Combatant one, Combatant two) {
            return two.InitiativeBonus.CompareTo(one.InitiativeBonus);
        }

        public void InitiativeTest() {
            foreach (Combatant element in Combatant.All) {
                element.InitiativeBonus += Die.Roll(1, Die.d20);
            }

            Combatant.All.Sort(OrganizeList);
        }

        public bool IsPreviousAdversary(Combatant comb) {
            var currentIndex = Combatant.All.IndexOf(comb);
            var previousIndex = currentIndex -1;

            var previousComb = (previousIndex >= 0) ?
                Combatant.All[previousIndex] :
                Combatant.All.Last();

            if (comb is MainCharacter) return previousComb is Enemy;
            else if (comb is Enemy) return previousComb is MainCharacter;
            else Assert(false); return false; // this line never runs 
        }

        public async Task<bool> StartBattle() {
            bool menuVisible = false;

            while (
                    Combatant.All.Any(comb => comb is MainCharacter) &&
                    Combatant.All.Any(comb => comb is Enemy)
                  ) {
                foreach (Combatant element in Combatant.All.ToList()) {
                    if (!Combatant.All.Any(comb => comb is Enemy)) break;
                    if (!Combatant.All.Any(comb => comb is MainCharacter)) break;

                    // The combatant may have been removed earlier in this round.
                    if (!Combatant.All.Contains(element)) continue;

                    GD.Print($"Turn Of {element.Name}");

                    if (!element.ValidTurn()) continue;

                    if (element is MainCharacter mainCharacter) {
                        if (!menuVisible) {
                            ThrowEvent(new Event("Slides Menu In", EventKind.Animation,
                                        EventBody.Animation(animationManager, "MenuSlideIn")));
                            menuVisible = true;
                        }

                        await mainCharacter.HeroTurn();
                    }
                    else if (element is Enemy enemy) {
                        if (menuVisible) {
                            ThrowEvent(new Event("Slides Menu Out", EventKind.Animation,
                                        EventBody.Animation(animationManager, "MenuSlideOut")));
                            menuVisible = false;
                        }

                        enemy.EnemyTurn();
                    }

                    foreach (Combatant comb in Combatant.All.ToList()) {
                        if (!Combatant.All.Contains(comb)) continue;

                        if (comb is Enemy enemyComb)
                            enemyComb.Card.UpdateValues(enemyComb);
                        else if (comb is MainCharacter mainComb)
                            mainComb.Card.UpdateValues(mainComb);

                        if (comb.Health <= 0) {
                            Combatant.All.Remove(comb);
                            continue;
                        }

                        foreach (Effect effect in comb.ActiveEffects.ToList()) {
                            if (effect.Lasting <= 0) {
                                comb.ActiveEffects.Remove(effect);
                            }
                            else {
                                effect.ApplyEffect(comb);
                                effect.Lasting--;
                            }
                        }
                    }

                    if (!Combatant.All.Any(comb => comb is Enemy)) break;
                    if (!Combatant.All.Any(comb => comb is MainCharacter)) break;
                }
            }

            return Combatant.All.Any(comb => comb is MainCharacter);
        }
    }
}
