using Godot;
using System;

public partial class missile : Object
{
	private DateTime startLunch;
	private int TimeLife = 10;
	public override void _Ready()
	{
		base._Ready();
		startLunch = DateTime.Now;
		parametreLevel.VitesseMax = VitesseMaxDefault*2;
		CpuParticles2D paticule2DFumée= GetNode<CpuParticles2D>("fumée");
		paticule2DFumée.Emitting=false;
		parametreLevel.Gravity = new ParametreLevel().Gravity;
		waitPlayer=true;
	}
	public override void _Process(double delta)
	{
		Velocity = new Vector2(parametreLevel.VitesseMax * direction.X, parametreLevel.VitesseMax * direction.Y);
		base._Process(delta);

		if(!modePlayer){
			LookAt(Position + direction);
		}

		KinematicCollision2D collision = MoveAndCollide(direction, true);
		if (collision != null)
		{
			setEtat(-1);
		}

		if(!modePlayer && !waitPlayer){
			TimeSpan duration = DateTime.Now.Subtract(startLunch);
			if(duration.TotalSeconds>TimeLife){
				annimation.Play("explosion");
			}
		}

	}

	protected override void On_animation_finish(string anim_name)
	{
		base.On_animation_finish(anim_name);
		if (anim_name == "explosion")
		{
			this.QueueFree();
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
				annimation.Play("mort");
				break;
			case 1:
				//normal
				break;
			case -1:
				direction = new Vector2(0, 0);
				annimation.Play("explosion");
				break;
		}
	}

	protected override bool OnCollision(Area2D otherArea)
	{
		if(!base.OnCollision(otherArea)){
			return false;
		}
		var otherParent = otherArea.GetParent();
		if (waitPlayer)
		{
			if (otherParent.IsInGroup("player"))
			{
				Patrick player = (Patrick)otherParent;
				player.setEtat(3);
				lessEtat();
			}
		}
		else
		{
			if (otherParent.IsInGroup("player"))
			{
				Patrick player = (Patrick)otherParent;
				if (!player.isInvincible)
				{
					if (player.directionCurrent.Y <= 0)
					{
						player.lessEtat();
					}
				}
			}
			else if (otherParent.IsInGroup("enemies"))
			{
				Charactere player = (Charactere)otherParent;
				if (player.directionCurrent.Y <= 0)
				{
					player.lessEtat();
				}
			}
		}
		return true;
	}
	public override void setModePlayer(Charactere user){
		base.setModePlayer(user);
		parametreLevel.Gravity=0f;
	}
	public override void setdirection(Vector2 dir,Charactere user)
	{
		base.setdirection(dir,user);
		parametreLevel.Gravity=0f;
	}
}
