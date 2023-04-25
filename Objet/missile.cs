using Godot;
using System;

public partial class missile : Object
{
	public override void _Ready()
	{
		base._Ready();
		setdirection(new Vector2(1, -1));
	}
	public override void _Process(double delta)
	{
		base._Process(delta);

		LookAt(Position+direction);

		KinematicCollision2D collision = MoveAndCollide(direction,true);
		if(collision != null)
		{
			setEtat(-1);
		}
		
	}

	protected override void On_animation_finish(string anim_name)
	{
		if (anim_name == "mort")
		{
			this.QueueFree();
		}
		if (anim_name == "explosion")
		{
			this.QueueFree();
		}
	}

	public override void lessEtat()
	{
		switch (etat)
		{
			case 1:
				setEtat(0);
				break;
		};
	}

	public override void setEtat(int i)
	{
		switch (i)
		{
			case 0:
				direction = new Vector2(0, 0);
				annimation.Play("mort");
				break;
			case 1:
				//normal
				break;
			case -1:
				direction = new Vector2(0, 0);
				annimation.Play("explosion");
				break;
		}
	}
}
