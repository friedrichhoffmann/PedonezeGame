using Godot;
using System;

public partial class character_move : CharacterBody2D
{
	private AnimatedSprite2D animSprite;

	public override void _Ready() {
		animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta) {
		if (Input.IsActionPressed("ui_right")) {
			animSprite.Play("h_walk");
			animSprite.FlipH = true;
		} else if (Input.IsActionPressed("ui_left")) {
			animSprite.Play("h_walk");
			animSprite.FlipH = false;
		} else if (Input.IsActionPressed("ui_up")) {
			animSprite.Play("up_walk");
			animSprite.FlipH = false;
		} else if (Input.IsActionPressed("ui_down")) {
			animSprite.Play("down_walk");
			animSprite.FlipH = false;
		} else {
			animSprite.Play("default");
			animSprite.FlipH = false;
		}
	}
}
