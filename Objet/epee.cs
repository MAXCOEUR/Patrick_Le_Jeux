using Godot;
using System;


public partial class epee : Object
{

	protected System.Timers.Timer timerFire;
	int vitessLance = 300;
	Vector2 size;


	public override void _Ready()
	{
		base._Ready();
		size = GetViewportRect().Size;
		parametreLevel.VitesseMax = vitessLance;
	}
	public override void setModePlayer(Charactere user)
	{
		base.setModePlayer(user);
		Scale=new Vector2(0.5f,0.5f);
		parametreLevel.Gravity=0f;
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public void lanceRight()
	{
		Velocity = new Vector2(vitessLance, -vitessLance);

		annimation.Play("attaque");
	}
	public void lanceLeft()
	{
		Velocity = new Vector2(-vitessLance, -vitessLance);

		annimation.Play("attaque");
	}
	public override void lessEtat()
	{
	}

	public override void setEtat(int i)
	{
		etat = i;
		switch (i)
		{
			case 0:
				annimation.Play("mort");
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
				Charactere player = (Charactere)otherParent;
				player.lessEtat();
			}
		}
		return true;
	}

	protected override void On_animation_finish(string anim_name)
	{
		base.On_animation_finish(anim_name);
		if (anim_name == "attaque")
		{
			annimation.Play("attaque");
		}
	}
	public override void setWaitPlayer(Charactere user)
	{
		base.setWaitPlayer(user);
		parametreLevel.VitesseMax = VitesseMaxDefault;
	}
}
