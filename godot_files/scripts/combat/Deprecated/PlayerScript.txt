using Godot;
using PlayerFile;

public partial class PlayerScript : Node2D
{
	private Player Instance;
	private Sprite2D Sprite;
	private AnimatedSprite2D AnimatedSprite;
	private Button Button;
	private Label TextLabel;
	
	public override void _Ready()
	{
		Instance = Player.player;
		
		Sprite = GetNode<Sprite2D>("PlayerSprite");
	}
}
