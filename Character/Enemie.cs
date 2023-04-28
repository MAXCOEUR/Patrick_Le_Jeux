using Godot;

public abstract partial class Enemies : Charactere
{
	public override void _Ready()
	{
		base._Ready();
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}

	protected override void OnCollision(Area2D otherArea)
	{
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player"))
		{
			Patrick player = (Patrick)otherParent;
			if (!player.isInvincible)
			{
				var bot_top = Position.Y - GetViewportRect().Size.Y/2;
				GD.Print(player.Position.Y+" "+player.GetViewportRect().Size.Y/2+"/"+Position.Y+" "+GetViewportRect().Size.Y/2);
				if (player.directionCurrent.Y <= 0)
				{
					player.lessEtat();
				}
			}
		}
	}
}
