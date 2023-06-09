using Godot;
using System;
using System.Diagnostics;
using System.Collections.Generic;

public partial class StartMenu : Control
{
	protected Database db = Database.Instance;
	private VideoStreamPlayer videoPlayer;
	VideoStream videoResource;

	private AudioStreamPlayer musique;
	AudioStream audioStream;
	SceneTree tree;
	int currentback=0;
	public override void _Ready()
	{
		GetNode<Button>("CenterContainer/GridContainer/bt_NouvellePartie").Connect("button_down", new Callable(this, "_OnBt_NouvellePartiePressed"));
		GetNode<Button>("CenterContainer/GridContainer/bt_quitter").Connect("button_down", new Callable(this, "_OnBt_quitter"));
		GetNode<Button>("CenterContainer/GridContainer/bt_collection").Connect("button_down", new Callable(this, "_OnBt_collection"));
		GetNode<Button>("CenterContainer/GridContainer/bt_recommencer").Connect("button_down", new Callable(this, "_OnBt_recommencer"));
		GetNode<Button>("CenterContainer/GridContainer/bt_siteWeb").Connect("button_down", new Callable(this, "_OnBt_siteWeb"));

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
		tree = GetTree();
	}
	private void _OnBt_siteWeb(){
		string url = "https://patrick-le-stegosaure.vercel.app";  // Remplacez par l'URL du site web que vous souhaitez ouvrir

		Process.Start(new ProcessStartInfo
		{
			FileName = url,
			UseShellExecute = true
		});
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
		tree.ChangeSceneToFile("res://level/level_1.tscn");
	}
	private void _OnBt_quitter()
	{	
		tree.Quit();
	}
	private void _OnBt_collection(){
		tree.ChangeSceneToFile("res://Collection/collection_show.tscn");
		
	}

	private void _OnBt_recommencer(){
		db.resetTable();
	}
	
}
