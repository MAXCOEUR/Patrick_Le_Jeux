using Godot;
using System;

public partial class Among_us_rouge : Among_us_vert
{

	private DateTime lastChangeEmpty= DateTime.UtcNow;


	public bool isOnPlatformEnd() {
		RayCast2D raycastLeft = GetNode<RayCast2D>("RayCast2D_left");
		RayCast2D  raycastRight = GetNode<RayCast2D>("RayCast2D_right");
		if(raycastLeft.IsColliding() && raycastRight.IsColliding()){
			return false;
		}
		return true;
	}

	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;
		if((isOnPlatformEnd()) && DateTime.UtcNow.Second-lastChangeEmpty.Second>=difTimeChangeDir){
			lastChangeEmpty= DateTime.UtcNow;
			life = (life==1)?-1:1;
		}
		//GD.Print(isEmptyLeftPlatforme()+" "+isEmptyRightPlatforme());

	}
}
