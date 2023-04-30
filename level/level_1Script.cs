using Godot;
using System;

public partial class level_1Script : Node2D
{
	private ParametreLevel parametreLevel = new ParametreLevel();
	private Patrick _player;
	private CameraScript _camera;
	private bool _isPause = false;

	private Polygon2D background;
	private VideoStreamPlayer videoPlayer;


	public override void _Ready()
	{
		videoPlayer = GetNode<VideoStreamPlayer>("Camera2D/VideoStreamPlayer");
		videoPlayer.Connect("finished", new Callable(this, "OnVideoFinish"));

		background = GetNode<Polygon2D>("Polygon2D");
		_camera = GetNode<CameraScript>("Camera2D");
		_player = GetNode<Patrick>("Patrick");
		_player.setEtat(1);
		debugMode();
		//setVideo("res://art/annimation/video_episode1/intro.ogv");
	}

	public void setMap1()
	{
		removeNonEssentialChildren();
		setVideo("res://art/annimation/video_episode1/debut_map1.ogv");
	}
	public void Finish_map_1()
	{
		GD.Print("finish map");
		setMapBoss1();
	}
	public void setMapBoss1()
	{
		GD.Print("setmapBoss1");
		removeNonEssentialChildren();
		setVideo("res://art/annimation/video_episode1/start_fight.ogv");
	}
	public void Finish_map_Boss_1()
	{
		GD.Print("Finish");
	}

	public void setVideo(string lienVideo)
	{
		GD.Print("set video");
		// Charger la vidéo
		_player.Position = new Vector2(0, 0);
		var videoFilePath = lienVideo; // chemin de la vidéo
		var videoResource = (VideoStream)ResourceLoader.Load(videoFilePath);
		videoPlayer.Stream = videoResource;

		// Démarrer la lecture de la vidéo
		GD.Print("set video play");
		videoPlayer.Play();
	}
	private void OnVideoFinish()
	{
		GD.Print("OnVideoFinish");
		switch (videoPlayer.Stream.ResourcePath.ToString())
		{
			case "res://art/annimation/video_episode1/intro.ogv":
				setMap1();
				break;
			case "res://art/annimation/video_episode1/debut_map1.ogv":
				_camera.set_maxOffset(new Vector2(15359, 0));
				background.Color = new Color(0 / 255.0f, 168 / 255.0f, 243 / 255.0f);
				PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://map/map_1.tscn");
				map_1 map = (map_1)missileScene.Instantiate();
				AddChild(map);
				_player.Position = new Vector2(100, 500);
				_player.setParam(new ParametreLevel());
				break;
			case "res://art/annimation/video_episode1/start_fight.ogv":
				_camera.set_maxOffset(new Vector2(1280, 0));
				background.Color = new Color(255 / 255.0f, 0 / 255.0f, 0 / 255.0f);
				PackedScene missileSceneBoss1 = (PackedScene)ResourceLoader.Load("res://map/boss/map_boss_1.tscn");
				map_boss_1 mapBoss1 = (map_boss_1)missileSceneBoss1.Instantiate();
				AddChild(mapBoss1);
				_player.Position = new Vector2(100, 500);
				_player.setParam(new ParametreLevel());
				break;
		}
	}

	public void removeNonEssentialChildren()
	{
		foreach (Node child in GetChildren())
		{
			if (!(child is Patrick || child is Camera2D || child is Polygon2D || child is VideoStreamPlayer))
			{
				if (child is ZoneGame)
				{
					((ZoneGame)child).isEndMap();
				}
				child.QueueFree();
			}
		}
		GD.Print("remove all children");
	}

	private void debugMode()
	{
		_camera.set_maxOffset(new Vector2(1280, 0));
		background.Color = new Color(255 / 255.0f, 0 / 255.0f, 0 / 255.0f);
		PackedScene missileSceneBoss1 = (PackedScene)ResourceLoader.Load("res://map/boss/map_boss_1.tscn");
		map_boss_1 mapBoss1 = (map_boss_1)missileSceneBoss1.Instantiate();
		AddChild(mapBoss1);
		_player.Position = new Vector2(100, 500);
		_player.setParam(new ParametreLevel());
	}

}
