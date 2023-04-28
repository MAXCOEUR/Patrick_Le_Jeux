using Godot;
using System;

public partial class MysteryBlock : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	protected AnimationPlayer annimation;
	protected Area2D area;

	protected Object Object;
	public override void _Ready()
	{
		annimation = GetNode<AnimationPlayer>("AnimationPlayer");
		area =GetNode<Area2D>("Area2D");
		area.Connect("area_entered", new Callable(this, "OnCollision"));
	}

	protected void OnCollision(Area2D otherArea){
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player"))
		{
			Patrick player = (Patrick)otherParent;
			annimation.Play("hit");
			Object.Position = new Vector2(Object.Position.X,Object.Position.Y-20);
			AddChild(Object);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
