using Godot;
using System;

public partial class mushroom_mini : mushroomGeneral
{
	protected override bool OnCollision(Area2D otherArea){
		if (base.OnCollision(otherArea)){
			return false;
		}


		var otherParent = otherArea.GetParent();
		GD.Print("collision");
		if (otherParent.IsInGroup("player"))
		{
			Patrick player = (Patrick)otherParent;
			player.setEtat(6);
			player.Powerup.Play();
			lessEtat();
		}


		return true;
	}
}
