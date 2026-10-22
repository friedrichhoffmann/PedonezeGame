using AnimalFile;
using EnumFile;
using Godot;

public partial class BearScript : Node2D
{
	private BrownBear Instance;
	private Sprite2D Sprite;
	private AnimatedSprite2D AnimatedSprite;
	private Button Button;
	private Label TextLabel;
	
	public override void _Ready()
	{
		Instance = BrownBear.bear;
		
		Sprite = GetNode<Sprite2D>("BearSprite");
		
		Button.Pressed += OnEnemyButtonPressed;
		Instance.EnemyAttacked += PlayAttackAnimation;
		Instance.CharacterDied += Instance.CharacterDie;
		
		TextLabel.Text = $"HP: {Instance.Health}, CA: {Instance.Armor}";
		
		Sprite.Texture = GD.Load<Texture2D>("res://bear_frame.png");
	}
	
	public void PlayAttackAnimation()
	{
		if (Instance.Weapons.Count == 1)
		{
			if (Instance.Weapons.Contains(ActionType.BrownBearBite))
			{
				AnimatedSprite.Visible = true;
				GD.Print("Urso-Pardo-Misciano mordeu.");
				// AnimatedSprite.Play("bear_bite.png");
				AnimatedSprite.Visible = false;
			}
			
			else if (Instance.Weapons.Contains(ActionType.BrownBearCrawl))
			{
				AnimatedSprite.Visible = true;
				GD.Print("Urso-Pardo-Misciano arranhou.");
				AnimatedSprite.Play("bear_crawl.png");
				AnimatedSprite.Visible = false;
			}
		}

		else
		{
			AnimatedSprite.Visible = true;
			GD.Print("Urso-Pardo-Misciano mordeu.");
			AnimatedSprite.Play("bear_bite.png");
			AnimatedSprite.Visible = false;
			AnimatedSprite.Visible = true;
			GD.Print("Urso-Pardo-Misciano arranhou.");
			AnimatedSprite.Play("bear_crawl.png");
			AnimatedSprite.Visible = false;
		}
	}
	
	public void OnEnemyButtonPressed() 
	{
		TextLabel.Text = $"{Instance.Name}";
	}
}
