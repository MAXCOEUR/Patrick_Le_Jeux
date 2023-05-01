using Godot;
using System;
using System.Data.SQLite;

public partial class level_1Script : Node2D
{
	private ParametreLevel parametreLevel = new ParametreLevel();
	private Patrick _player;
	private CameraScript _camera;
	private bool _isPause = false;

	private Polygon2D background;
	private VideoStreamPlayer videoPlayer;
	protected Database db = Database.Instance;


	public override void _Ready()
	{
		videoPlayer = GetNode<VideoStreamPlayer>("Camera2D/VideoStreamPlayer");
		videoPlayer.Connect("finished", new Callable(this, "OnVideoFinish"));

		background = GetNode<Polygon2D>("Polygon2D");
		_camera = GetNode<CameraScript>("Camera2D");
		_player = GetNode<Patrick>("Patrick");
		_player.setEtat(1);


		SQLiteCommand command = new SQLiteCommand("SELECT MAX(numeroMap) FROM MapCurrent", db.getConnection);
		object result = command.ExecuteScalar();

		int maxNumeroMap = 0;
		try
		{
			maxNumeroMap = Convert.ToInt32(result);
		}
		catch
		{
			maxNumeroMap = 1;
		}

		//debugMode();
		setMapChose(maxNumeroMap);
	}

	private void setMapChose(int numMap){
		switch (numMap) {
			case 1:
				setVideo("res://art/annimation/video_episode1/intro.ogv");
			break;
			case 2:
				setMap2();
			break;
			case 3:
			break;
			case 4:
			break;
			case 5:
			break;
		}
	}

	public void setMap1()
	{
		removeNonEssentialChildren();

		try{
			string insertQuery = "INSERT INTO MapCurrent(numeroMap) VALUES(@numeroMap)";
			SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);
			insertCommand.Parameters.AddWithValue("@numeroMap", 1);
			insertCommand.ExecuteNonQuery();
		}catch{
			
		}
		

		setVideo("res://art/annimation/video_episode1/debut_map1.ogv");
	}
	public void Finish_map_1()
	{
		setMapBoss1();
	}
	public void setMapBoss1()
	{
		removeNonEssentialChildren();
		setVideo("res://art/annimation/video_episode1/start_fight.ogv");
	}
	public void Finish_map_Boss_1()
	{
		setVideo("res://art/annimation/video_episode1/fin_fight.ogv");
	}

	public void setMap2()
	{
		removeNonEssentialChildren();
		try{
			string insertQuery = "INSERT INTO MapCurrent(numeroMap) VALUES(@numeroMap)";
			SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);
			insertCommand.Parameters.AddWithValue("@numeroMap", 2);
			insertCommand.ExecuteNonQuery();
		}catch{

		}

		setVideo("res://art/annimation/video_episode2/debut_map.ogv");
	}
	public void Finish_map_2()
	{
		setMapBoss2();
	}
	public void setMapBoss2()
	{
		removeNonEssentialChildren();
		setVideo("res://art/annimation/video_episode2/start_fight.ogv");
	}
	public void Finish_map_Boss_2()
	{
		setVideo("res://art/annimation/video_episode2/fin_fight.ogv");
	}

	public void setVideo(string lienVideo)
	{
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
				_camera.set_maxOffset(new Vector2(15359, -400));
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
			case "res://art/annimation/video_episode1/fin_fight.ogv":
				setMap2();
				break;
			case "res://art/annimation/video_episode2/debut_map.ogv":
				_camera.set_maxOffset(new Vector2(25600, -1440));
				background.Color = new Color(0 / 255.0f, 168 / 255.0f, 243 / 255.0f);
				PackedScene SceneMap2 = (PackedScene)ResourceLoader.Load("res://map/map_2.tscn");
				map_2 map2 = (map_2)SceneMap2.Instantiate();
				AddChild(map2);
				_player.Position = new Vector2(100, 500);
				_player.setParam(new ParametreLevel());
				break;
			case "res://art/annimation/video_episode2/start_fight.ogv":
				_camera.set_maxOffset(new Vector2(1280, 0));
				background.Color = new Color(255 / 255.0f, 0 / 255.0f, 0 / 255.0f);
				PackedScene missileSceneBoss2 = (PackedScene)ResourceLoader.Load("res://map/boss/map_boss_2.tscn");
				map_boss_2 mapBoss2 = (map_boss_2)missileSceneBoss2.Instantiate();
				AddChild(mapBoss2);
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
		_player.setEtat(_player.getEtat());
		GD.Print("remove all children");
	}

	private void debugMode()
	{
		_camera.set_maxOffset(new Vector2(15359, -400));
		background.Color = new Color(0 / 255.0f, 168 / 255.0f, 243 / 255.0f);
		PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://map/map_1.tscn");
		map_1 map = (map_1)missileScene.Instantiate();
		AddChild(map);
		_player.Position = new Vector2(14300, 0);
		_player.setParam(new ParametreLevel());
	}

}
