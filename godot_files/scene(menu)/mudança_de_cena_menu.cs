using Godot;
using System;

public partial class mudança_de_cena_menu : TextureButton
{
	
	public override void _Ready()
	{
		Pressed += AoClicar;
	}
	
	private void AoClicar()
	{
		GetTree().ChangeSceneToFile("mapa1.tscn");
	}
	
	}
