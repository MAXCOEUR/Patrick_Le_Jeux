using Godot;
using System;

public partial class Among_us_vert : Charactere
{
	private ParametreLevel parametreLevel = new ParametreLevel();

	private Sprite2D sprite;
	private TileMap tileMap;

	protected int life=-1;
	protected private float currentVitess = 0;
	protected int difTimeChangeDir = 2;
	private DateTime lastChange= DateTime.UtcNow;
	AnimationPlayer annimation ;

	private int etat =1;
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		setEtat(1);
		tileMap = GetNode<TileMap>("../TileMap");
		annimation = GetNode<AnimationPlayer>("AnimationPlayer");
		annimation.Connect("animation_finished", new Callable(this, "On_animation_finish"));

		parametreLevel.VitesseMax=100;
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

	private void On_animation_finish(string anim_name) {
		if(anim_name=="mort"){
			this.QueueFree();
		}
	}

	private void goRight(Vector2 velocity,Vector2 direction)
	{
		sprite.FlipH = false;

		if (velocity.X<0)
		{
			currentVitess *= parametreLevel.Friction;
		}

		currentVitess = Mathf.Clamp(currentVitess + direction.X * parametreLevel.Acceleration,-parametreLevel.VitesseMax,parametreLevel.VitesseMax);

	}

	private void goLeft(Vector2 velocity,Vector2 direction)
	{
		sprite.FlipH = true;

		if (velocity.X>0)
		{
			currentVitess *= parametreLevel.Friction;
		}
		
		currentVitess = Mathf.Clamp(currentVitess + direction.X * parametreLevel.Acceleration,-parametreLevel.VitesseMax,parametreLevel.VitesseMax);
	}

	public override void _PhysicsProcess(double delta){
		parametreLevel.VitesseMax=100;
		Vector2 velocity = Velocity;
		if (!IsOnFloor())
			velocity.Y += parametreLevel.Gravity * (float)delta;

		Vector2 direction = new Vector2(0,0);

		if (isOnWall()&&DateTime.UtcNow.Second-lastChange.Second>=difTimeChangeDir){
			lastChange= DateTime.UtcNow;
			life = (life==1)?-1:1;
		}
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
		GD.Print(parametreLevel.VitesseMax);
		velocity.X=currentVitess;

		Velocity = velocity;
		MoveAndSlide();
	}
}
