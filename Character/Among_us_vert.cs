using Godot;
using System;

public partial class Among_us_vert : Charactere
{

	

	protected int difTimeChangeDir = 300;
	private DateTime lastChange= DateTime.UtcNow;

	public override void _Ready()
	{
		base._Ready();

		parametreLevel.VitesseMax=50;
		parametreLevel.Friction = 0;
	}

	override public void setEtat(int i){
		switch (i){
			case 0:
				directionDeplacment= new Vector2(0, 0);
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
		
		var otherParent = otherArea.GetParent();
		

		if (otherParent.IsInGroup("player"))
		{
			Patrick player = (Patrick)otherParent;
			if (!player.isInvincible)
			{
				if (player.directionCurrent.Y <= 0)
				{
					player.lessEtat();
				}
			}
		}
	}

	protected void changeDirection()
	{
		directionDeplacment.X = (directionDeplacment.X==1) ? -1 : 1;
	}

	public override void _PhysicsProcess(double delta){
		base._PhysicsProcess(delta);

		Vector2 velocity = Velocity;

		Vector2 direction = new Vector2(0,0);

		TimeSpan duration = DateTime.UtcNow.Subtract(lastChange);

		
		if(base.directionDeplacment.X==-1){
			direction = new Vector2(-1,0);
		}
		else if (base.directionDeplacment.X ==1){
			direction = new Vector2(1,0);
		}

		if (direction != Vector2.Zero)
		{
			if(direction.X>0){
				velocity.X += goRight(direction);
			}else{
				velocity.X += goLeft(direction);
			}
		}
		else
		{
			velocity.X *= parametreLevel.Friction;
		}
		if (isOnWall() && duration.TotalMilliseconds >= difTimeChangeDir)
		{
			lastChange = DateTime.UtcNow;
			changeDirection();
		}

		
		Velocity = velocity;
	}
}
