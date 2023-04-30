using Godot;
using System;

public partial class menu_niveaux : Control
{
	level_1Script level_1Script;
	echap ech;
	public override void _Ready()
	{
		level_1Script = GetNode<level_1Script>("../../../../level_1");
		ech = GetNode<echap>("../../echap");
		GetNode<Button>("bt_quitter").Connect("button_down", new Callable(this, "_OnBt_quitter"));

		GetNode<Button>("level_1/GridContainer/bt_map_1").Connect("button_down", new Callable(this, "_OnBt_level1_map1"));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	void _OnBt_quitter(){
		Visible=false;
	}

	void _OnBt_level1_map1(){
		Visible=false;
		ech.active=false;
		level_1Script.setMap1();
	}
}
