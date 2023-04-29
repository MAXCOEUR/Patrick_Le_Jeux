using Godot;
using System;

public partial class moving_platform_v : CharacterBody2D
{
	bool isDown=false;
	protected System.Timers.Timer changeTimer;
	public override void _Ready()
	{
		changeTimer = new System.Timers.Timer(10000);
		changeTimer.Elapsed += (timerSender, timerEvent) => change(timerSender, timerEvent);
		changeTimer.AutoReset = true;
		changeTimer.Enabled = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = new Vector2(0,0);

		if(isDown){
			velocity.Y=1;
		}else{
			velocity.Y=-1;
		}

		MoveAndCollide(velocity);
	}

	void change(object source, System.Timers.ElapsedEventArgs e){
		isDown=!isDown;
	}
}
