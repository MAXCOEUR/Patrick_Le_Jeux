using Godot;
using System;

public partial class moving_platform_v3 : moving_platform_v
{
	public override void _Ready(){
		base._Ready();
		changeTimer.Interval=3000;
	}
}
