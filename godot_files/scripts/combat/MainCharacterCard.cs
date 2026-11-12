using Godot;
using MainCharacterFile;
using EventSystem;

using static EventSystem.EventHandler;
using static EventSystem.EventBody;

namespace CardFile {
    public partial class MainCharacterCard : Card {
        public Label ManaLabel { get; private set; }
        public Label DamageLabel { get; private set; }
        public AnimationPlayer Anim { get; private set; }
        public Panel CharacterFacePanel { get; private set; }
        private Image characterFaceImage;

        public override void _Ready() {
            CharacterName      = GetNode<Label>("statsPanel/labelHolder/name");
            HealthLabel        = GetNode<Label>("statsPanel/values/hpValue");
            ManaLabel          = GetNode<Label>("statsPanel/values/manaValue");

            Anim               = GetNode<AnimationPlayer>("AnimationPlayer");
            DamageLabel        = GetNode<Label>("DamageLabel");
            CharacterFacePanel = GetNode<Panel>("head");
        }

        public void SetCharacterFaceImage(string path) =>
            characterFaceImage = Image.LoadFromFile(path);

        public  void UpdateValues(MainCharacter mainCharacter) {
                ThrowEvent(new Event("Sets The Label <Name>", EventKind.Label,
                    Label(CharacterName, mainCharacter.Name)));
                ThrowEvent(new Event("Sets The Image <CharacterFace>", EventKind.ImageUpdate,
                    ImageUpdate(CharacterFacePanel, characterFaceImage, "ClericFace")));
                ThrowEvent(new Event("Sets The Label <Mana>", EventKind.Label,
                    Label(ManaLabel,$"{mainCharacter.Mana}/{mainCharacter.manaMax}")));
                ThrowEvent(new Event("Sets The Label <Health>", EventKind.Label,
                    Label(HealthLabel,$"{mainCharacter.Health}/{mainCharacter.healthMax}")));
        }
    }
}

