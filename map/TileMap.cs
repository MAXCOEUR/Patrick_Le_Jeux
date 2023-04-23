using Godot;
using System;

public partial class TileMap : Godot.TileMap
{
	public override void _Ready()
	{
		AddToGroup("decor");
	}

	internal int GetCellSourceId(int v, int y)
	{
		throw new NotImplementedException();
	}
}
