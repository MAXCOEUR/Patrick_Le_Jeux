using Godot;
using System;

public partial class CameraScript : Camera2D
{
	private CharacterBody2D _player;
	private int speed = 4000;
	private Vector2 _maxOffset;
	private Vector2 _velocity;

	[Export]
	private float _smoothTime = 0.3f;

	public override void _Ready()
	{
		_player = GetNode<CharacterBody2D>("../Patrick");
		Vector2 _viewportSize = GetViewportRect().Size;
		//_maxOffset = new Vector2(15359, 720);

		Vector2 targetPos = _player.GlobalPosition;
		Vector2 camPos = Position;
		Vector2 offset = targetPos;

		offset.X = Mathf.Max(offset.X, _viewportSize.X / 2);
		offset.Y = Mathf.Min(offset.Y,-_viewportSize.Y/2+720 );
		Position=offset;
	}

	public override void _PhysicsProcess(Double delta)
	{
		Vector2 _viewportSize = GetViewportRect().Size;
		Vector2 targetPos = _player.GlobalPosition;
		Vector2 camPos = Position;
		Vector2 offset = targetPos;

		offset.X = Mathf.Max(offset.X, _viewportSize.X / 2);
		offset.Y = Mathf.Min(offset.Y,-_viewportSize.Y/2+720 );

		// Déplacer la caméra en douceur
		Position = Position.MoveToward(offset, speed * (float)delta);

	}
}

