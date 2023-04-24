using Godot;
using System;

public abstract partial class Charactere : CharacterBody2D
{
	protected int etat=1;
	protected float currentVitess = 0;
	protected ParametreLevel parametreLevel = new ParametreLevel();
	protected Sprite2D sprite;
	protected Area2D area;
	protected AnimationPlayer annimation;
	protected RayCast2D raycastLeft ;
	protected RayCast2D raycastRight;
	protected RayCast2D raycastBot;

	abstract public void lessEtat();
	abstract public void setEtat(int i);

	abstract protected void OnCollision(Area2D otherArea);
	abstract protected void On_animation_finish(string anim_name);




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

	protected void goRight(Vector2 velocity, Vector2 direction)
	{
		sprite.FlipH = false;

		if (velocity.X < 0)
		{
			currentVitess *= parametreLevel.Friction;
		}

		currentVitess = Mathf.Clamp(currentVitess + direction.X * parametreLevel.Acceleration, -parametreLevel.VitesseMax, parametreLevel.VitesseMax);

	}

	protected void goLeft(Vector2 velocity, Vector2 direction)
	{
		sprite.FlipH = true;

		if (velocity.X > 0)
		{
			currentVitess *= parametreLevel.Friction;
		}

		currentVitess = Mathf.Clamp(currentVitess + direction.X * parametreLevel.Acceleration, -parametreLevel.VitesseMax, parametreLevel.VitesseMax);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += parametreLevel.Gravity * (float)delta;

		Velocity=velocity;
	}
}
