using Godot;
using System;

public partial class moving_platform_v1 : moving_platform_v
{
	public override void _Ready(){
		base._Ready();
		endPosition = new Vector2(Position.X,Position.Y-450);
	}
}
