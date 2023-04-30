using Godot;
using System;

public partial class map_2 : ZoneGame
{
	// Called when the node enters the scene tree for the first time.
	Area2D look_map;
	CameraScript camera;
	public override void _Ready()
	{
		base._Ready();
		look_map= GetNode<Area2D>("look_map");
		look_map.Connect("area_entered", new Callable(this, "on_UnZoom"));
		look_map.Connect("area_exited", new Callable(this, "on_Zoom"));
		camera = GetNode<CameraScript>("../Camera2D");
	}

	void on_UnZoom(Area2D otherArea) {
		GD.Print("unZoom");
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player")) {
			camera.ZoomOutFullMapView();
		}
	}
	void on_Zoom(Area2D otherArea) {
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player")) {
			camera.ZoomInNormalView();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
