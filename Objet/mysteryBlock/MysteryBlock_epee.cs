using Godot;
using System;

public partial class MysteryBlock_epee : MysteryBlock
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		PackedScene mushroomScene = (PackedScene)ResourceLoader.Load("res://Objet/epee.tscn");
		Object = (Object)mushroomScene.Instantiate();

		// Récupération de la valeur de la metadata "TypeObjet" en tant que chaîne de caractères
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		
	}
}
