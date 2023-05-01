using Godot;
using System;

public partial class moving_platform_h : CharacterBody2D
{
	protected bool isleft=false;

	protected Vector2 startPosition;
	protected Vector2 endPosition;
	public override void _Ready(){

		startPosition = Position;
		endPosition = Position;
	}
	public override void _Process(double delta){

		if(Position.X <=startPosition.X)
		{
			isleft=false;
		}
		else if(Position.X>=endPosition.X)
		{
			isleft=true;
		}

		Vector2 velocity = new Vector2(0,0);

		if(isleft){
			velocity.X=-2;
		}else{
			velocity.X=2;
		}

		MoveAndCollide(velocity);
	}
}
