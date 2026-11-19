
using Godot;

public partial class Button1 : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		GetTree().ChangeSceneToFile(
            "res://menu.tscn"
		);
	}
}
