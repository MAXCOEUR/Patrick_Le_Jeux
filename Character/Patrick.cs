using Godot;
using System;

public partial class Patrick : Charactere
{
	private long currentJumpTime = 0;
	private bool isJumping = false;
	bool isInvincible=false;

	private DateTime lastWallLeftJump= DateTime.UtcNow;
	private DateTime lastWallRightJump= DateTime.UtcNow;

	public override void _Ready()
	{
		base._Ready();
	}

	public void setParam(ParametreLevel parametreLevel)
	{
		this.parametreLevel = parametreLevel;
	}

	override protected void OnCollision(Area2D otherArea) {
		Vector2 velocity = Velocity;
		var otherParent = otherArea.GetParent();
		GD.Print(otherParent.GetGroups());
		if (otherParent.IsInGroup("enemies")) {
			if(!isInvincible){
				if(Velocity.Y>0){
					annimation.Play("saut_sur_ennmie");
					Velocity = new Vector2(Velocity.X,parametreLevel.jumpBase);
					Charactere enemie= (Charactere)otherParent;
					enemie.lessEtat();
				}
				else{
					lessEtat();
				}
			}
		}
	}
	override public void lessEtat(){
		isInvincible=true;
		annimation.Play("hit");
		switch (etat){
			case 1:
				setEtat(0);
			break;
			case 2:
				setEtat(1);
			break;
			case 3:
				setEtat(2);
			break;
		};
	}

	override public void setEtat(int i){
		Vector2 newScale;
		etat=i;
		switch (i){
			case 0:
				annimation.Play("mort");
				
			break;
			case 1:
				newScale = new Vector2(0.5f,0.5f);
				this.Scale=newScale;
			break;
			case 2:
				newScale = new Vector2(1f,1f);
				this.Scale=newScale;
			break;
			case 3:
			break;
		}
	}

	override protected void On_animation_finish(string anim_name) {
		if(anim_name=="mort"){
			GameOver gameOver = GetNode<GameOver>("../Camera2D/GameOver");
			gameOver.setVisible();
		}
		else if (anim_name=="hit"){
			isInvincible=false;
		}
		
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;


		// Handle Jump.
		if (Input.IsActionJustPressed("ui_jump")){
			
			if(IsOnFloor()){
				isJumping = true;
				currentJumpTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
				velocity.Y += parametreLevel.jumpBase;
			}
			else if (isOnLeftWall()) {
				lastWallLeftJump= DateTime.UtcNow;
				sprite.FlipH = false;
				velocity.Y = parametreLevel.jumpBase*1f;
				currentVitess = -parametreLevel.jumpBase*1.5f;
			}
			else if (isOnRightWall()) {
				lastWallRightJump= DateTime.UtcNow;
				sprite.FlipH = true;
				velocity.Y = parametreLevel.jumpBase*1f;
				currentVitess = parametreLevel.jumpBase*1.5f;
			}
			
		}
		if (isJumping && DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()- currentJumpTime < parametreLevel.maxJumpTime && Input.IsActionPressed("ui_jump"))
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
			if(direction.X>0 && DateTime.UtcNow.Second-lastWallRightJump.Second>=1){
				goRight(velocity,direction);
			}else if (DateTime.UtcNow.Second-lastWallLeftJump.Second>=1){
				goLeft(velocity,direction);
			}
		}
		else
		{
			currentVitess *= parametreLevel.Friction;
		}
		

		velocity.X=currentVitess;

		Velocity = velocity;
		MoveAndSlide();
	}

}
