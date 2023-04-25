using Godot;
using System;

public abstract partial class Object : CharacterBody2D
{
	protected Vector2 direction;
	protected Sprite2D sprite;
	protected AnimationPlayer annimation;
	protected int etat = 1;

	protected int vitesse = 75;
	public override void _Ready()
	{
		direction = new Vector2(1, 0);
		sprite = GetNode<Sprite2D>("Sprite2D");
		annimation = GetNode<AnimationPlayer>("AnimationPlayer");
		annimation.Connect("animation_finished", new Callable(this, "On_animation_finish"));
	}

	abstract protected void On_animation_finish(string anim_name);
	abstract public void lessEtat();
	abstract public void setEtat(int i);
	public void setdirection(Vector2 dir)
	{
		this.direction = dir;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;

		velocity.Y = vitesse * direction.Y;
		velocity.X = vitesse * direction.X;

		Velocity = velocity;
		MoveAndSlide();
	}
}
