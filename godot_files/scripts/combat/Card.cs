using Godot;
using GenericEnemy;
using CharacterFile;
using MainCharacterFile;
using EventSystem;

using static EventSystem.EventHandler;
using static EventSystem.EventBody;

namespace CardFile {
    public abstract partial class Card : Control {
        public Label HealthLabel { get; protected set; }
        public Label CharacterName { get; protected set; }
        public Combatant ValueProvider { get; set; }
    }
}

