using Godot;
using System;

public partial class map_boss_1 : ZoneGame
{
	protected System.Timers.Timer timerMovingPlatforme;
	private Node2D previousMovingPlatform_left;
	private Node2D previousMovingPlatform_right;
	PackedScene MovingPlatformScene = (PackedScene)ResourceLoader.Load("res://map/moving_plaform/moving_platform_v2.tscn");
	public override void _Ready()
	{
		base._Ready();
		timerMovingPlatforme = new System.Timers.Timer(5000);
		timerMovingPlatforme.Elapsed += (timerSender, timerEvent) => sendMovingPlatforme();
		timerMovingPlatforme.AutoReset = true;
		timerMovingPlatforme.Enabled = true;
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
		moving_platform_left.Position = new Vector2(120, 650);
		moving_platform_right.Position = new Vector2(1190, 650);
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
