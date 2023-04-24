using Godot;
using System;

public partial class Among_us_rouge : Among_us_vert
{

	private DateTime lastChangeEmpty= DateTime.UtcNow;
	


	public override void _Ready()
	{
		base._Ready();
	}
	public bool isOnPlatformEnd() {
		if(raycastBot.IsColliding()){
			return false;
		}
		return true;
	}

	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;
		TimeSpan duration = DateTime.UtcNow.Subtract(lastChangeEmpty);
		if ((isOnPlatformEnd()) && duration.TotalMilliseconds >= difTimeChangeDir)
		{
			lastChangeEmpty = DateTime.UtcNow;
			changeDirection();
		}

	}
}



