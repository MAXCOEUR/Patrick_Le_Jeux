using Godot;
using System;
using System.Data.SQLite;

public partial class thierry : Among_us_vert
{
	protected System.Timers.Timer timerFire;

	protected ProgressBar progressBar;
	protected level_1Script level_1;
	protected Database db = Database.Instance;

	public override void _Ready()
	{
		base._Ready();
		progressBar = GetNode<ProgressBar>("../ProgressBar");
		level_1 = GetNode<level_1Script>("../../../level_1");

		setEtat(5);
		timerFire = new System.Timers.Timer(2000);
		timerFire.Elapsed += (timerSender, timerEvent) => sendFireball();
		timerFire.AutoReset = true;
		timerFire.Enabled = true;
	}
	public void sendFireball()
	{
		PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://Objet/fireball.tscn");
		fireball fireball = (fireball)missileScene.Instantiate();
		fireball.Position = Position;

		Vector2 direction;
		Random random = new Random();
		float[] possibleValues = new float[] { -0.5f, -0.25f, 0f, 0.25f, 0.5f };
		if (directionDeplacment.X < 0)
		{
			direction = new Vector2(-1, possibleValues[random.Next(possibleValues.Length)]);
		}
		else
		{
			direction = new Vector2(1, possibleValues[random.Next(possibleValues.Length)]);
		}

		fireball.setdirection(direction, this);
		GetParent().AddChild(fireball);
		

	}
	override public void setEtat(int i)
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
				sprite.Modulate = new Color(1, 0, 0);
				annimation.Play("enervement");

				timerFire.Interval = timerFire.Interval / 4 * 3;
				break;
			case 3:
				annimation.Play("prend_un_cout");
				Scale = new Vector2(1, 1);
				break;
			case 4:
				annimation.Play("prend_un_cout");
				Scale = new Vector2(1.25f, 1.25f);
				break;
			case 5:
				Scale = new Vector2(1.5f, 1.5f);
				break;
		}
	}
	override protected void On_animation_finish(string anim_name)
	{
		base.On_animation_finish(anim_name);
		if (anim_name == "mort")
		{
			string insertQuery = "INSERT INTO Collection(numeroMap, numeroCollection) VALUES(@numeroMap, @numeroCollection)";
			SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);

			insertCommand.Parameters.AddWithValue("@numeroCollection", 1);
			insertCommand.Parameters.AddWithValue("@numeroMap", 1);//ajout de pouet a la collection

			insertCommand.ExecuteNonQuery();

			level_1.Finish_map_Boss_1();
		}
	}
	override public void lessEtat()
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
		};
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
}
