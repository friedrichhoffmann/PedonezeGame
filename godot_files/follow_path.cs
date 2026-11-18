using Godot;

public partial class follow_path : Path2D
{
	[Export]
	public float Speed { get; set; } = 80.0f;

	[Export]
	public float DistanceBetweenNpcs { get; set; } = 100.0f;

	private PathFollow2D _leader;
	private PathFollow2D _follower;

	private float _pathLength;

	public override void _Ready()
	{
		_leader = GetNodeOrNull<PathFollow2D>("PathFollow2D");
		_follower = GetNodeOrNull<PathFollow2D>("PathFollow2D2");

		if (_leader == null)
		{
			GD.PrintErr("PathFollow2D principal não encontrado!");
			SetProcess(false);
			return;
		}

		if (Curve == null)
		{
			GD.PrintErr("Path2D não possui uma Curve2D!");
			SetProcess(false);
			return;
		}

		_pathLength = Curve.GetBakedLength();

		if (_pathLength <= 0.0f)
		{
			GD.PrintErr("A curva não possui comprimento válido!");
			SetProcess(false);
			return;
		}

		_leader.Loop = true;
		_leader.Rotates = false;
		_leader.Progress = 0.0f;

		if (_follower != null)
		{
			_follower.Loop = true;
			_follower.Rotates = false;

			_follower.Progress = Mathf.PosMod(
				-DistanceBetweenNpcs,
				_pathLength
			);
		}
	}

	public override void _Process(double delta)
	{
		if (_leader == null || _pathLength <= 0.0f)
			return;

		_leader.Progress = Mathf.PosMod(
			_leader.Progress + Speed * (float)delta,
			_pathLength
		);

		if (_follower != null)
		{
			_follower.Progress = Mathf.PosMod(
				_leader.Progress - DistanceBetweenNpcs,
				_pathLength
			);
		}
	}
}
