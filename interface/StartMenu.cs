using Godot;
using System;
using System.Collections.Generic;

public partial class StartMenu : Control
{
	protected Database db = Database.Instance;
	private VideoStreamPlayer videoPlayer;
	VideoStream videoResource;

	private AudioStreamPlayer musique;
	AudioStream audioStream;

	int currentback=0;
	public override void _Ready()
	{
		GetNode<Button>("CenterContainer/GridContainer/bt_NouvellePartie").Connect("button_down", new Callable(this, "_OnBt_NouvellePartiePressed"));
		GetNode<Button>("CenterContainer/GridContainer/bt_quitter").Connect("button_down", new Callable(this, "_OnBt_quitter"));
		GetNode<Button>("CenterContainer/GridContainer/bt_collection").Connect("button_down", new Callable(this, "_OnBt_collection"));
		GetNode<Button>("CenterContainer/GridContainer/bt_recommencer").Connect("button_down", new Callable(this, "_OnBt_recommencer"));

		videoPlayer = GetNode<VideoStreamPlayer>("VideoStreamPlayer");
		videoPlayer.Connect("finished", new Callable(this, "OnVideoFinish"));
		var videoFilePath = "res://art/annimation/back.ogv"; // chemin de la vidéo
		videoResource = (VideoStream)ResourceLoader.Load(videoFilePath);
		videoPlayer.Stream = videoResource;
		videoPlayer.Play();

		musique = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		musique.Connect("finished", new Callable(this, "OnMusiqueFinish"));
		var musiqueFilePath = "res://art/musique/StartPage/1.mp3"; // chemin de la vidéo
		audioStream = (AudioStream)ResourceLoader.Load(musiqueFilePath);
		musique.Stream = audioStream;
		musique.Play();
	}
	private void OnVideoFinish(){
		videoPlayer.Stream = videoResource;
		videoPlayer.Play();
	}
	private void OnMusiqueFinish(){
		musique.Stream = audioStream;
		musique.Play();
	}
	
	private void _OnBt_NouvellePartiePressed()
	{	
		GetTree().ChangeSceneToFile("res://level/level_1.tscn");
	}
	private void _OnBt_quitter()
	{	
		GetTree().Quit();
	}
	private void _OnBt_collection(){
		GetTree().ChangeSceneToFile("res://Collection/collection_show.tscn");
	}

	private void _OnBt_recommencer(){
		db.resetTable();
	}
	
}
