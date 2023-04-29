using Godot;
using System;

public partial class moving_platform_h3 : moving_platform_h
{
	public override void _Ready(){
		base._Ready();
		changeTimer.Interval=3000;
	}
}
