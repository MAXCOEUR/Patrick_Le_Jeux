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

	private Vector2 OldDirectionCurrent;
	public Vector2 directionCurrent;

	abstract public void lessEtat();
	abstract public void setEtat(int i);

	abstract protected void OnCollision(Area2D otherArea);
	abstract protected void On_animation_finish(string anim_name);

	protected Vector2 scaleAbsolute;

	Shape2D shape;
	protected Vector2 size;


	public Area2D GetArea2D(){
		return area;
	}

	public override void _Ready()
	{
		shape = GetNode<CollisionShape2D>("CollisionShape2D").Shape;
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

		OldDirectionCurrent = Position;


	}

	public Vector2 getSize(){
		return size;
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

		if (Velocity.X < 0)
		{
			return direction.X * parametreLevel.Acceleration * (1+parametreLevel.Friction);
		}

		return direction.X * parametreLevel.Acceleration;

	}

	protected float goLeft(Vector2 direction)
	{
		sprite.FlipH = true;

		if (Velocity.X > 0)
		{
			return direction.X * parametreLevel.Acceleration * (1 + parametreLevel.Friction);
		}

		return direction.X * parametreLevel.Acceleration;
	}
	public void setParam(ParametreLevel parametreLevel)
	{
		this.parametreLevel = parametreLevel;
	}
	

	public override void _PhysicsProcess(double delta)
	{
		size = shape.GetRect().Size;
		Vector2 tmpDir = Position-OldDirectionCurrent;
		OldDirectionCurrent = Position;
		tmpDir.Normalized();
		directionCurrent = tmpDir.Clamp(-Vector2.One,Vector2.One);

		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += parametreLevel.Gravity * (float)delta;


		velocity.X = Mathf.Clamp(velocity.X, -parametreLevel.VitesseMax, parametreLevel.VitesseMax);
		Velocity = velocity;
		MoveAndSlide();

	}
}
