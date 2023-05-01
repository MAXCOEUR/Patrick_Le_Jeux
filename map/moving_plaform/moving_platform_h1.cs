using Godot;
using System;

public partial class moving_platform_h1 : moving_platform_h
{
	public override void _Ready(){
		base._Ready();
		endPosition = new Vector2(Position.X+750,Position.Y);
	}
}
