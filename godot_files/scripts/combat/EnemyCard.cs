using Godot;
using GenericEnemy;
using EventSystem;

using static EventSystem.EventHandler;
using static EventSystem.EventBody;

namespace CardFile {
    public partial class EnemyCard : Card {
        public Panel CharacterFacePanel { get; private set; }
        public Label DamageLabel { get; private set; }
        public AnimationPlayer Anim { get; private set; }
        private Image characterFaceImage;

        public override void _Ready() {
            CharacterName      = GetNode<Label>("statsPanel/labelHolder/name");
            HealthLabel        = GetNode<Label>("statsPanel/values/hpValue");
            Anim               = GetNode<AnimationPlayer>("AnimationPlayer");
            DamageLabel        = GetNode<Label>("DamageLabel");
            CharacterFacePanel = GetNode<Panel>("head");
        }

        public void SetCharacterFaceImage(string path) =>
            characterFaceImage = Image.LoadFromFile(path);

        public  void UpdateValues(Enemy enemy) {
                ThrowEvent(new Event("Sets The Label <Name>", EventKind.Label,
                    Label(CharacterName, enemy.Name)));
                ThrowEvent(new Event("Sets The Image <CharacterFace>", EventKind.ImageUpdate,
                    ImageUpdate(CharacterFacePanel, characterFaceImage, "ClericFace")));
                ThrowEvent(new Event("Sets The Label <Health>", EventKind.Label,
                    Label(HealthLabel,$"{enemy.Health}/{enemy.healthMax}")));
        }
    }
}

