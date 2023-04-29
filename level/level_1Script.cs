using Godot;
using System;

public partial class level_1Script : Node2D
{
	private ParametreLevel parametreLevel = new ParametreLevel();
	private Patrick _player;
	private CameraScript _camera;
	private bool _isPause = false;

	private Polygon2D background;


	public override void _Ready()
	{
		background = GetNode<Polygon2D>("Polygon2D");
		_camera = GetNode<CameraScript>("Camera2D");
		_player = GetNode<Patrick>("Patrick");
		_player.setEtat(1);
		setMap1();
	}

	public void setMap1()
	{
		removeNonEssentialChildren();
		_camera.set_maxOffset(new Vector2(15359, 720));
		background.Color = new Color(0 / 255.0f, 168 / 255.0f, 243 / 255.0f);
		PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://map/map_1.tscn");
		map_1 map = (map_1)missileScene.Instantiate();
		AddChild(map);
		//_player.Position = new Vector2(50, 500);
	}
	public void Finish_map_1(){
		GD.Print("Finish");
	}
	public void removeNonEssentialChildren()
	{
		foreach (Node child in GetChildren())
		{
			if (!(child is Patrick || child is Camera2D || child is Polygon2D))
			{
				child.QueueFree();
			}
		}
	}

}
