using Godot;

public partial class NpcPath : PathFollow2D
{
	[Export]
	public float Speed { get; set; } = 60f;

	private float _direction = 1f;
	private float _pathLength;

	public override void _Ready()
	{
		_pathLength = GetParent<Path2D>().Curve.GetBakedLength();
	}

	public override void _Process(double delta)
	{
		Progress += Speed * _direction * (float)delta;

		if (Progress >= _pathLength)
		{
			Progress = _pathLength;
			_direction = -1f;
		}
		else if (Progress <= 0)
		{
			Progress = 0;
			_direction = 1f;
		}
	}
}
