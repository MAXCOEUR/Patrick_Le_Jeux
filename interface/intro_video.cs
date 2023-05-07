using Godot;
using System;

public partial class intro_video : Control
{
	private VideoStreamPlayer videoPlayer;
	public override void _Ready()
	{
		videoPlayer = GetNode<VideoStreamPlayer>("VideoStreamPlayer");
		videoPlayer.Connect("finished", new Callable(this, "OnVideoFinish"));
		var videoFilePath = "res://art/annimation/intro-Jeux.ogv"; // chemin de la vidéo
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
