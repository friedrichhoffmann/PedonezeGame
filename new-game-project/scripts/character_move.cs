using Godot;
using System;

public partial class character_move : CharacterBody2D
{
	private AnimatedSprite2D anim_sprite;
	
	[Export]
	public float speed = 200;  // pixels per second

	public override void _Ready() {
		anim_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta) {
		Vector2 input_direction = Vector2.Zero;
		string animation = "default";
		bool flip = false;

		// Gather input
		if (Input.IsActionPressed("ui_right")) {
			input_direction.X += 1;
			animation = "h_walk";
			flip = true;
		}
		if (Input.IsActionPressed("ui_left")) {
			input_direction.X -= 1;
			animation = "h_walk";
			flip = false;
		}
		if (Input.IsActionPressed("ui_down")) {
			input_direction.Y += 1;
			animation = "down_walk";
		}
		if (Input.IsActionPressed("ui_up")) {
			input_direction.Y -= 1;
			animation = "up_walk";
		}

		// Apply movement
		Velocity = input_direction.Normalized() * speed;
		MoveAndCollide(Velocity * (float)delta);

		// Update animation
		anim_sprite.Play(animation);
		anim_sprite.FlipH = flip;
	}
}

