using Godot;
using System;

public partial class pistolet : Collection
{
	public override void _Ready()
	{
		base._Ready();
		id_collection.numeroMap=5;
		id_collection.id = 3;
	}
}
