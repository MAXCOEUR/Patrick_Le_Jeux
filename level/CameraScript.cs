using Godot;
using System;

public partial class CameraScript : Camera2D
{
	private CharacterBody2D _player;
	private int speed = 4000;
	private Vector2 _maxOffset;
	private Vector2 _sizeScreen = new Vector2(1280,720);
	private Vector2 _velocity;

	[Export]
	private float _smoothTime = 0.3f;

	private bool _isZoomedOut = false;
	private float _zoomFactor;

	public override void _Ready()
	{
		_player = GetNode<CharacterBody2D>("../Patrick");
		_maxOffset = new Vector2(15359, 0);
	}

	public void set_maxOffset(Vector2 maxOffset)
	{
		_maxOffset = maxOffset;
	}

	public override void _Process(double delta)
	{
		if (_isZoomedOut)
		{
			float zoom = _zoomFactor;
			Position = Position.MoveToward(_maxOffset/2, speed * (float)delta);
			Zoom = Zoom.MoveToward(new Vector2(zoom,zoom),2*((float)delta));
		}
		else
		{
			Vector2 _viewportSize = GetViewportRect().Size;

			// Calculer le zoom en fonction du ratio de l'écran
			float zoom = _viewportSize.Y / _sizeScreen.Y;

			// Ajuster la position maximale de la caméra en fonction du zoom
			Vector2 maxPos = _maxOffset * zoom;

			Vector2 targetPos = _player.GlobalPosition;
			Vector2 offset = targetPos;

			offset.X = Mathf.Clamp(offset.X,_sizeScreen.X/ 2, maxPos.X-_sizeScreen.X / 2 );
			offset.Y = Mathf.Clamp(offset.Y,maxPos.Y+_sizeScreen.Y / 2,_sizeScreen.Y / 2);

			// Déplacer la caméra en douceur
			Position = Position.MoveToward(offset, speed * (float)delta);

			// Ajuster le zoom de la caméra
			Zoom = Zoom.MoveToward(new Vector2(zoom, zoom),2*((float)delta));
		}
	}

	public void MoveToFullMapView()
	{
		Vector2 fullMapOffset = new Vector2(_maxOffset.X / 2, 0f);
		Position = fullMapOffset;
	}

	public void ZoomOutFullMapView()
	{
		Vector2 sizeScreen = GetViewportRect().Size;
		GD.Print(sizeScreen.X + " / "+ _maxOffset.X);
		_zoomFactor = sizeScreen.X/_maxOffset.X;
		_isZoomedOut = true;
	}

	public void ZoomInNormalView()
	{
		_isZoomedOut = false;
	}
}
