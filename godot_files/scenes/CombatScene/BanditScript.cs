using Godot;
using System;
using CharacterFile;
using EnemyFile;
using EnumFile;

public partial class BanditScript : Node2D
{
	[Export] private int TypeOfBandit = 2;
	private Bandit Instance;
	private Sprite2D Sprite;
	private AnimatedSprite2D AnimatedSprite;
	private Button Button;
	private Label TextLabel;
	
	public override void _Ready()
	{
		if (TypeOfBandit == 1)
		{
			Instance = Bandit.banditOne;
		}
		
		else if (TypeOfBandit == 2)
		{
			Instance = Bandit.banditTwo;	
		}
		
		Sprite = GetNode<Sprite2D>("BanditSprite");
		AnimatedSprite = GetNode<AnimatedSprite2D>("EnemyAttack");
		Button = GetNode<Button>("EnemyButton");
		TextLabel = GetNode<Label>("EnemyTextLabel");
				
		AnimatedSprite.Visible = false;
		
		Button.Pressed += OnEnemyButtonPressed;
		Instance.EnemyAttacked += PlayAttackAnimation;
		Instance.CharacterDied += Instance.CharacterDie;
		
		TextLabel.Text = $"HP: {Instance.Health}, CA: {Instance.Armor}";
		
		if (Instance.Weapons.Contains(ActionType.BanditScimitar))
		{
			Sprite.Texture = GD.Load<Texture2D>("res://bandit_scimitar_frame.png");
		}
		
		else if (Instance.Weapons.Contains(ActionType.BanditCrossbow))
		{
			Sprite.Texture = GD.Load<Texture2D>("res://bandit_crossbow_frame.png");
		}
		
	}
	
	public void PlayAttackAnimation()
	{
		if (Instance.Weapons.Contains(ActionType.BanditScimitar))
		{
			AnimatedSprite.Visible = true;
			GD.Print("Bandido atacou com uma cimitarra.");
			AnimatedSprite.Play("bandit_scimitar.png");
			AnimatedSprite.Visible = false;
		}
		
		else if (Instance.Weapons.Contains(ActionType.BanditCrossbow))
		{
			AnimatedSprite.Visible = true;
			GD.Print("Bandido atacou com um arco.");
			AnimatedSprite.Play("bandit_crossbow.png");
			AnimatedSprite.Visible = false;
		}
	}

	public void OnEnemyButtonPressed()
	{
		TextLabel.Text = $"{Instance.Name}";
	}
}
