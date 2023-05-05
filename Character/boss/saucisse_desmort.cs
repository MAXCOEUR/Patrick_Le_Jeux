using Godot;
using System;
using System.Data.SQLite;
using System.Collections.Generic;

public partial class saucisse_desmort : Enemies
{
	protected level_1Script level_1;
	protected ProgressBar progressBar;
	protected Database db = Database.Instance;
	protected int currentSol;
	protected bool isLeft = true;
	protected bool isUp = true;
	protected Timer timerFire;
	protected Timer timerTeleportation;
	PackedScene fireBallScene;
	List<Vector2> listDirection = new List<Vector2>();
	List<Vector2> listPosition = new List<Vector2>();

	public void setSol(int i)
	{
		currentSol = i;
	}
	public override void _Ready()
	{
		base._Ready();
		progressBar = GetNode<ProgressBar>("../ProgressBar");
		level_1 = GetNode<level_1Script>("../../../level_1");
		fireBallScene = (PackedScene)ResourceLoader.Load("res://Objet/fireball.tscn");
		setEtat(10);
		parametreLevel.Gravity = 0;

		timerFire = new Timer();
		timerFire.WaitTime = 10;
		timerFire.Connect("timeout", new Callable(this, "sendFireBall"));
		AddChild(timerFire);
		timerFire.Start();

		timerTeleportation = new Timer();
		timerTeleportation.WaitTime = 30;
		timerTeleportation.Connect("timeout", new Callable(this, "sendTeleportation"));
		AddChild(timerTeleportation);
		timerTeleportation.Start();

		listDirection.Add(new Vector2(0.1f, -0.90f));
		listDirection.Add(new Vector2(0.4f, -0.60f));
		listDirection.Add(new Vector2(0.70f, -0.30f));
		listDirection.Add(new Vector2(1f, 0f));
		listDirection.Add(new Vector2(0.1f, 0.90f));
		listDirection.Add(new Vector2(0.4f, 0.60f));
		listDirection.Add(new Vector2(0.70f, 0.30f));
		listDirection.Add(new Vector2(-0.1f, -0.90f));
		listDirection.Add(new Vector2(-0.4f, -0.60f));
		listDirection.Add(new Vector2(-0.70f, -0.30f));
		listDirection.Add(new Vector2(-1f, 0f));
		listDirection.Add(new Vector2(-0.1f, 0.90f));
		listDirection.Add(new Vector2(-0.4f, 0.60f));
		listDirection.Add(new Vector2(-0.70f, 0.30f));

		listPosition.Add(new Vector2(10, -90));
		listPosition.Add(new Vector2(40, -60));
		listPosition.Add(new Vector2(70, -30));
		listPosition.Add(new Vector2(100, 0));
		listPosition.Add(new Vector2(10, 90));
		listPosition.Add(new Vector2(40, 60));
		listPosition.Add(new Vector2(70, 30));
		listPosition.Add(new Vector2(-10, -90));
		listPosition.Add(new Vector2(-40, -60));
		listPosition.Add(new Vector2(-70, -30));
		listPosition.Add(new Vector2(-100, 0));
		listPosition.Add(new Vector2(-10, 90));
		listPosition.Add(new Vector2(-40, 60));
		listPosition.Add(new Vector2(-70, 30));

	}
	private void sendFireBall()
	{
		for (int i = 0; i < listDirection.Count; i++)
		{
			fireball fireball = (fireball)fireBallScene.Instantiate();
			fireball.Position = Position + listPosition[i];
			fireball.setdirection(listDirection[i], this);
			GetParent().AddChild(fireball);
		}
	}
	private void sendTeleportation(){
		GD.Print("tele");
		Random rand = new Random();
		Position = new Vector2(rand.Next(100,2500),rand.Next(300,500));
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 direction = Velocity;

		if (isLeft)
		{
			direction += new Vector2(goLeft(new Vector2(-1, 0)), 0);
		}
		else
		{
			direction += new Vector2(goRight(new Vector2(1, 0)), 0);
		}
		if (isUp)
		{
			direction += new Vector2(0, goUp(new Vector2(0, -1)));
		}
		else
		{
			direction += new Vector2(0, goDown(new Vector2(0, 1)));
		}

		if (currentSol == 1)
		{
			if (Position.X < 100)
			{
				isLeft = false;
			}
			else if (Position.X > 2500)
			{
				isLeft = true;
			}
			if (Position.Y > 500)
			{
				isUp = true;
			}
			else if (Position.Y < 400)
			{
				isUp = false;
			}
		}
		else if (currentSol == 2)
		{
			if (Position.X < 100)
			{
				isLeft = false;
			}
			else if (Position.X > 2500)
			{
				isLeft = true;
			}
			if (Position.Y > 400)
			{
				isUp = true;
			}
			else if (Position.Y < 300)
			{
				isUp = false;
			}
		}
		else
		{
			if (Position.X < 500)
			{
				isLeft = false;
			}
			else if (Position.X > 2000)
			{
				isLeft = true;
			}
			if (Position.Y > 500)
			{
				isUp = true;
			}
			else if (Position.Y < 300)
			{
				isUp = false;
			}
		}

		Velocity = direction;
	}
	public override void lessEtat()
	{

		switch (etat)
		{
			case 1:
				setEtat(0);
				break;
			case 2:
				setEtat(1);
				break;
			case 3:
				setEtat(2);
				break;
			case 4:
				setEtat(3);
				break;
			case 5:
				setEtat(4);
				break;
			case 6:
				setEtat(5);
				break;
			case 7:
				setEtat(6);
				break;
			case 8:
				setEtat(7);
				break;
			case 9:
				setEtat(8);
				break;
			case 10:
				setEtat(9);
				break;
		};
	}

	public override void setEtat(int i)
	{
		etat = i;
		if (progressBar != null)
		{
			progressBar.Value = etat;
		}
		switch (i)
		{
			case 0:
				directionDeplacment = new Vector2(0, 0);
				annimation.Play("mort");
				break;
			case 1:
				annimation.Play("prend_un_cout");
				break;
			case 2:
				annimation.Play("prend_un_cout");
				break;
			case 3:
				annimation.Play("enervement");
				break;
			case 4:
				annimation.Play("prend_un_cout");
				break;
			case 5:
				annimation.Play("prend_un_cout");
				break;
			case 6:
				annimation.Play("prend_un_cout");
				break;
			case 7:
				annimation.Play("prend_un_cout");
				break;
			case 8:
				annimation.Play("prend_un_cout");
				break;
			case 9:
				annimation.Play("prend_un_cout");
				break;
			case 10:
				break;
		}
	}

	protected override void On_animation_finish(string anim_name)
	{
		if (anim_name == "mort")
		{
			this.QueueFree();
			level_1.Finish_map_Boss_5();

			string insertQuery = "INSERT INTO Collection(numeroMap, numeroCollection) VALUES(@numeroMap, @numeroCollection)";
			SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);

			insertCommand.Parameters.AddWithValue("@numeroCollection", 1);
			insertCommand.Parameters.AddWithValue("@numeroMap", 5);

			insertCommand.ExecuteNonQuery();

		}
	}

	protected float goUp(Vector2 direction)
	{
		if (Velocity.Y > 0)
		{
			return direction.Y * parametreLevel.Acceleration * (1 + parametreLevel.Friction);
		}
		return direction.Y * parametreLevel.Acceleration;
	}
	protected float goDown(Vector2 direction)
	{
		if (Velocity.Y < 0)
		{
			return direction.Y * parametreLevel.Acceleration * (1 + parametreLevel.Friction);
		}
		return direction.Y * parametreLevel.Acceleration;
	}
}
