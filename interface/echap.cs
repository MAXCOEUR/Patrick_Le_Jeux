using Godot;
using System;

public partial class echap : Control
{

	private Camera2D _camera;
	
	private bool active =false;
	
	public override void _Ready()
	{
		_camera = GetNode<Camera2D>("/root/level_1/Camera2D");

		Visible = active;
		
		GetNode<Button>("CenterContainer/GridContainer/bt_continuer").Connect("button_down", new Callable(this, "_OnBt_Resume"));
		GetNode<Button>("CenterContainer/GridContainer/bt_revenir_au_menu").Connect("button_down", new Callable(this, "_OnBt_revenir_au_menu"));
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("ui_escape"))
		{
			Rect2 cameraRect = _camera.GetViewport().GetVisibleRect();
			Vector2 newSize = new Vector2(cameraRect.Size.X,cameraRect.Size.Y);
			Size = newSize;

			SetPosition(new Vector2(-cameraRect.Size.X/2,-cameraRect.Size.Y/2));

			active=!active;
			Visible = active;
		}
	}
	
	
	private void _OnBt_Resume()
	{
		active=false;
		Visible = active;
	}
	private void _OnBt_revenir_au_menu()
	{
		GetTree().ChangeSceneToFile("res://interface/StartMenu.tscn");
	}
}
