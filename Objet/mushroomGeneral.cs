using Godot;
using System;

public partial class mushroomGeneral : Object
{
	protected RayCast2D raycastLeft;
	protected RayCast2D raycastRight;
	protected RayCast2D raycastBot;
	private DateTime lastChange= DateTime.UtcNow;
	protected int difTimeChangeDir = 300;
	public override void _Ready()
	{
		base._Ready();
		raycastLeft = GetNode<RayCast2D>("RayCast2D_left");
		raycastRight = GetNode<RayCast2D>("RayCast2D_right");
		raycastBot = GetNode<RayCast2D>("RayCast2D_bot");
		direction = new Vector2(1, 0);
	}

	protected override bool OnCollision(Area2D otherArea){
		if (base.OnCollision(otherArea)){
			return false;
		}
		return true;
	}

	protected void changeDirection()
	{
		direction.X = -direction.X;
	}
	public bool isOnWall()
	{
		if (isOnLeftWall() || isOnRightWall())
		{
			return true;
		}
		return false;
	}
	public bool isOnLeftWall()
	{
		return raycastLeft.IsColliding();
	}
	public bool isOnRightWall()
	{
		return raycastRight.IsColliding();
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		TimeSpan duration = DateTime.UtcNow.Subtract(lastChange);
		if (isOnWall() && duration.TotalMilliseconds >= difTimeChangeDir)
		{
			lastChange = DateTime.UtcNow;
			changeDirection();
		}
	}
	public override void lessEtat()
	{
		switch (etat)
		{
			case 1:
				setEtat(0);
				break;
		};
	}

	public override void setEtat(int i)
	{
		etat = i;
		switch (i)
		{
			case 0:
				direction = new Vector2(0, 0);
				this.QueueFree();
				break;
			case 1:
				//normal
				break;
		}
	}
}
