using Godot;
using System;

public abstract partial class Charactere : CharacterBody2D
{
	//private int etat=1;
	abstract public void lessEtat();
	abstract public void setEtat(int i);

	public bool isOnWall(){
		if (isOnLeftWall()|| isOnRightWall())
		{
			return true;
		}
		return false;
	}
	public bool isOnLeftWall(){
		RayCast2D raycastLeft = GetNode<RayCast2D>("RayCast2D_side_left");
		return raycastLeft.IsColliding();
	}
	public bool isOnRightWall(){
		RayCast2D  raycastRight = GetNode<RayCast2D>("RayCast2D_side_right");
		return raycastRight.IsColliding();
	}
	
}
