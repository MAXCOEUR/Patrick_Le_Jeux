using Godot;
using System;

public partial class echap : Control
{

	private Camera2D _camera;
	
	public bool active =false;
	
	public override void _Ready()
	{
		_camera = GetNode<Camera2D>("/root/level_1/Camera2D");

		Visible = active;
		
		GetNode<Button>("CenterContainer/GridContainer/bt_continuer").Connect("button_down", new Callable(this, "_OnBt_Resume"));
		GetNode<Button>("CenterContainer/GridContainer/bt_revenir_au_menu").Connect("button_down", new Callable(this, "_OnBt_revenir_au_menu"));
		GetNode<Button>("CenterContainer/GridContainer/bt_menu_des_niveaux").Connect("button_down", new Callable(this, "_OnBt_menu_des_niveaux"));
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
		}
		Visible = active;
	}
	
	
	private void _OnBt_Resume()
	{
		active=false;
	}
	private void _OnBt_revenir_au_menu()
	{
		GetTree().ChangeSceneToFile("res://interface/StartMenu.tscn");
	}
	private void _OnBt_menu_des_niveaux()
	{
		menu_niveaux menu_niv = GetNode<menu_niveaux>("menu_niveaux");
		menu_niv.update();
		menu_niv.Visible=true;
	}
}
