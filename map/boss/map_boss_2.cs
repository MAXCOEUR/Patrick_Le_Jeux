using Godot;
using System;

public partial class map_boss_2 : ZoneGame
{
	private Timer timer;

	private Timer timerRemoveSol_1;
	private Timer timerRemoveSol_2;
	private Timer timerRemoveSol_3;
	private PackedScene sol_1_Scene;
	private PackedScene sol_2_Scene;
	private PackedScene sol_3_Scene;
	private int solActif = 1;
	private AudioStreamPlayer musique;
	AudioStream audioStream;

	pascal boss;
	public override void _Ready()
	{
		base._Ready();
		sol_1_Scene = (PackedScene)ResourceLoader.Load("res://map/boss/sol_boss_2/sol_1.tscn");
		sol_2_Scene = (PackedScene)ResourceLoader.Load("res://map/boss/sol_boss_2/sol_2.tscn");
		sol_3_Scene = (PackedScene)ResourceLoader.Load("res://map/boss/sol_boss_2/sol_3.tscn");

		boss = GetNode<pascal>("Pascal");

		timer = new Timer();
		timer.WaitTime = 20;
		timer.OneShot = false;
		timer.Connect("timeout", new Callable(this, "ChangerSols"));
		AddChild(timer);
		timer.Start();

		timerRemoveSol_1 = new Timer();
		timerRemoveSol_1.WaitTime = 3;
		timerRemoveSol_1.OneShot = true;
		timerRemoveSol_1.Connect("timeout", new Callable(this, "removeSol1"));
		AddChild(timerRemoveSol_1);

		timerRemoveSol_2 = new Timer();
		timerRemoveSol_2.WaitTime = 3;
		timerRemoveSol_2.OneShot = true;
		timerRemoveSol_2.Connect("timeout", new Callable(this, "removeSol2"));
		AddChild(timerRemoveSol_2);

		timerRemoveSol_3 = new Timer();
		timerRemoveSol_3.WaitTime = 3;
		timerRemoveSol_3.OneShot = true;
		timerRemoveSol_3.Connect("timeout", new Callable(this, "removeSol3"));
		AddChild(timerRemoveSol_3);

		ChangerSols();
		musique = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		musique.Connect("finished", new Callable(this, "OnMusiqueFinish"));
		var musiqueFilePath = "res://art/musique/map_2_boss/boss2.mp3"; // chemin de la vidéo
		audioStream = (AudioStream)ResourceLoader.Load(musiqueFilePath);
		musique.Stream = audioStream;
		musique.Play();
	}
	private void OnMusiqueFinish(){
		musique.Stream = audioStream;
		musique.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	private void ChangerSols()
	{
		boss.setSol(solActif);
		if (solActif == 1)
		{
			sol_1 sol = (sol_1)sol_1_Scene.Instantiate();
			AddChild(sol);
			solActif = 2;
			timerRemoveSol_3.Start();
		}
		else if (solActif == 2)
		{
			sol_2 sol = (sol_2)sol_2_Scene.Instantiate();
			AddChild(sol);
			solActif = 3;
			timerRemoveSol_1.Start();
		}
		else if (solActif == 3)
		{
			sol_3 sol = (sol_3)sol_3_Scene.Instantiate();
			AddChild(sol);
			solActif = 1;
			timerRemoveSol_2.Start();
		}
	}

	public void removeSol1(){
		foreach (Node child in GetChildren())
		{
			if (child is sol_1)
				{
					child.QueueFree();
				}
		}
	}
	public void removeSol2(){
		foreach (Node child in GetChildren())
		{
			if (child is sol_2)
				{
					child.QueueFree();
				}
		}
	}
	public void removeSol3(){
		foreach (Node child in GetChildren())
		{
			if (child is sol_3)
				{
					child.QueueFree();
				}
		}
	}
}
