using Godot;
using GenericTeammateFile;

namespace PlayerTeamManagerFile;

public partial class PlayerTeamManager : Node2D
{
    private const TeammateClass ARCHER   = TeammateClass.Archer;
    private const TeammateClass WIZARD   = TeammateClass.Wizard;
    private const TeammateClass CLERIC   = TeammateClass.Cleric;
    private const TeammateClass GADIATOR = TeammateClass.Gladiator;
    
	// Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        string _objectPathPrefix = "scenes/ObjectsAsScenes/Characters";
        string _pathToGenericTeammate = $"res://{_objectPathPrefix}/GenericPlayerTeamCharacter.tscn";

        PackedScene _genericTeammate = ResourceLoader.Load<PackedScene>(_pathToGenericTeammate);

        PackedScene[] _playerTeamScenes = [
            _genericTeammate,
            _genericTeammate,
            _genericTeammate,
            _genericTeammate,
        ];

        TeammateClass[] _playerTeamClasses = [
            ARCHER,
            WIZARD,
            CLERIC,
            GADIATOR,
        ];

        // Spread the team out in a horizontal line, 32 px apart, centred on the manager.
        float _spacing = 50.0f;
        Vector2[] _spawnOffsets = new Vector2[4];
        for (int i = 0; i < _spawnOffsets.Length; i++) {
            float _x = (i - (_spawnOffsets.Length - 1) / 2.0f) * _spacing;
            _spawnOffsets[i] = new Vector2(_x, 0);
        }

        var _team = new GenericTeammate[_playerTeamScenes.Length];

        for (int i = 0; i < _playerTeamScenes.Length; i++) {
            var _teammateNode = _playerTeamScenes[i].Instantiate<GenericTeammate>();

            _teammateNode.MateClass = _playerTeamClasses[i];
            _teammateNode.Torch = _teammateNode.MateClass == TeammateClass.Cleric;
            _teammateNode.Position = _spawnOffsets[i]; // set before AddChild so it is applied on entry

            AddChild(_teammateNode);
            _team[i] = _teammateNode;
        }

        // Once every teammate is in the tree, make them ignore each other's bodies.
        foreach (var _mate in _team) {
            foreach (var _other in _team) {
                if (_mate != _other) {
                    _mate.AddCollisionExceptionWith(_other);
                }
            }
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
