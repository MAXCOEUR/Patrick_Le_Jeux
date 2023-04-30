using Godot;
using System;

public partial class Patrick : Charactere
{
	private long currentJumpTime = 0;
	private bool isJumping = false;
	public bool isInvincible = false;

	private Object objet;

	private DateTime lastWallLeftJump = DateTime.UtcNow;
	private DateTime lastWallRightJump = DateTime.UtcNow;

	private DateTime lastShootMissile = DateTime.UtcNow;

	CollisionShape2D collisionShapePlayer;
	bool directionPlayerWantIsLeft=false;

	public int getEtat(){
		return etat;
	}
	public override void _Ready()
	{
		base._Ready();
		collisionShapePlayer = GetNode<CollisionShape2D>("CollisionShape2D");
	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_attack"))
		{
			if (objet is epee)
			{
				objet.attack_player(directionPlayerWantIsLeft);
			}
			else if (objet is missile)
			{
				TimeSpan durationLastShootMissile = DateTime.Now.Subtract(lastShootMissile);
				if (durationLastShootMissile.TotalSeconds > 1)
				{
					lastShootMissile = DateTime.Now;
					PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://Objet/missile.tscn");
					missile missile = (missile)missileScene.Instantiate();
					missile.Position = Position;

					GetParent().AddChild(missile);

					if (directionCurrent.X < 0)
					{
						missile.setdirection(new Vector2(-1, 0), this);
					}
					else
					{
						missile.setdirection(new Vector2(1, 0), this);
					}
				}


			}

		}
	}

	override protected void OnCollision(Area2D otherArea)
	{
		Vector2 velocity = Velocity;
		var otherParent = otherArea.GetParent();
		
		if (otherParent.IsInGroup("enemies"))
		{
			if (!isInvincible)
			{
				Charactere enemie = (Charactere)otherParent;
				float piedPatrick = Position.Y + (getSize()*Scale/2).Y -5 ;
				float teteEnemie = enemie.Position.Y - (enemie.getSize()*enemie.Scale/2).Y +5;

				if (piedPatrick <= teteEnemie)
				{
					annimation.Play("saut_sur_ennmie");
					Velocity = new Vector2(Velocity.X, parametreLevel.jumpBase);
					
					enemie.lessEtat();
				}
			}
		}
		else if (otherParent.IsInGroup("ObjectEnemie"))
		{
			if (!isInvincible)
			{
				Object enemie = (Object)otherParent;
				if (enemie.user != this)
				{

					float piedPatrick = Position.Y + (getSize()*Scale/2).Y -5;
					float teteEnemie = enemie.Position.Y - (enemie.getSize()*enemie.Scale/2).Y+5;

					if (piedPatrick <= teteEnemie)
					{
						if (!enemie.waitPlayer)
						{
							annimation.Play("saut_sur_ennmie");
							Velocity = new Vector2(Velocity.X, parametreLevel.jumpBase);

							enemie.lessEtat();
						}
					}

				}

			}
		}
		else if (otherParent.IsInGroup("fin")){
			annimation.Play("fin");
			parametreLevel.setDed();
		}
	}
	override public void lessEtat()
	{
		isInvincible = true;
		annimation.Play("hit");
		switch (etat)
		{
			case 1:
				setEtat(0);
				break;
			case 2:
				setEtat(1);
				break;
			case 3:
				setEtat(2);
				break;
			case 4:
				setEtat(2);
				break;
			case 5:
				setEtat(2);
				break;
			case 6:
				setEtat(0);
				break;
			case 7:
				setEtat(2);
				break;
		};
	}

	void resetObjet()
	{
		try
		{
			objet.setEtat(0);
		}
		catch
		{

		}

		objet = null;
	}
	override public void setEtat(int i)
	{
		Vector2 newScale;
		etat = i;
		int o = 0;
		switch (i)
		{
			case 0: //ded
				annimation.Play("mort");
				parametreLevel.setDed();
				break;
			case 1: //petit
				newScale = new Vector2(0.5f, 0.5f);
				this.Scale = newScale;
				resetObjet();
				break;
			case 2: //grand
				newScale = new Vector2(1f, 1f);
				this.Scale = newScale;
				resetObjet();
				break;
			case 3: //missile
				newScale = new Vector2(1f, 1f);
				this.Scale = newScale;
				resetObjet();
				PackedScene missileScene = (PackedScene)ResourceLoader.Load("res://Objet/missile.tscn");
				missile ob = (missile)missileScene.Instantiate();
				objet = ob;
				AddChild(ob);
				ob.setModePlayer(this);
				break;
			case 4: //epee
				newScale = new Vector2(1f, 1f);
				this.Scale = newScale;
				resetObjet();
				PackedScene epeeScene = (PackedScene)ResourceLoader.Load("res://Objet/epee.tscn");
				epee epee = (epee)epeeScene.Instantiate();
				objet = epee;
				AddChild(epee);
				epee.setModePlayer(this);
				break;
			case 5: //fire
				newScale = new Vector2(1f, 1f);
				this.Scale = newScale;
				resetObjet();
				break;
			case 6: //mini
				newScale = new Vector2(0.25f, 0.25f);
				this.Scale = newScale;
				resetObjet();
				break;
			case 7: //invincible
				newScale = new Vector2(1f, 1f);
				this.Scale = newScale;
				resetObjet();
				break;
		}
	}

	override protected void On_animation_finish(string anim_name)
	{
		if (anim_name == "mort")
		{
			GameOver gameOver = GetNode<GameOver>("../Camera2D/GameOver");
			gameOver.setVisible();
			Visible = false;
		}
		else if (anim_name == "hit")
		{
			isInvincible = false;
		}

	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;

		if(objet != null){
			Shape2D shape = collisionShapePlayer.Shape;
			Vector2 size = shape.GetRect().Size;
			if(directionPlayerWantIsLeft){
				objet.setdirectionFlip(true);
				objet.Position = new Vector2(-size.X / 2, 0);
			}
			else{
				objet.setdirectionFlip(false);
				objet.Position = new Vector2(size.X / 2, 0);
			}
		}



		// Handle Jump.
		if (Input.IsActionJustPressed("ui_jump"))
		{

			if (IsOnFloor())
			{
				isJumping = true;
				currentJumpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
				velocity.Y += parametreLevel.jumpBase;
			}
			else if (isOnLeftWall())
			{
				lastWallLeftJump = DateTime.UtcNow;
				sprite.FlipH = false;
				velocity.Y = parametreLevel.Walljump * 1f;
				velocity.X = -parametreLevel.Walljump * 4f;
			}
			else if (isOnRightWall())
			{
				lastWallRightJump = DateTime.UtcNow;
				sprite.FlipH = true;
				velocity.Y = parametreLevel.Walljump * 1f;
				velocity.X = parametreLevel.Walljump * 4f;
			}

		}
		if (isJumping && DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - currentJumpTime < parametreLevel.maxJumpTime && Input.IsActionPressed("ui_jump"))
		{
			velocity.Y += parametreLevel.jumpHold;
		}
		else
		{
			isJumping = false;
			currentJumpTime = 0;
		}
		if (Input.IsActionJustReleased("ui_jump"))
		{
			isJumping = false;
			currentJumpTime = 0;
		}

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (direction != Vector2.Zero)
		{
			TimeSpan durationLastWallRightJump = DateTime.UtcNow.Subtract(lastWallRightJump);
			TimeSpan durationLastWallLeftJump = DateTime.UtcNow.Subtract(lastWallLeftJump);
			if (direction.X > 0 && durationLastWallRightJump.TotalSeconds >= 1)
			{
				directionPlayerWantIsLeft = false;
				velocity.X += goRight(direction);
			}
			else if (direction.X < 0 && durationLastWallLeftJump.TotalSeconds >= 1)
			{
				directionPlayerWantIsLeft=true;
				velocity.X += goLeft(direction);
			}
		}
		else
		{
			velocity.X *= parametreLevel.Friction;
		}




		Velocity = velocity;
	}

}
