using Godot;
using System;

public abstract partial class Object : CharacterBody2D
{
	protected Vector2 direction;
	protected Sprite2D sprite;
	protected Area2D area;
	protected AnimationPlayer annimation;
	protected int etat = 1;
	public Charactere user;

	protected const int VitesseMaxDefault=75;

	public bool waitPlayer=true;
	protected bool modePlayer=false;
	Shape2D shape;
	protected Vector2 size;

	protected ParametreLevel parametreLevel = new ParametreLevel();
	public override void _Ready()
	{
		shape = GetNode<CollisionShape2D>("CollisionShape2D").Shape;
		parametreLevel.VitesseMax = VitesseMaxDefault;
		if(waitPlayer){
			direction = new Vector2(0, 0);
		}
		sprite = GetNode<Sprite2D>("Sprite2D");
		annimation = GetNode<AnimationPlayer>("AnimationPlayer");
		annimation.Connect("animation_finished", new Callable(this, "On_animation_finish"));

		area = GetNode<Area2D>("Area2D");
		area.Connect("area_entered", new Callable(this, "OnCollision"));
	}

	public void attack_player(bool isLeft){
		if(isLeft){
			attaqueLeft();
		}
		else{
			attaqueRight();
		}
	}
	public void attaqueRight()
	{
		annimation.Play("attaque_player_right");
	}
	public void attaqueLeft()
	{
		annimation.Play("attaque_player_left");
	}

	public void setdirectionFlip(bool isLeft){
		if(isLeft){
			sprite.FlipH=true;
		}
		else{
			sprite.FlipH=false;
		}
	}
	public virtual void setModePlayer(Charactere user){
		this.user=user;
		modePlayer=true;
	}

	protected virtual void On_animation_finish(string anim_name){
		if (anim_name == "mort")
		{
			this.QueueFree();
		}
	}
	protected virtual bool OnCollision(Area2D otherArea){
		var otherParent = otherArea.GetParent();
		if(otherParent==user){
			return false;
		}
		return true;
	}
	abstract public void lessEtat();
	abstract public void setEtat(int i);
	public virtual void setdirection(Vector2 dir,Charactere user)
	{
		this.user=user;
		this.direction = dir;
		waitPlayer=false;
		modePlayer=false;
	}

	public Vector2 getSize(){
		return size;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		size = shape.GetRect().Size;
		Vector2 velocity = Velocity;
		if(!(this is missile))
		{
			velocity.Y += parametreLevel.VitesseMax * direction.Y;
			velocity.X += parametreLevel.VitesseMax * direction.X;
			velocity.X = Mathf.Clamp(velocity.X, -parametreLevel.VitesseMax, parametreLevel.VitesseMax);
		}
		

		if (!IsOnFloor())
			velocity.Y += parametreLevel.Gravity * (float)delta;


		
		Velocity = velocity;
		
		MoveAndSlide();
	}
}
