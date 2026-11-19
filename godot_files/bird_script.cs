
using Godot;

public partial class bird_script : CharacterBody2D
{
	[Export]
	public bool FlipRight { get; set; } = true;

	private AnimatedSprite2D _sprite;
	private PathFollow2D _pathFollow;
	private Path2D _path;

	private string _currentAnimation = "";

	public override void _Ready()
	{
		_sprite = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");

		_pathFollow = GetParent() as PathFollow2D;

		if (_pathFollow != null)
			_path = _pathFollow.GetParent() as Path2D;

		if (_sprite == null)
			GD.PushError("AnimatedSprite2D não encontrado!");

		if (_pathFollow == null)
			GD.PushError("NPC não é filho direto de PathFollow2D!");

		if (_path == null)
			GD.PushError("Path2D não encontrado como pai de PathFollow2D!");

		PlayAnimation("Front");
	}

	public override void _Process(double delta)
	{
		if (_path == null || _pathFollow == null || _sprite == null)
			return;

		Curve2D curve = _path.Curve;

		if (curve == null)
			return;

		float length = curve.GetBakedLength();

		if (length <= 0.0f)
			return;

		float currentProgress = Mathf.PosMod(
			_pathFollow.Progress,
			length
		);

		float nextProgress = Mathf.PosMod(
			currentProgress + 2.0f,
			length
		);

		Vector2 currentPosition = _path.ToGlobal(
			curve.SampleBaked(currentProgress)
		);

		Vector2 nextPosition = _path.ToGlobal(
			curve.SampleBaked(nextProgress)
		);

		Vector2 direction = (nextPosition - currentPosition).Normalized();

		if (direction.LengthSquared() < 0.01f)
			return;

		UpdateAnimation(direction);
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

		_sprite.FlipH = animation == "Right" && FlipRight;
		_sprite.FlipV = animation == "Front";

		_sprite.Play(animation);
	}
}
