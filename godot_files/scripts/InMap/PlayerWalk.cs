using System;
using Godot;

public partial class PlayerWalk : CharacterBody2D
{

    public bool InBattle { get; set; }

	private AnimatedSprite2D animSprite;

	public override void _Ready()
	{
		animSprite = FindChild("AnimatedSprite2D", recursive: true) as AnimatedSprite2D;

		if (animSprite == null)
			GD.PushError("AnimatedSprite2D wasn't found under the player.");
	}

	public override void _PhysicsProcess(double delta)
    {
    }
}
