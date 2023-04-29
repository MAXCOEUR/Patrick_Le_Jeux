using Godot;
using System;

public partial class soldat_pasclassique : Among_us_rouge
{
	bool isInvincible = false;
	protected System.Timers.Timer timerFire;
	public override void _Ready()
	{
		base._Ready();
		timerFire = new System.Timers.Timer(6000);
		timerFire.Elapsed += (timerSender, timerEvent) => shoot(timerSender, timerEvent);
		timerFire.AutoReset = true;
		timerFire.Enabled = true;
		setEtat(3);
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
	public void shoot(object source, System.Timers.ElapsedEventArgs e)
	{
		isInvincible = true;
		annimation.Play("shoot");
	}
	override public void setEtat(int i)
	{
		etat = i;
		switch (i)
		{
			case 0:
				directionDeplacment = new Vector2(0, 0);
				break;
			case 1:
				Scale = new Vector2(0.5f, 0.5f);
				break;
			case 2:

				Scale = new Vector2(0.75f, 0.75f);
				break;
			case 3:
				Scale = new Vector2(1, 1);
				break;
		}
	}
	override protected void On_animation_finish(string anim_name)
	{
		base.On_animation_finish(anim_name);
		if (anim_name == "cout_pri")
		{
			isInvincible = false;
		}
		else if (anim_name=="shoot")
		{
			isInvincible = false;
		}
	}
	override public void lessEtat()
	{
		if (!isInvincible)
		{
			switch (etat)
			{
				case 1:
					annimation.Play("mort");
					setEtat(0);
					break;
				case 2:
					isInvincible = true;
					annimation.Play("cout_pri");
					setEtat(1);
					break;
				case 3:
					isInvincible = true;
					annimation.Play("cout_pri");
					setEtat(2);
					break;
			};
		}

	}
}
