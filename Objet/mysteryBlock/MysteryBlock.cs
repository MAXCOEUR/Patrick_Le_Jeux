using Godot;
using System;

public partial class MysteryBlock : StaticBody2D
{
    // Called when the node enters the scene tree for the first time.
    protected AnimationPlayer annimation;
    protected Area2D area;
    CollisionShape2D collisionShapeBlock;
    Vector2 size;

    protected Object Object;
    public override void _Ready()
    {
        collisionShapeBlock = GetNode<CollisionShape2D>("CollisionShape2D");
        Shape2D shape = collisionShapeBlock.Shape;
        size = shape.GetRect().Size;
        annimation = GetNode<AnimationPlayer>("AnimationPlayer");
        area = GetNode<Area2D>("Area2D");
        area.Connect("area_entered", new Callable(this, "OnCollision"));
    }

    protected void OnCollision(Area2D otherArea)
    {
        var otherParent = otherArea.GetParent();
        if (otherParent.IsInGroup("player"))
        {
            Patrick player = (Patrick)otherParent;
            annimation.Play("hit");
            if (Object != null)
            {
                CallDeferred("AddObjectToChild", Object);
                Object = null;
            }
        }
    }

    protected void AddObjectToChild(Object obj)
    {
        obj.Position = new Vector2(obj.Position.X, (obj.Position.Y - size.Y));
        AddChild(obj);
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
