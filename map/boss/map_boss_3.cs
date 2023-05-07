using Godot;
using System;

public partial class map_boss_3 : ZoneGame
{
	private AudioStreamPlayer musique;
	AudioStream audioStream;
	public override void _Ready()
	{
		base._Ready();
		musique = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		musique.Connect("finished", new Callable(this, "OnMusiqueFinish"));
		var musiqueFilePath = "res://art/musique/map_3_boss/boss3.mp3"; // chemin de la vidéo
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
}
