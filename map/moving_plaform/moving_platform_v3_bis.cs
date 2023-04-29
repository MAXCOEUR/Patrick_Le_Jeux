using Godot;
using System;

public partial class moving_platform_v3_bis : moving_platform_v
{
	public override void _Ready(){
		base._Ready();
		isDown=true;
		changeTimer.Interval=5000;
	}
}
