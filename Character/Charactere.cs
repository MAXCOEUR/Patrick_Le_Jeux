using Godot;
using System;

public abstract partial class Charactere : CharacterBody2D
{
	protected int etat=1;
	protected ParametreLevel parametreLevel = new ParametreLevel();
	protected Sprite2D sprite;
	protected Area2D area;
	protected AnimationPlayer annimation;
	protected RayCast2D raycastLeft ;
	protected RayCast2D raycastRight;
	protected RayCast2D raycastBot;
	protected Vector2 directionDeplacment = new Vector2(-1,0);

	abstract public void lessEtat();
	abstract public void setEtat(int i);

	abstract protected void OnCollision(Area2D otherArea);
	abstract protected void On_animation_finish(string anim_name);

	protected Vector2 scaleAbsolute;




	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		annimation = GetNode<AnimationPlayer>("AnimationPlayer");
		annimation.Connect("animation_finished", new Callable(this, "On_animation_finish"));
		setEtat(1);

		area = GetNode<Area2D>("Area2D");
		area.Connect("area_entered", new Callable(this, "OnCollision"));

		raycastLeft = GetNode<RayCast2D>("RayCast2D_left");
		raycastRight = GetNode<RayCast2D>("RayCast2D_right");
		raycastBot = GetNode<RayCast2D>("RayCast2D_bot");
		scaleAbsolute = Scale;


    }

	public bool isOnWall(){
		if (isOnLeftWall()|| isOnRightWall())
		{
			return true;
		}
		return false;
	}
	public bool isOnLeftWall(){
		return raycastLeft.IsColliding();
	}
	public bool isOnRightWall(){
		return raycastRight.IsColliding();
	}

	protected float goRight(Vector2 direction)
	{
		sprite.FlipH = false;

		return direction.X * parametreLevel.Acceleration;

	}

	protected float goLeft(Vector2 direction)
	{
		sprite.FlipH = true;


		return direction.X * parametreLevel.Acceleration;
	}
	public void setParam(ParametreLevel parametreLevel)
	{
		this.parametreLevel = parametreLevel;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += parametreLevel.Gravity * (float)delta;


		velocity.X = Mathf.Clamp(velocity.X, -parametreLevel.VitesseMax, parametreLevel.VitesseMax);
		Velocity = velocity;
		MoveAndSlide();

	}
}
