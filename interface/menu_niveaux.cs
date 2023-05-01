using Godot;
using System;
using System.Data.SQLite;
using System.Collections.Generic;

public partial class menu_niveaux : Control
{
	level_1Script level_1Script;
	echap ech;
	protected Database db = Database.Instance;

	List<Button> buttons = new List<Button>();
	public override void _Ready()
	{
		level_1Script = GetNode<level_1Script>("../../../../level_1");
		ech = GetNode<echap>("../../echap");
		Button tmp = GetNode<Button>("CenterContainer/GridContainer/bt_quitter");
		tmp.Connect("button_down", new Callable(this, "_OnBt_quitter"));
		buttons.Add(tmp);

		tmp = GetNode<Button>("CenterContainer/GridContainer/bt_map_1");
		tmp.Connect("button_down", new Callable(this, "_OnBt_level1_map1"));
		buttons.Add(tmp);

		tmp = GetNode<Button>("CenterContainer/GridContainer/bt_map_2");
		tmp.Connect("button_down", new Callable(this, "_OnBt_level1_map2"));
		buttons.Add(tmp);

		tmp = GetNode<Button>("CenterContainer/GridContainer/bt_map_3");
		tmp.Connect("button_down", new Callable(this, "_OnBt_level1_map3"));
		buttons.Add(tmp);

		tmp = GetNode<Button>("CenterContainer/GridContainer/bt_map_4");
		tmp.Connect("button_down", new Callable(this, "_OnBt_level1_map4"));
		buttons.Add(tmp);

		tmp = GetNode<Button>("CenterContainer/GridContainer/bt_map_5");
		tmp.Connect("button_down", new Callable(this, "_OnBt_level1_map5"));
		buttons.Add(tmp);

		update();

	}

	public void update()
	{
		SQLiteCommand command = new SQLiteCommand("SELECT MAX(numeroMap) FROM MapCurrent", db.getConnection);
		object result = command.ExecuteScalar();

		int maxNumeroMap = 0;
		try
		{
			maxNumeroMap = Convert.ToInt32(result);
		}
		catch
		{
			maxNumeroMap = 0;
		}


		int i = 0;
		foreach (Button button in buttons)
		{
			if (i > maxNumeroMap)
			{
				button.Disabled = true;
			}else{
				button.Disabled = false;
			}
			i++;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	void _OnBt_quitter()
	{
		Visible = false;
	}

	void _OnBt_level1_map1()
	{
		Visible = false;
		ech.active = false;
		level_1Script.setMap1();
	}
	void _OnBt_level1_map2()
	{
		Visible = false;
		ech.active = false;
		level_1Script.setMap2();
	}
}
