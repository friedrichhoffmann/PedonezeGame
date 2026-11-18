using Godot;

public partial class GameCamera : Camera3D
{
	public override void _Ready()
	{
		MakeCurrent();
	}
}
