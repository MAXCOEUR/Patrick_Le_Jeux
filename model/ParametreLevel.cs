using Godot;
using System;

public partial class ParametreLevel
{
	public float jumpBase;
	public float jumpHold ;
	public float maxJumpTime ;
	public float VitesseMax ;
	public float Friction ;
	public float Acceleration ;
	public float Gravity;
	
	public ParametreLevel(){
		jumpBase = -500.0f;
		jumpHold = -10.0f;
		maxJumpTime = 500;
		VitesseMax = 300.0f;
		Friction = 0.80f;
		Acceleration = 10.0f;
		Gravity = 980.0f;
	}
}
