using Godot;
using System;

public partial class ZoneGame : Node2D
{

	Area2D zoneGameAera;
	public override void _Ready()
	{
		zoneGameAera= GetNode<Area2D>("Area2D");
		zoneGameAera.Connect("area_exited", new Callable(this, "On_Exited"));
	}

	void On_Exited(Area2D otherArea) {
		var otherParent = otherArea.GetParent();
		
		if (otherParent.IsInGroup("player")) {
			Charactere enemie= (Charactere)otherParent;
			enemie.setEtat(0);
			GD.Print("a dieux player !");
		} else if (otherParent.IsInGroup("enemies")) {
			Charactere enemie= (Charactere)otherParent;
			enemie.setEtat(0);
			GD.Print("a dieux enemies !");
		}
		else if (otherParent.IsInGroup("ObjectEnemie")) {
			Object enemie= (Object)otherParent;
			enemie.setEtat(0);
			GD.Print("a dieux ObjectEnemie !");
		}
		else if (otherParent.IsInGroup("ObjectEnemieInvincible"))
		{
			Object enemie = (Object)otherParent;
			enemie.setEtat(0);
			GD.Print("a dieux ObjectEnemieInvincible !");
		}
	}
}
