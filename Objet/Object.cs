using Godot;
using System;

public abstract partial class Object : CharacterBody2D
{
	protected Vector2 direction;
	protected Sprite2D sprite;
    protected Area2D area;
    protected AnimationPlayer annimation;
	protected int etat = 1;

    protected ParametreLevel parametreLevel = new ParametreLevel();
	public override void _Ready()
	{
		parametreLevel.VitesseMax = 75;
		direction = new Vector2(1, 0);
		sprite = GetNode<Sprite2D>("Sprite2D");
		annimation = GetNode<AnimationPlayer>("AnimationPlayer");
		annimation.Connect("animation_finished", new Callable(this, "On_animation_finish"));

        area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", new Callable(this, "OnCollision"));
    }

	abstract protected void On_animation_finish(string anim_name);
    abstract protected void OnCollision(Area2D otherArea);
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
