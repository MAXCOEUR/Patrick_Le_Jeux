using Godot;
using System;

public partial class SoldatRouge : Among_us_rouge
{
	protected System.Timers.Timer timerFire;
	public override void _Ready()
	{
		base._Ready();
		timerFire = new System.Timers.Timer(1500);
		timerFire.Elapsed += (timerSender, timerEvent) => send(timerSender, timerEvent);
		timerFire.AutoReset = true;
		timerFire.Enabled = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
	public void send(object source, System.Timers.ElapsedEventArgs e)
	{
		PackedScene epeeScene = (PackedScene)ResourceLoader.Load("res://Objet/epee.tscn");
		epee epee = (epee)epeeScene.Instantiate();
		epee.Position = Position;
		epee.setUser(this);

		//missile.setdirection(direction);
		GetParent().AddChild(epee);
		if (directionDeplacment.X < 0)
		{
			epee.lanceLeft();
		}
		else
		{
			epee.lanceRight();
		}

	}
}
