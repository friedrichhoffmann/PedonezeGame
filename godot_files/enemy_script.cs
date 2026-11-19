using CombatStarterFile;
using GenericTeammateFile;
using Godot;

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
	private CharacterBody2D _player;

	private EnemyState _state = EnemyState.Idle;
	private string _currentAnimation = "";

	public override void _Ready()
	{
		_detectionArea = GetNode<Area2D>("DetectionArea");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		_detectionArea.BodyEntered += OnBodyEntered;
		_sprite.AnimationFinished += OnAnimationFinished;

		PlayAnimation("Idle");
	}

	private void OnBodyEntered(Node2D body)
	{
		if (!body.IsInGroup("PlayerTeam"))
			return;

		if (_state != EnemyState.Idle)
			return;

		if (body is not CharacterBody2D player)
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
		{
			ChangeToBattle();
		}
	}

	private void ChangeToBattle()
	{
        Node _combatStarterNode = GetTree().Root.FindChild("CombatStarter");
        CombatStarter _combatStarter = _combatStarterNode.GetNode<CombatStarter>("CombatStarter");
        GenericTeammate _targetTeammate = _player.GetNode<GenericTeammate>("GenericTeammate");
        _combatStarter.Start([banditOne()], _targetTeammate);
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
