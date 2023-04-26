using Godot;
using System;
using System.Reflection;
using System.Threading;
using System.Timers;

public partial class tank : Charactere
{

	protected Patrick patrick;
	protected Sprite2D canon;
	protected Sprite2D tankSprite;

	protected System.Timers.Timer timerFire;
	Vector2 direction = new Vector2(1,0);
	Vector2 distance = new Vector2(1, 0);


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
		var otherParent = otherArea.GetParent();

		
		if (otherParent.IsInGroup("player"))
		{
			Patrick player = (Patrick)otherParent;
			if (!player.isInvincible)
			{
				if (player.Velocity.Y <= 0)
				{
					player.lessEtat();
				}
			}
		}
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

		Vector2 tankPosition = Position;
		Vector2 patrickPosition = patrick.Position;

		distance = new Vector2(patrickPosition.X - tankPosition.X, patrickPosition.Y - tankPosition.Y);
		Vector2 directiontmp = distance.Normalized();
		direction = directiontmp.Clamp(-Vector2.One, Vector2.One);
		if (direction.X<0)
		{
			tankSprite.FlipH = false;
		}
		else
		{
			tankSprite.FlipH = true;
		}

		canon.LookAt(patrickPosition);
	}

	public void send(object source, System.Timers.ElapsedEventArgs e)
	{
		if (distance.Length() < 1000)
		{
			PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://Objet/missile.tscn");
			// Créer une nouvelle instance du missile à partir de la scène préfabriquée
			missile missile = (missile)missileScene.Instantiate();
			// Définir la position initiale du missile à la position actuelle du tank
			missile.Position = Position;


			//missile.setdirection(direction);
			GetParent().AddChild(missile);
			missile.setdirection(direction);
		}
		
	}
}
