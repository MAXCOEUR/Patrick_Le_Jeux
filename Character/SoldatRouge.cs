using Godot;
using System;

public partial class SoldatRouge : Among_us_rouge
{
	protected Timer timerFire;
	PackedScene epeeScene;
	public override void _Ready()
	{
		base._Ready();
		timerFire = new Timer();
		timerFire.WaitTime = 2f;
		timerFire.Connect("timeout", new Callable(this,"send"));
		AddChild(timerFire);
		timerFire.Start();
		epeeScene = (PackedScene)ResourceLoader.Load("res://Objet/epee.tscn");
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
	public void send()
	{
		epee epee = (epee)epeeScene.Instantiate();
		epee.Position = Position;
		epee.setdirection(new Vector2(0,0),this);

		//missile.setdirection(direction);
		GetParent().AddChild(epee);
		if (directionDeplacment.X < 0)
		{
			epee.setdirection(new Vector2(-1,0),this);
		}
		else
		{
			epee.setdirection(new Vector2(1,0),this);
		}
		epee.lance();

	}
}
