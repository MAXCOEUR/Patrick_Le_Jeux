using Godot;
using System;

public abstract partial class fin_map : Node2D
{
	protected Area2D area;
	public override void _Ready()
	{
		area = GetNode<Area2D>("Area2D");
	}

}
