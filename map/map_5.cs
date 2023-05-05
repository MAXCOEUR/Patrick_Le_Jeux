using Godot;
using System;

public partial class map_5 : ZoneGame
{
	PackedScene fireBall_Scene;
	protected Timer timerFire;
	public override void _Ready()
	{
		base._Ready();

		fireBall_Scene= (PackedScene)ResourceLoader.Load("res://Objet/fireball.tscn");

		timerFire = new Timer();
		timerFire.WaitTime = 3;
		timerFire.Connect("timeout", new Callable(this, "sendFireBall"));
		AddChild(timerFire);
		timerFire.Start();

		
		
	}
	protected void sendFireBall(){
		sendBall(new Vector2(930,730));
		sendBall(new Vector2(1340,730));
		sendBall(new Vector2(1840,730));
		sendBall(new Vector2(2300,730));
		sendBall(new Vector2(2800,730));
		sendBall(new Vector2(3230,730));
	}

	private void sendBall(Vector2 vector){
		fireball fireball = (fireball)fireBall_Scene.Instantiate();
		fireball.Position = vector;
		fireball.setdirection(new Vector2(0,-1));
		AddChild(fireball);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
{
	base._Process(delta);


	if (Input.IsActionJustPressed("ui_cancel"))
	{
		// Arrêter le timer et supprimer tous les enfants avant de quitter
		
		
		timerFire.Stop();
		foreach (Node child in GetChildren())
		{
			child.QueueFree();
		}
		GetTree().Quit();
	}
}

}
