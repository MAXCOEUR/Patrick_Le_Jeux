using Godot;
using System;

public partial class renard : Collection
{
	public override void _Ready()
	{
		base._Ready();
		id_collection.numeroMap=2;
		id_collection.id = 1;
	}
}
