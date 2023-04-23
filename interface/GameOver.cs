using Godot;
using System;

public partial class GameOver : Control
{
	private Camera2D _camera;
	
	
	public override void _Ready()
	{
		_camera = GetNode<Camera2D>("../../Camera2D");
		
		
		Visible = false;
		
		GetNode<Button>("CenterContainer/GridContainer/bt_recommancer").Connect("button_down", new Callable(this, "_OnBt_Recommancer"));
		GetNode<Button>("CenterContainer/GridContainer/bt_revenir_au_menu").Connect("button_down", new Callable(this, "_OnBt_revenir_au_menu"));
	}
	
	public void setVisible(){
		Rect2 cameraRect = _camera.GetViewportRect();
		Vector2 newSize = new Vector2(cameraRect.Size.X,cameraRect.Size.Y);
		Size = newSize;
		
		SetPosition(new Vector2(-cameraRect.Size.X/2,-cameraRect.Size.Y/2));
		
		Visible = true;
	}
	
	private void _OnBt_Recommancer()
	{
		GetTree().ReloadCurrentScene();
	}
	private void _OnBt_revenir_au_menu()
	{
		GetTree().ChangeSceneToFile("res://interface/StartMenu.tscn");
	}
}
