using Godot;
using System;

public partial class moving_platform_v3_bis : moving_platform_v
{
	public override void _Ready(){
		base._Ready();
		isDown=true;
		startPosition = new Vector2(Position.X,Position.Y+450);
	}
}
