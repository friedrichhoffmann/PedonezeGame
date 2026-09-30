using Godot;
using System;

public partial class following_camera : Camera2D
{
	public override void _Process(double delta)
	{
        	GlobalPosition = GetParent<Node2D>().GlobalPosition;
    	}
}
