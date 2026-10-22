using Godot;
using CharacterFile;
using PlayerFile;
using EnemyFile;
	
public partial class MainProgram : Node2D
{
	public override void _Ready()
	{
		Combatant.Hero.Add(Player.player);
			
		Combatant.Enemy.Add(Bandit.banditOne);
		Combatant.Enemy.Add(Bandit.banditTwo);
			
		Combatant.All.AddRange(Combatant.Hero);
		Combatant.All.AddRange(Combatant.Enemy);

		GD.Print("Seus aliados são: \n");
		Combatant.Hero.ForEach(element => GD.Print($"{element.Name}."));
			
		GD.Print("\nSeus inimigos são: \n");
		Combatant.Enemy.ForEach(element => GD.Print($"{element.Name}."));
			
		GD.Print("Realize um teste de iniciativa: \n");

		foreach (Combatant element in Combatant.All)
		{
			element.IniciativeBonus += element.RollDice(1, 21);
			GD.Print($"{element.Name} tirou {element.IniciativeBonus}.");
		}

		static int OrganizeList(Combatant one, Combatant two)
		{
			return two.IniciativeBonus.CompareTo(one.IniciativeBonus);
		}
		Combatant.All.Sort(OrganizeList);
			
		GD.Print("\nA ordem será: \n");
		Combatant.All.ForEach(element => GD.Print($"{element.Name}."));
			
		GD.Print($"\nComeçando:\n");
		Combatant.All.ForEach(element => element.Stats());
			
		while (true) // Algoritmo dos turnos
		{
			Player.player.HealthCopy = Player.player.Health;
			foreach (Combatant element in Combatant.All)
			{
				if (Combatant.Hero.Contains(element))
				{
					element.HeroTurn();
				}
					
				else if (Combatant.Enemy.Contains(element))
				{
					element.EnemyTurn();
				}
					
				element.DealState();
				element.DealEffect();
				element.DealDeadCharacter(Combatant.All);
			}
				
			if (Combatant.Hero.Count == 0)
			{
				GD.Print("Inimigos venceram!"); 
				break;
			} 
			
			if (Combatant.Enemy.Count == 0)
			{
				GD.Print("Heróis venceram!");
				break;
			}
		} 
	}
}   
