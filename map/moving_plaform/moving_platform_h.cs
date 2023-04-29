using Godot;
using System;

public partial class moving_platform_h : CharacterBody2D
{
	protected bool isleft=false;
	protected System.Timers.Timer changeTimer;
	public override void _Ready(){
		changeTimer = new System.Timers.Timer(10000);
		changeTimer.Elapsed += (timerSender, timerEvent) => change(timerSender, timerEvent);
		changeTimer.AutoReset = true;
		changeTimer.Enabled = true;
	}
	public override void _Process(double delta){
		Vector2 velocity = new Vector2(0,0);

		if(isleft){
			velocity.X=-2;
		}else{
			velocity.X=2;
		}

		MoveAndCollide(velocity);
	}

	void change(object source, System.Timers.ElapsedEventArgs e){
		isleft=!isleft;
	}
}
