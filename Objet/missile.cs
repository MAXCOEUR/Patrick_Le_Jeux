using Godot;
using System;

public partial class missile : Object
{
	public override void _Ready()
	{
		base._Ready();
		parametreLevel.Gravity = 0f;
		setdirection(new Vector2(1, -1));
	}
	public override void _Process(double delta)
	{
		Velocity = new Vector2(parametreLevel.VitesseMax * direction.X, parametreLevel.VitesseMax * direction.Y);
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

	protected override void OnCollision(Area2D otherArea)
	{
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player"))
		{
			Patrick player = (Patrick)otherParent;
			if (!player.isInvincible)
			{
				if (player.Velocity.Y <= 0)
				{
					player.lessEtat();
				}
			}
		}
		if (otherParent.IsInGroup("enemies") && !(otherParent is tank))
		{
			Charactere player = (Charactere)otherParent;
			if (player.Velocity.Y <= 0)
			{
				player.lessEtat();
			}
		}
	}
}
