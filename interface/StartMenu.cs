using Godot;
using System;

public partial class StartMenu : Control
{
	protected Database db = Database.Instance;
	public override void _Ready()
	{
		GetNode<Button>("CenterContainer/GridContainer/bt_NouvellePartie").Connect("button_down", new Callable(this, "_OnBt_NouvellePartiePressed"));
		GetNode<Button>("CenterContainer/GridContainer/bt_quitter").Connect("button_down", new Callable(this, "_OnBt_quitter"));
		GetNode<Button>("CenterContainer/GridContainer/bt_collection").Connect("button_down", new Callable(this, "_OnBt_collection"));
		GetNode<Button>("CenterContainer/GridContainer/bt_recommencer").Connect("button_down", new Callable(this, "_OnBt_recommencer"));

	}
	
	
	private void _OnBt_NouvellePartiePressed()
	{	
		GetTree().ChangeSceneToFile("res://level/level_1.tscn");
	}
	private void _OnBt_quitter()
	{	
		GetTree().Quit();
	}
	private void _OnBt_collection(){
		GetTree().ChangeSceneToFile("res://Collection/collection_show.tscn");
	}

	private void _OnBt_recommencer(){
		db.resetTable();
	}
	
}
