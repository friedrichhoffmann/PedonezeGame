using Godot;
using System.Linq;
using CharacterFile;
using System.Threading.Tasks;
using System.Collections.Generic;
using MainCharacterFile;
using GenericEnemy;
using Dice;

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

	    public void HeroJoinBattle(Combatant hero) {
	        Combatant.Hero.Add(hero);
	    }

	    public void EnemyJoinBattle(Combatant enemy) {
	        Combatant.Enemy.Add(enemy);
	    }

	    public void InitiativeTest() {
	        foreach (Combatant element in Combatant.All) {
	            element.InitiativeBonus += Die.Roll(1, Die.d20);
	        }

	        Combatant.All.Sort(OrganizeList);
	    }

    public void DealDeadCombatant(List<Combatant> allCombatants) {
        foreach (Combatant element in allCombatants.Where(c => c.Health <= 0).ToList()) {
            if (Combatant.Hero.Remove(element)) {
                // Herói derrotado.
            }
            else if (Combatant.Enemy.Remove(element)) {
                // todo!
                // Player.player.Xp += element.Xp;
            }

            allCombatants.Remove(element);
        }
    }

        public async Task StartBattle() {
            while (Combatant.Hero.Count > 0 && Combatant.Enemy.Count > 0) {
                foreach (Combatant element in Combatant.All.ToList()) {
                    if (Combatant.Hero.Count == 0 || Combatant.Enemy.Count == 0) { break; }

                    if (element is MainCharacter) { await (element as MainCharacter).HeroTurn(); }
                    else if (element is Enemy) { (element as Enemy).EnemyTurn(); }

                    DealDeadCombatant(Combatant.All);

                    if (Combatant.Hero.Count == 0 || Combatant.Enemy.Count == 0) { break; }

                    Combatant.DealEffect();
                }
            }

            GD.Print(Combatant.Hero.Count == 0
                ? "Os heróis foram derrotados."
                : "Os inimigos foram derrotados.");
        }
    }
}
