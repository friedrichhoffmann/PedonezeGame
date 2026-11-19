
using Godot;

public partial class GameOver : Node2D
{
	private Button _retryButton;

	public override void _Ready()
	{
		_retryButton = GetNode<Button>(
            "CanvasLayer/Control/RetryButton"
		);

		_retryButton.Pressed += OnRetryPressed;
	}

	private void OnRetryPressed()
	{
		GetTree().ChangeSceneToFile(
            "res://godot_files/mapa1.tscn"
		);
	}
}
