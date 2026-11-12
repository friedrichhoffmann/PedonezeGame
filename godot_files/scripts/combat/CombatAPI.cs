using Godot;
using System.Linq;
using CharacterFile;
using System.Threading.Tasks;
using MainCharacterFile;
using GenericEnemy;
using EventSystem;
using System.Linq;
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

	class Battle {
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

        public async Task StartBattle() {
            bool isFirstRound = true;
            while (
                    Combatant.All.Where(comb => comb is MainCharacter).Count() > 0 && 
                    Combatant.All.Where(comb => comb is Enemy).Count() > 0
            )   {
                foreach (Combatant element in Combatant.All.ToList()) {
                    // i could have used or, but i really did not wish to make this only one gigantic if statement
                    if (Combatant.All.Where(comb => comb is Enemy).Count() == 0)         break;
                    if (Combatant.All.Where(comb => comb is MainCharacter).Count() == 0) break;

                    if (element is MainCharacter mainCharacter) {
                        if (IsPreviousAdversary(mainCharacter) || isFirstRound)
                            ThrowEvent(new Event("Slides Menu In", EventKind.Animation,
                                EventBody.Animation(animationManager, "MenuSlideIn")));
                        await mainCharacter.HeroTurn();
                    }

                    if (Combatant.All.Where(comb => comb is Enemy).Count() == 0)         break;
                    if (Combatant.All.Where(comb => comb is MainCharacter).Count() == 0) break;

                    else if (element is Enemy enemy) {
                        if (IsPreviousAdversary(enemy))
                            ThrowEvent(new Event("Slides Menu Out", EventKind.Animation,
                                EventBody.Animation(animationManager, "MenuSlideOut")));
                        enemy.EnemyTurn();
                    }

                    if (Combatant.All.Where(comb => comb is Enemy).Count() == 0)         break;
                    if (Combatant.All.Where(comb => comb is MainCharacter).Count() == 0) break;

                    Combatant.All.RemoveAll(elements => elements.Health == 0);
                    Combatant.DealEffect();
                    isFirstRound = false;
                }
            }

            GD.Print(Combatant.All.Where(comb => comb is MainCharacter).Count() == 0
                ? "Os heróis foram derrotados."
                : "Os inimigos foram derrotados.");
        }
    }
}
