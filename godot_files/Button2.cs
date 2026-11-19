
using Godot;

public partial class RetryButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		GetTree().ChangeSceneToFile(
            "res://godot_files/mapa1.tscn"
		);
	}
}
