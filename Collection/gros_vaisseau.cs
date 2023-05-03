using Godot;
using System;

public partial class gros_vaisseau : Collection
{
	public override void _Ready()
	{
		base._Ready();
		id_collection.numeroMap=3;
		id_collection.id = 2;
	}
}
