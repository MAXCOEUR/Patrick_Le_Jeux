using Godot;
using System;

public partial class map_5 : ZoneGame
{
	PackedScene fireBall_Scene;
	protected Timer timerFire;
	Area2D look_map;
	CameraScript camera;
	public override void _Ready()
	{
		base._Ready();
		look_map = GetNode<Area2D>("look_map");
		look_map.Connect("area_entered", new Callable(this, "on_UnZoom"));
		look_map.Connect("area_exited", new Callable(this, "on_Zoom"));
		camera = GetNode<CameraScript>("../Camera2D");

		fireBall_Scene = (PackedScene)ResourceLoader.Load("res://Objet/fireball.tscn");

		timerFire = new Timer();
		timerFire.WaitTime = 3;
		timerFire.Connect("timeout", new Callable(this, "sendFireBall"));
		AddChild(timerFire);
		timerFire.Start();



	}
	protected void sendFireBall()
	{
		sendBall(new Vector2(930, 730));
		sendBall(new Vector2(1340, 730));
		sendBall(new Vector2(1840, 730));
		sendBall(new Vector2(2300, 730));
		sendBall(new Vector2(2800, 730));
		sendBall(new Vector2(3230, 730));
		sendBall(new Vector2(4060, 730));
		sendBall(new Vector2(4930, 730));
		sendBall(new Vector2(5310, 730));
		sendBall(new Vector2(5650, 730));
		sendBall(new Vector2(5960, 730));
		sendBall(new Vector2(6300, 730));
		sendBall(new Vector2(7130, 730));
		sendBall(new Vector2(7540, 730));
		sendBall(new Vector2(7940, 730));
		sendBall(new Vector2(8340, 730));
		sendBall(new Vector2(15470, 730));
		sendBall(new Vector2(15850, 730));
		sendBall(new Vector2(16200, 730));
		sendBall(new Vector2(16560, 730));
		sendBall(new Vector2(16900, 730));
		sendBall(new Vector2(17280, 730));
		sendBall(new Vector2(17650, 730));
		sendBall(new Vector2(20080, 730));
		sendBall(new Vector2(20470, 730));
		sendBall(new Vector2(20800, 730));
		sendBall(new Vector2(21130, 730));
		sendBall(new Vector2(21450, 730));
		sendBall(new Vector2(22270, 730));
		sendBall(new Vector2(22710, 730));
		sendBall(new Vector2(23110, 730));
		sendBall(new Vector2(23500, 730));
		sendBall(new Vector2(26340, 730));
		sendBall(new Vector2(26600, 730));
		sendBall(new Vector2(26900, 730));
		sendBall(new Vector2(27150, 730));
		sendBall(new Vector2(27440, 730));
		sendBall(new Vector2(27720, 730));
	}

	private void sendBall(Vector2 vector)
	{
		fireball fireball = (fireball)fireBall_Scene.Instantiate();
		fireball.Position = vector;
		fireball.setdirection(new Vector2(0, -1));
		AddChild(fireball);
	}

	void on_UnZoom(Area2D otherArea)
	{
		GD.Print("unZoom");
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player"))
		{
			camera.ZoomOutFullMapView();
		}
	}
	void on_Zoom(Area2D otherArea)
	{
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player"))
		{
			camera.ZoomInNormalView();
		}
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

}
