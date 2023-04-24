using Godot;
using System;

public partial class Among_us_vert : Charactere
{

	protected int life=-1;

	protected int difTimeChangeDir = 300;
	private DateTime lastChange= DateTime.UtcNow;

	public override void _Ready()
	{
		base._Ready();

		parametreLevel.VitesseMax=100;
		parametreLevel.Friction = 0;
	}

	override public void setEtat(int i){
		switch (i){
			case 0:
				life=0;
				annimation.Play("mort");
			break;
			case 1:
				//normal
			break;
		}
	}
	override public void lessEtat(){
		switch (etat){
			case 1:
				setEtat(0);
			break;
		};
	}

	override protected void On_animation_finish(string anim_name) {
		if(anim_name=="mort"){
			this.QueueFree();
		}
	}
	override protected void OnCollision(Area2D otherArea)
	{

	}

	protected void changeDirection()
	{
		life = (life == 1) ? -1 : 1;
	}

	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);

		Vector2 velocity = Velocity;

		Vector2 direction = new Vector2(0,0);

		TimeSpan duration = DateTime.UtcNow.Subtract(lastChange);

		
		if(life==-1){
			direction = new Vector2(-1,0);
		}
		else if (life==1){
			direction = new Vector2(1,0);
		}

		if (direction != Vector2.Zero)
		{
			if(direction.X>0){
				goRight(velocity,direction);
			}else{
				goLeft(velocity,direction);
			}
		}
		else
		{
			currentVitess *= parametreLevel.Friction;
		}
		if (isOnWall() && duration.TotalMilliseconds >= difTimeChangeDir)
		{
			lastChange = DateTime.UtcNow;
			changeDirection();
		}
		velocity.X=currentVitess;

		
		Velocity = velocity;
		MoveAndSlide();
	}
}
