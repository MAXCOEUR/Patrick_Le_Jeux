using Godot;
using System;

public partial class tank : Charactere
{
	protected int life = 0;

	protected int difTimeLastShoot = 2;
	private DateTime lastShoot = DateTime.UtcNow;

	protected Patrick patrick;
	protected Sprite2D canon;
	protected Sprite2D tankSprite;

	public override void _Ready()
	{
		base._Ready();
		patrick = GetNode<Patrick>("../../Patrick");
		canon = sprite.GetNode<Sprite2D>("Canon");
		tankSprite = sprite.GetNode<Sprite2D>("Tank");

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
		switch (i)
		{
			case 0:
				life = 0;
				annimation.Play("mort");
				break;
			case 1:
				//normal
				break;
		}
	}

	protected override void OnCollision(Area2D otherArea)
	{
		
	}

	protected override void On_animation_finish(string anim_name)
	{
		if (anim_name == "mort")
		{
			this.QueueFree();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		TimeSpan duration = DateTime.UtcNow.Subtract(lastShoot);

		Vector2 tankPosition = Position + canon.Position;
		Vector2 patrickPosition = patrick.Position;


		float angle = (float)Math.Atan2(patrickPosition.Y - tankPosition.Y, patrickPosition.X - tankPosition.X);
		if (angle < -MathF.PI / 2 || angle > MathF.PI / 2)
		{
			tankSprite.FlipH = false;
		}
		else
		{
			tankSprite.FlipH = true;
		}
		angle += MathF.PI;


		canon.Rotation = angle;
		

		MoveAndSlide();
	}
}
