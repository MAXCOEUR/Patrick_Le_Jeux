using Godot;
using System;

public partial class moving_platform_h2_bis : moving_platform_h
{
	public override void _Ready(){
		base._Ready();
		isleft=true;
		startPosition = new Vector2(Position.X-750,Position.Y);
	}
}
