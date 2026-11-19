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
    public void Start(Combatant[] enemies, GenericTeammate contactingTeamMate) {
        Start([.. enemies, .. GetPlayerTeam(contactingTeamMate)]);
    }

    private void Start(Combatant[] combatants) {
        Node canvasManagerNode = GetTree().Root.FindChild("Camera2D");
        CameraCanvasManager canvasManager = 
            canvasManagerNode.GetNode<CameraCanvasManager>("CameraCanvasManager");
        CombatManager combatManager =    
            canvasManager.Combat.GetNode<CombatManager>("CombatManager");

        ThrowEvent(canvasManager.FadeIn);
        Battle battle = combatManager.Start(combatants);
        ThrowEvent(canvasManager.FadeOut);
        _ = battle.StartBattle();

    }

    private static MainCharacter[] GetPlayerTeam(GenericTeammate teammate) {
        List<MainCharacter> PlayerTeam = [];
        foreach (GenericTeammate tm in teammate.GetTeam()) {
            PlayerTeam.Add(tm.CharacterClass);
        }
        return [.. PlayerTeam];
    }
}
