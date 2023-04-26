using Godot;
using System;


public partial class epee : Object
{

	protected System.Timers.Timer timerFire;
	int vitessLance = 300;
	Vector2 size;

	Charactere user;
	public override void _Ready()
	{
		base._Ready();
		size = GetViewportRect().Size;
		parametreLevel.VitesseMax = vitessLance;
	}

	public void setUser(Charactere user){
		this.user=user;
	}
	public void setModePlayer()
	{
		parametreLevel.Gravity = 0;
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
	public void attaqueRight()
	{
		setModePlayer();
		annimation.Play("attaque_player_right");
	}
	public void attaqueLeft()
	{
		setModePlayer();
		annimation.Play("attaque_player_left");
	}

	public override void lessEtat()
	{
		switch (etat)
		{
		};
	}

	public override void setEtat(int i)
	{
		switch (i)
		{
			case 1:
				//normal
				break;
		}
	}

	protected override void OnCollision(Area2D otherArea)
	{
		var otherParent = otherArea.GetParent();
		if(otherParent==user){
			return;
		}
		if (otherParent.IsInGroup("player"))
		{
			Patrick player = (Patrick)otherParent;
			if (!player.isInvincible)
			{
				player.lessEtat();
			}
		}
		if (otherParent.IsInGroup("enemies"))
		{
			Charactere player = (Charactere)otherParent;
			player.lessEtat();
		}
	}

	protected override void On_animation_finish(string anim_name)
	{
		if(anim_name == "attaque")
		{
			annimation.Play("attaque");
		}
	}
}
