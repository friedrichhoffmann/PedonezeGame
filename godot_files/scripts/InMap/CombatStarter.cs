using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

using CombatAPI;
using CombatManagerFile;

using EnemyFile;
using CharacterFile;
using MainCharacterFile;

using GenericTeammateFile;

using EventSystem;
using static EventSystem.EventHandler;

namespace CombatStarterFile;

public partial class CombatStarter : Node2D {
	public Battle Start(Combatant[] enemies, GenericTeammate contactingTeamMate) {
        GD.Print("Ran start");
		return Start([.. enemies, .. GetPlayerTeam(contactingTeamMate)]);
	}

    private Battle Start(Combatant[] combatants) {
        var cameraManagerNode = GetTree().Root.FindChild("Camera2D", true, false);
        GD.Print("Camera2D found: ", cameraManagerNode != null);

        var canvasManagerNode = cameraManagerNode?.GetNodeOrNull<CameraCanvasManager>("CameraCanvasManager");
        GD.Print("CanvasCanvasManager found: ", canvasManagerNode != null);

        var combatNode = canvasManagerNode?.Combat;
        GD.Print("Combat control assigned: ", combatNode != null);

        var combatManager = combatNode?.GetNodeOrNull<CombatManager>("CombatManager");
        GD.Print("CombatManager found: ", combatManager != null);

        if (combatManager == null)
            throw new InvalidOperationException("Could not find the CombatManager node.");

        return combatManager.Start(combatants);
    }

	private static MainCharacter[] GetPlayerTeam(GenericTeammate teammate) {
		List<MainCharacter> PlayerTeam = [];
		foreach (GenericTeammate tm in teammate.GetTeam()) {
			PlayerTeam.Add(tm.CharacterClass);
		}
		return [.. PlayerTeam];
	}
}
