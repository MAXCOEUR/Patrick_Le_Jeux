using Godot;
using System;
using System.Threading;
using System.Timers;

public partial class tank : Charactere
{

	protected int difTimeLastShoot = 2;
	private DateTime lastShoot = DateTime.UtcNow;

	protected Patrick patrick;
	protected Sprite2D canon;
	protected Sprite2D tankSprite;

	protected System.Timers.Timer timerFire;

	public override void _Ready()
	{
		base._Ready();
		patrick = GetNode<Patrick>("../../Patrick");
		canon = sprite.GetNode<Sprite2D>("Canon");
		tankSprite = sprite.GetNode<Sprite2D>("Tank");
		directionDeplacment = new Vector2(0, 0);

		timerFire = new System.Timers.Timer(5000);
		timerFire.Elapsed += (timerSender, timerEvent) => send(timerSender, timerEvent);
		timerFire.AutoReset = true;
		timerFire.Enabled = true;

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
				directionDeplacment = new Vector2(0,0);
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
	}

	public void send(object source, System.Timers.ElapsedEventArgs e)
	{
		PackedScene packedScene = GD.Load<PackedScene>("../Among_us_rouge.tscn");
		missile missile = (missile)packedScene.GetScript();


		Vector2 directionaled = new Vector2(1, 0);
		missile.setdirection(directionaled);


		GetTree().Root.AddChild(missile);

	}
}
