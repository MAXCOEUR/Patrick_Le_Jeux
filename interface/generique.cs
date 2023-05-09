using Godot;
using System;

public partial class generique : Control
{
	private VideoStreamPlayer videoPlayer;
	public override void _Ready()
	{
		videoPlayer = GetNode<VideoStreamPlayer>("VideoStreamPlayer");
		videoPlayer.Connect("finished", new Callable(this, "OnVideoFinish"));
		var videoFilePath = "res://art/annimation/generique.ogv"; // chemin de la vidéo
		var videoResource = (VideoStream)ResourceLoader.Load(videoFilePath);
		videoPlayer.Stream = videoResource;

		// Démarrer la lecture de la vidéo
		GD.Print("set video play");
		videoPlayer.Play();
	}

	private void OnVideoFinish(){
		GetTree().ChangeSceneToFile("res://interface/StartMenu.tscn");
	}
}
