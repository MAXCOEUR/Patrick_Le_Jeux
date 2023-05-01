using Godot;
using System;
using System.Data.SQLite;
using System.Collections.Generic;

public partial class pascal : Enemies
{
	protected level_1Script level_1;
	protected ProgressBar progressBar;
	protected Database db = Database.Instance;

	protected bool isLeft = true;

	List<Instruction> instructions = new List<Instruction>();
	int indexInstruction = 0;

	protected int currentSol;
	public void setSol(int i){
		isLeft=true;
		currentSol = i;
		if(i==1){
			Position = new Vector2(1150,500);
		}
		else if(i==2){
			Position = new Vector2(630,300);
		}
		else if(i==3){
			Position = new Vector2(630,300);
		}
	}
	public override void _Ready()
	{
		base._Ready();
		setEtat(10);
		progressBar = GetNode<ProgressBar>("../ProgressBar");
		level_1 = GetNode<level_1Script>("../../../level_1");
		parametreLevel.VitesseMax=200;
		parametreLevel.jumpBase=-700;

		instructions.Add(new Instruction(new Vector2(500,270),new Vector2(550,300),"jump"));
		instructions.Add(new Instruction(new Vector2(100,110),new Vector2(110,140),"changeDirection"));
		instructions.Add(new Instruction(new Vector2(730,270),new Vector2(750,300),"jump"));
		instructions.Add(new Instruction(new Vector2(1180,110),new Vector2(1200,140),"changeDirection"));
		instructions.Add(new Instruction(new Vector2(250,420),new Vector2(270,450),"changeDirection"));
		instructions.Add(new Instruction(new Vector2(350,420),new Vector2(400,450),"jump"));
		instructions.Add(new Instruction(new Vector2(1020,420),new Vector2(1100,450),"changeDirection"));
		instructions.Add(new Instruction(new Vector2(920,420),new Vector2(950,450),"jump"));
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 direction = Velocity;

		if(isLeft){
			direction += new Vector2(goLeft(new Vector2(-1,0)),0);
		}
		else{
			direction += new Vector2(goRight(new Vector2(1,0)),0);
		}
		

		if(currentSol==1){
			if(Position.X<100){
				isLeft=false;
			}
			else if(Position.X>1185){
				isLeft=true;
			}
			else if(Position.X<1000 && Position.X>720){
				if(IsOnFloor()){
					direction += new Vector2(0,goJump());;
				}
			}
			else if(Position.X<550 && Position.X>260){
				if(IsOnFloor()){
					direction += new Vector2(0,goJump());;
				}
			}
		}
		else if(currentSol==2){
			if(Position>instructions[indexInstruction].getTop_left() && Position<instructions[indexInstruction].getBot_right()){
				if(instructions[indexInstruction].getAction()=="jump"){
					direction += new Vector2(0,goJump());
					indexInstruction=(indexInstruction+1)%instructions.Count;
				}
				else if(instructions[indexInstruction].getAction()=="changeDirection"){
					isLeft=!isLeft;
					indexInstruction=(indexInstruction+1)%instructions.Count;
				}
			}
		}
		else if(currentSol==3){
			if(Position.X<570){
				isLeft=false;
			}
			else if(Position.X>750){
				isLeft=true;
			}
		}
		

		Velocity=direction;
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
				annimation.Play("prend_un_cout");
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
			level_1.Finish_map_Boss_2();
			
			string insertQuery = "INSERT INTO Collection(numeroMap, numeroCollection) VALUES(@numeroMap, @numeroCollection)";
			SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);

			insertCommand.Parameters.AddWithValue("@numeroCollection", 1);
			insertCommand.Parameters.AddWithValue("@numeroMap", 2);//ajout de pouet a la collection

			insertCommand.ExecuteNonQuery();

		}
	}


}
