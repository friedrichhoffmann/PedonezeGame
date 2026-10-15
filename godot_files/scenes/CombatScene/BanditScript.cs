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

        else
        {
            Instance = Bandit.banditTwo;	
        }
		
        Sprite = GetNode<Sprite2D>("BanditSprite");
        AnimatedSprite = GetNode<AnimatedSprite2D>("EnemyAttack");
        Button = GetNode<Button>("EnemyButton");
        TextLabel = GetNode<Label>("EnemyTextLabel");

        AnimatedSprite.Visible = false;
		
        Button.Pressed += OnEnemyButton1Pressed;
        Instance.EnemyAttacked += PlayAttackAniamtion;
		
        TextLabel.Text = $"HP: {Instance.Health}, CA: {Instance.Armor}";
		
        if (Instance.Weapons.Contains(ActionType.BanditScimitar))
        {
            Sprite.Texture = GD.Load<Texture2D>("res://");
        }
		
        else if (Instance.Weapons.Contains(ActionType.BanditCrossbow))
        {
            Sprite.Texture = GD.Load<Texture2D>("res://bandit_crossbow_frame");
        }
		
    }

    public void PlayAttackAniamtion()
    {
        if (Instance.Weapons.Contains(ActionType.BanditScimitar))
        {
            AnimatedSprite.Visible = true;
            GD.Print("Bandido atacou com uma cimitarra.");
            AnimatedSprite.Play("bandit_scimitar");
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

    public void OnEnemyButton1Pressed() 
    {
        TextLabel.Text = $"{Instance.Name}";
    }
}