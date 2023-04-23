using Godot;
using System;

public partial class level_1Script : Node2D
{
	private ParametreLevel parametreLevel = new ParametreLevel();
	private Patrick _player;
	private CameraScript _camera;
	private bool _isPause =false;

	
	public override void _Ready()
	{
		_player = GetNode<Patrick>("Patrick");
		_player.setParam(parametreLevel);
		
	}
}
