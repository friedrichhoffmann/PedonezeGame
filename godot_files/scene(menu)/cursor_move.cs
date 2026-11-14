using Godot;
using System;

public partial class cursor_move : Node2D
{
	private Sprite2D cursor;
	
	public override void _Ready()
	{
		cursor = GetNode<Sprite2D>("Sprite2D");
		
		Input.MouseMode = Input.MouseModeEnum.Hidden;
	}
	
	public override void _Process(double delta)
	{
		cursor.GlobalPosition = GetGlobalMousePosition();
	}
}
