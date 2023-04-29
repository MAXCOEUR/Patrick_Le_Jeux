using Godot;
using System;

public partial class MysteryBlock_missile : MysteryBlock
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		PackedScene mushroomScene = (PackedScene)ResourceLoader.Load("res://Objet/missile.tscn");
		Object = (Object)mushroomScene.Instantiate();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
