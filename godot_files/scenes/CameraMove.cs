using Godot;
using System;

public partial class CameraMove : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Process(double delta)
	{
        	GlobalPosition = GetParent<Node2D>().GlobalPosition;
  }
}
