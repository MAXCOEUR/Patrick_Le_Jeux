using Godot;
using System;

public partial class epee : Object
{

	bool modePlayer = false;
	public override void _Ready()
	{
		base._Ready();
	}

	public void setModePlayer()
	{
		modePlayer = true;
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
	}
	public void attaque()
	{
		annimation.Play("attaque");
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

		}
	}
}
