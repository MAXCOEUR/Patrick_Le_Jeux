using Godot;
using System;

public partial class map_boss_1 : ZoneGame
{
	protected System.Timers.Timer timerMovingPlatforme;
	private Node2D previousMovingPlatform_left;
	private Node2D previousMovingPlatform_right;
	PackedScene MovingPlatformScene = (PackedScene)ResourceLoader.Load("res://map/moving_plaform/moving_platform_v2.tscn");
	private AudioStreamPlayer musique;
	AudioStream audioStream;
	public override void _Ready()
	{
		base._Ready();
		timerMovingPlatforme = new System.Timers.Timer(6000);
		timerMovingPlatforme.Elapsed += (timerSender, timerEvent) => sendMovingPlatforme();
		timerMovingPlatforme.AutoReset = true;
		timerMovingPlatforme.Enabled = true;

		musique = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		musique.Connect("finished", new Callable(this, "OnMusiqueFinish"));
		var musiqueFilePath = "res://art/musique/map_1_boss/boss1.mp3"; // chemin de la vidéo
		audioStream = (AudioStream)ResourceLoader.Load(musiqueFilePath);
		musique.Stream = audioStream;
		musique.Play();
	}

	private void OnMusiqueFinish(){
		musique.Stream = audioStream;
		musique.Play();
	}
	public void sendMovingPlatforme()
	{
		if (previousMovingPlatform_left != null)
		{
			previousMovingPlatform_left.QueueFree();
		}
		if (previousMovingPlatform_right != null)
		{
			previousMovingPlatform_right.QueueFree();
		}

		moving_platform_v2 moving_platform_left = (moving_platform_v2)MovingPlatformScene.Instantiate();
		moving_platform_v2 moving_platform_right = (moving_platform_v2)MovingPlatformScene.Instantiate();
		moving_platform_left.Position = new Vector2(200, 680);
		moving_platform_right.Position = new Vector2(1100, 680);
		GetParent().AddChild(moving_platform_left);
		GetParent().AddChild(moving_platform_right);

		previousMovingPlatform_left = moving_platform_left;
		previousMovingPlatform_right = moving_platform_right;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
}
