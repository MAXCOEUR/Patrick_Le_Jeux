using Godot;
using System;

public partial class Pamouk : CharacterBody2D
{

	private float jumpBase = -400.0f;
	private float jumpHold = -15.0f;
	private float maxJumpTime = 500;
	private float VitesseMax = 100.0f;
	private float Friction = 0.92f;
	private float Acceleration = 300.0f;
	private float Gravity = 1000.0f;
	int i=0;

	private Vector2 velocity = Vector2.Zero;
	private Sprite2D sprite;
	Area2D area;

	bool isLeft=true;
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		AddToGroup("enemies");
		setEtat(1);
		area = GetNode<Area2D>("Area2D");
		area.Connect("area_entered", new Callable(this, "OnCollision"));
	}
	void OnCollision(Area2D otherArea) {
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("enemies")) {
			isLeft=!isLeft;
		}
	}
	public void setEtat(int i){
		switch (i){
			case 0:
				//ded
			break;
			case 1:
				//normal
			break;
		}
	}

	private void goRight()
	{
		sprite.FlipH = true;
		velocity.X += 10;
	}
	private void goLeft()
	{
		sprite.FlipH = false;
		velocity.X -= 10;
	}

	public override void _PhysicsProcess(double delta){
		
		if(isLeft){
			goLeft();
		}else{
			goRight();
		}
		
		// Appliquer la gravité

		if (!Input.IsActionPressed("ui_right") && !Input.IsActionPressed("ui_left"))
		{
			velocity.X *= Friction;
		}
		velocity.X = Mathf.Clamp(velocity.X, -VitesseMax, VitesseMax);
		velocity.Y += Gravity * (float)delta;
		MoveAndCollide(velocity);


	}
}
