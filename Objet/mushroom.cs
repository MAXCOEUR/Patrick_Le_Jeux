using Godot;
using System;

public partial class mushroom : mushroomGeneral
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
			if(player.getEtat()<2 ||player.getEtat()==6 ){
				player.setEtat(2);
				player.Powerup.Play();
				lessEtat();
			}
			
		}


		return true;
	}
}
