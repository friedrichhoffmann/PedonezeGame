using Godot;
using MainCharacterFile;
using GenericEnemy;
using EnemyFile;
using AllyFile;
using CardFile;

namespace SpawnCardsFile
{
    public partial class SpawnCards : Control
    {
        private static HBoxContainer enemyField;
        private static HBoxContainer playerCardRow;
        public  static Control PlayerRowRootNode { get; set; }
        public  static Control AttackSetsRootNode { get; set; }
        public  static Control EnemyFieldRootNode { get; set; }
        public  static PackedScene GenericPlayerTeamCardScene { get; set; }

        public override void _Ready()
        {
            PlayerRowRootNode = GetNode<Control>(
                "/root/Control/CombatManager/UI/PlayerCardsRoot");
            
            EnemyFieldRootNode = GetNode<Control>(
                "/root/Control/CombatManager/UI/EnemyFieldRoot");

            AttackSetsRootNode = GetNode<Control>(
                "/root/Control/CombatManager/AttackSets");

            GenericPlayerTeamCardScene =
                GD.Load<PackedScene>(ProjectSettings.GlobalizePath("res://PlayerTeamCard.tscn"));
        }

        public static void SpawnEnemyCards(Enemy[] enemyTeam) {
            if (EnemyFieldRootNode == null) {
                GD.PushError("HUD root or player card scene is not initialized.");
                return;
            }

            if (enemyField == null)
            {
                enemyField = new HBoxContainer();
                enemyField.SetAnchorsPreset(Control.LayoutPreset.CenterTop);
                enemyField.OffsetBottom = 300;
                // enemyField.OffsetRight = -20;
                enemyField.GrowHorizontal = Control.GrowDirection.Both; // or End depending on your setup
                enemyField.Alignment = BoxContainer.AlignmentMode.Center;    // hug right side
                enemyField.AddThemeConstantOverride("separation", 280);
                EnemyFieldRootNode.AddChild(enemyField);
            }

            foreach (Enemy enemy in enemyTeam) {
                string enemyCardScenePath = enemy switch {
                    Bandit _ => "res://scenes/ObjectsAsScenes/Cards/Enemies/ThiefCard.tscn",
                    BrownBear _ => "res://scenes/ObjectsAsScenes/Cards/Enemies/BearCard.tscn",
                    _ => "res://scenes/ObjectsAsScenes/Cards/Enemies/ThiefCard.tscn"
                };

                string enemyFacePath = enemy switch {
                    Bandit _ => "res://assets/char/enemys/InCombatEnemys/Thief/ThiefStatic.png",
                    BrownBear _ => "res://assets/char/enemys/InCombatEnemys/Bear/BearStatic.png",
                    _ => ""
                };


                PackedScene enemyCardScene = 
                    GD.Load<PackedScene>(ProjectSettings.GlobalizePath(enemyCardScenePath));

                Control cardScene = enemyCardScene.Instantiate<Control>();
                enemyField.AddChild(cardScene);

                EnemyCard enemyCard = cardScene.GetNode<EnemyCard>("EnemyCard");
                AnimatedSprite2D enemyAttackSet = enemyCard.GetNode<AnimatedSprite2D>("AttackSet");

                Control attackSetParentNode = new Control();
                attackSetParentNode.Name = $"{enemy.Name.Split('1')[0]}";

                AttackSetsRootNode.AddChild(attackSetParentNode);
                enemyAttackSet.Reparent(attackSetParentNode, true);

                enemyCard.SetCharacterFaceImage(ProjectSettings.GlobalizePath(enemyFacePath));
                enemyCard.UpdateValues(enemy);

                enemy.AttackSetAnimation = enemyAttackSet;
                enemy.Card = enemyCard;
            } 
        }

        public static void SpawnPlayerCards(MainCharacter[] playerTeam)
        {
            if (PlayerRowRootNode == null || GenericPlayerTeamCardScene == null)
            {
                GD.PushError("HUD root or player card scene is not initialized.");
                return;
            }

            if (playerCardRow == null)
            {
                playerCardRow = new HBoxContainer();
                playerCardRow.SetAnchorsPreset(Control.LayoutPreset.TopRight);
                playerCardRow.OffsetRight = -20;
                playerCardRow.OffsetBottom = 30;
                playerCardRow.GrowHorizontal = Control.GrowDirection.Begin; // or End depending on your setup
                playerCardRow.Alignment = BoxContainer.AlignmentMode.End;    // hug right side
                playerCardRow.AddThemeConstantOverride("separation", 185);
                PlayerRowRootNode.AddChild(playerCardRow);
            }

            foreach (MainCharacter character in playerTeam)
            {
                Control cardScene = GenericPlayerTeamCardScene.Instantiate<Control>();
                playerCardRow.AddChild(cardScene);

                string characterFacePath = character switch {
                    Gladiator _ => "res://assets/char/player_team/InBattle/FaceGladiator.png",
                    Cleric _ => "res://assets/char/player_team/InBattle/FaceCleric.png",
                    Archer _ => "res://assets/char/player_team/InBattle/FaceArcher.png", 
                    Wizard _ => "res://assets/char/player_team/InBattle/FaceWizard.png",
                    _ => ""
                };


                MainCharacterCard characterCard = cardScene.GetNode<MainCharacterCard>("CharacterCard");
                characterCard.SetCharacterFaceImage(ProjectSettings.GlobalizePath(characterFacePath));
                characterCard.UpdateValues(character);

                character.Card = characterCard;
            }
        }

    }
}

