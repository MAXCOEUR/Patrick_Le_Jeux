using Godot;
using System;

public partial class moving_platform_v : CharacterBody2D
{
	protected bool isDown=false;
	protected Vector2 startPosition;
	protected Vector2 endPosition;

	public override void _Ready()
	{
		startPosition = Position;
		endPosition = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Position.Y>=startPosition.Y)
		{
			isDown=false;
		}
		else if(Position.Y<=endPosition.Y)
		{
			isDown=true;
		}

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
