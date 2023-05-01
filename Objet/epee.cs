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
		parametreLevel.Friction=1f;
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public void lance()
	{
		if(direction.X<0){
			Velocity = new Vector2(-vitessLance, -vitessLance);
		}
		else
		{
			Velocity = new Vector2(vitessLance, -vitessLance);
		}
		

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
			if (otherParent.IsInGroup("player"))
			{
				Patrick player = (Patrick)otherParent;
				player.setEtat(4);
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

	protected override void On_animation_finish(string anim_name)
	{
		base.On_animation_finish(anim_name);
		if (anim_name == "attaque")
		{
			annimation.Play("attaque");
		}
	}
	public override void setModePlayer(Charactere user)
	{
		base.setModePlayer(user);
		Scale=new Vector2(0.5f,0.5f);
		parametreLevel.Gravity=0f;
		waitPlayer=false;
	}
	public override void setdirection(Vector2 dir,Charactere user){
		base.setdirection(dir,user);
		CollisionMask &= ~(uint)48;
	}
}
