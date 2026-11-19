
using Godot;
using GenericTeammateFile;
using CombatStarterFile;

using static PreMadeEnemies.Enemies;

public partial class enemy_script : CharacterBody2D
{
	private enum EnemyState
	{
		Idle,
		Chasing,
		Fighting
	}

	[Export]
	public float Speed { get; set; } = 90.0f;

	private Area2D _detectionArea;
	private AnimatedSprite2D _sprite;
	private GenericTeammate _player;

	private EnemyState _state = EnemyState.Idle;
	private string _currentAnimation = "";

	public override void _Ready()
	{
		_detectionArea = GetNode<Area2D>("DetectionArea");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		_detectionArea.CollisionMask |= 128;
		CollisionMask |= 128;

		_detectionArea.BodyEntered += OnBodyEntered;
		_sprite.AnimationFinished += OnAnimationFinished;

		PlayAnimation("Idle");
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is not GenericTeammate player)
			return;

		if (_state != EnemyState.Idle)
			return;

		_player = player;

		_player.SetPhysicsProcess(false);

		_state = EnemyState.Chasing;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_state == EnemyState.Idle ||
			_state == EnemyState.Fighting ||
			_player == null)
		{
			return;
		}

		if (!GodotObject.IsInstanceValid(_player))
			return;

		Vector2 direction = GlobalPosition.DirectionTo(
			_player.GlobalPosition
		);

		Velocity = direction * Speed;

		MoveAndSlide();

		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);

			if (collision.GetCollider() == _player)
			{
				StartFight();
				return;
			}
		}

		UpdateAnimation(direction);
	}

	private void StartFight()
	{
		_state = EnemyState.Fighting;

		Velocity = Vector2.Zero;

		PlayAnimation("fight");
	}

	private void OnAnimationFinished()
	{
		if (_currentAnimation == "fight")
			ChangeToBattle();
	}

	private void ChangeToBattle()
	{
		Node managerNode = GetTree().Root.FindChild(
			"CameraCanvasManager",
			true,
			false
		);

		if (managerNode is not CameraCanvasManager cameraManager)
		{
			GD.PushError("CameraCanvasManager não encontrado.");
			return;
		}
        var combatStarterNode = GetTree().Root.FindChild(
                "CombatStarter",
                recursive: true,
                owned: false
                );

        if (combatStarterNode is not CombatStarter combatStarter) {
            GD.PushError("CombatStarter node not found or doesn't have the CombatStarter script.");
            return;
        }

        var battle = combatStarter.Start([banditOne()], _player);
        cameraManager.ShowCombat(battle);

	}

	private void UpdateAnimation(Vector2 direction)
	{
		string animation;

		if (Mathf.Abs(direction.X) > Mathf.Abs(direction.Y))
		{
			animation = direction.X < 0 ? "Left" : "Right";
		}
		else
		{
			animation = direction.Y < 0 ? "Back" : "Front";
		}

		PlayAnimation(animation);
	}

	private void PlayAnimation(string animation)
	{
		if (_currentAnimation == animation)
			return;

		_currentAnimation = animation;

		_sprite.Play(animation);
	}
}
