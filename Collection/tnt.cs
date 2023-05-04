using Godot;
using System;

public partial class tnt : Collection
{
	public override void _Ready()
	{
		base._Ready();
		id_collection.numeroMap=5;
		id_collection.id = 2;
	}
}
