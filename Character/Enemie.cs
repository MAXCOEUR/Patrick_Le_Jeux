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
				float piedPatrick = player.Position.Y + (player.getSize()*player.Scale/2).Y-10;
				float teteEnemie = Position.Y - (getSize()*Scale/2).Y+10;
				GD.Print(piedPatrick+"<="+teteEnemie+" "+player.Velocity);
				if (!(piedPatrick <= teteEnemie))
				{
					player.lessEtat();
				}
			}
		}
	}
}
