using Godot;
using System;

public partial class fireball : Object
{
	private DateTime startLunch;
	private int TimeLife = 10;
	AudioStreamPlayer2D lunch;
	public override void _Ready()
	{
		base._Ready();
		lunch=GetNode<AudioStreamPlayer2D>("lunch");
		startLunch = DateTime.Now;
		parametreLevel.VitesseMax = VitesseMaxDefault * 2;
		if(waitPlayer){
			parametreLevel.Gravity = new ParametreLevel().Gravity;
		}
		lunch.Play();
	}
	public override void _Process(double delta)
	{
		Velocity = new Vector2(parametreLevel.VitesseMax * direction.X, parametreLevel.VitesseMax * direction.Y);
		base._Process(delta);

		if (!modePlayer)
		{
			KinematicCollision2D collision = MoveAndCollide(direction, true);
			if (collision != null)
			{
				setEtat(0);
			}
		}


		if (!modePlayer && !waitPlayer)
		{
			TimeSpan duration = DateTime.Now.Subtract(startLunch);
			if (duration.TotalSeconds > TimeLife)
			{
				setEtat(0);
			}
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
		etat = i;
		switch (i)
		{
			case 0:
				direction = new Vector2(0, 0);
				this.QueueFree();
				break;
			case 1:
				//normal
				break;
		}
	}
	protected override bool OnCollision(Area2D otherArea)
	{
		if(!base.OnCollision(otherArea)){
			return false;
		}
		var otherParent = otherArea.GetParent();
		if (waitPlayer)
		{
			if (otherParent.IsInGroup("player"))
			{
				Patrick player = (Patrick)otherParent;
				player.setEtat(5);
				player.Powerup.Play();
				setEtat(0);
			}
		}
		else
		{
			if (otherParent.IsInGroup("player"))
			{
				Patrick player = (Patrick)otherParent;
				if (!player.isInvincible)
				{
					player.lessEtat();
				}
			}
			else if (otherParent.IsInGroup("enemies"))
			{
				GD.Print("collison epee with enemies "+otherParent);
				Charactere player = (Charactere)otherParent;
				player.lessEtat();
			}
		}
		return true;
	}
	public override void setModePlayer(Charactere user)
	{
		base.setModePlayer(user);
		parametreLevel.Gravity = 0f;
	}
	public override void setdirection(Vector2 dir, Charactere user=null)
	{
		base.setdirection(dir, user);
		parametreLevel.Gravity = 0f;
	}
}
