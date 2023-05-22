using Godot;
using System;
using System.Data.SQLite;
using System.Collections.Generic;

public partial class pamouk : Among_us_vert
{
	protected Database db = Database.Instance;
	protected ProgressBar progressBar;
	protected level_1Script level_1;

	protected Timer timerFire;
	float oldVitessMax;

	bool isInvincible=false;
	PackedScene missileScene;
	List<Vector2> listDirection = new List<Vector2>();
	List<Vector2> listPosition = new List<Vector2>();

	int probJump =80;
	public override void _Ready()
	{
		base._Ready();
		progressBar = GetNode<ProgressBar>("../ProgressBar");
		level_1 = GetNode<level_1Script>("../../../level_1");
		missileScene = (PackedScene)ResourceLoader.Load("res://Objet/fireball.tscn");

		setEtat(10);

		timerFire = new Timer();
		timerFire.WaitTime = 5;
		timerFire.Connect("timeout", new Callable(this, "send"));
		AddChild(timerFire);
		timerFire.Start();
		
		listDirection.Add(new Vector2(0.1f,-0.90f));
		listDirection.Add(new Vector2(0.4f,-0.60f));
		listDirection.Add(new Vector2(0.70f,-0.30f));
		listDirection.Add(new Vector2(1f,0f));
		listDirection.Add(new Vector2(0.1f,0.90f));
		listDirection.Add(new Vector2(0.4f,0.60f));
		listDirection.Add(new Vector2(0.70f,0.30f));
		listDirection.Add(new Vector2(-0.1f,-0.90f));
		listDirection.Add(new Vector2(-0.4f,-0.60f));
		listDirection.Add(new Vector2(-0.70f,-0.30f));
		listDirection.Add(new Vector2(-1f,0f));
		listDirection.Add(new Vector2(-0.1f,0.90f));
		listDirection.Add(new Vector2(-0.4f,0.60f));
		listDirection.Add(new Vector2(-0.70f,0.30f));

		listPosition.Add(new Vector2(10,-90));
		listPosition.Add(new Vector2(40,-60));
		listPosition.Add(new Vector2(70,-30));
		listPosition.Add(new Vector2(100,0));
		listPosition.Add(new Vector2(10,90));
		listPosition.Add(new Vector2(40,60));
		listPosition.Add(new Vector2(70,30));
		listPosition.Add(new Vector2(-10,-90));
		listPosition.Add(new Vector2(-40,-60));
		listPosition.Add(new Vector2(-70,-30));
		listPosition.Add(new Vector2(-100,0));
		listPosition.Add(new Vector2(-10,90));
		listPosition.Add(new Vector2(-40,60));
		listPosition.Add(new Vector2(-70,30));
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
	override protected void On_animation_finish(string anim_name)
	{
		base.On_animation_finish(anim_name);
		if (anim_name == "mort")
		{
			this.QueueFree();
			level_1.Finish_map_Boss_3();
			string insertQuery = "INSERT INTO Collection(numeroMap, numeroCollection) VALUES(@numeroMap, @numeroCollection)";
			SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);

			insertCommand.Parameters.AddWithValue("@numeroCollection", 1);
			insertCommand.Parameters.AddWithValue("@numeroMap", 3);//ajout de pouet a la collection

			insertCommand.ExecuteNonQuery();
		}
		if (anim_name == "attack")
		{
			parametreLevel.VitesseMax = oldVitessMax;
			isInvincible=false;
		}
		if (anim_name == "prend_un_cout")
		{
			isInvincible=false;
		}
	}

	private void send()
	{
		Random random = new Random();
		int redomNumber = random.Next(100);

		if (redomNumber < probJump)
		{
			if(!annimation.IsPlaying()){
				oldVitessMax = parametreLevel.VitesseMax;
				Vector2 velocity = Velocity;
				parametreLevel.VitesseMax = 1000;
				if(directionDeplacment.X<0){
					velocity.X += parametreLevel.jumpBase;
				}
				else{
					velocity.X -= parametreLevel.jumpBase;
				}
				velocity.Y += parametreLevel.jumpBase;
				Velocity = velocity;
				isInvincible=true;
			
				annimation.Play("attack");
			}
			
			
		}
		else{
			for (int i = 0; i < listDirection.Count; i++)
			{
				fireball fireball = (fireball)missileScene.Instantiate();
				fireball.Position = Position+listPosition[i];
				fireball.setdirection(listDirection[i], this);
				GetParent().AddChild(fireball);
			}
			
		}

	}

	public override void lessEtat()
	{
		if(isInvincible){
			return;
		}
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
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 2:
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 3:
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 4:
				isInvincible=true;
				annimation.Play("enervement");
				probJump=50;
				break;
			case 5:
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 6:
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 7:
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 8:
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 9:
				isInvincible=true;
				annimation.Play("prend_un_cout");
				break;
			case 10:
				break;
		}
	}

}
