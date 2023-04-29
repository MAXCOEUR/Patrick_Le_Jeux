using Godot;
using System;
using System.Threading;

public abstract partial class fin_map : Node2D
{
	protected Area2D area;
	protected level_1Script level_1;
	protected System.Timers.Timer timerEnd;
	public override void _Ready()
	{
		area = GetNode<Area2D>("Area2D");
		area.Connect("area_entered", new Callable(this, "OnFinish"));
		level_1 = GetNode<level_1Script>("../../../level_1");
		timerEnd = new System.Timers.Timer(3000);
		timerEnd.Elapsed += (timerSender, timerEvent) =>callLevel1();
	}

	protected void OnFinish(Area2D otherArea){
		timerEnd.Start();
	}

	protected abstract void callLevel1();

}
