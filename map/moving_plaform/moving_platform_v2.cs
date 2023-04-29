using Godot;
using System;

public partial class moving_platform_v2 : moving_platform_v
{
	public override void _Ready(){
		base._Ready();
		changeTimer.Interval=5000;
	}
}
