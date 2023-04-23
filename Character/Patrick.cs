using Godot;
using System;

public partial class Patrick : Charactere
{
	private float currentVitess = 0;
	private ParametreLevel parametreLevel = new ParametreLevel();
	private Sprite2D sprite;
	private long currentJumpTime = 0;
	private bool isJumping = false;
	Area2D patrickArea;
	AnimationPlayer annimation ;

	bool isInvincible=false;
	bool isOnWallCurrent = false;

	private DateTime lastWallLeftJump= DateTime.UtcNow;
	private DateTime lastWallRightJump= DateTime.UtcNow;

	private int etat =1;

	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("Sprite2D");
		patrickArea = GetNode<Area2D>("Area2D");
		annimation = GetNode<AnimationPlayer>("AnimationPlayer");
		annimation.Connect("animation_finished", new Callable(this, "On_animation_finish"));
		setEtat(2);

		patrickArea.Connect("area_entered", new Callable(this, "OnCollision"));
	}

	void OnCollision(Area2D otherArea) {
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
	public void setParam(ParametreLevel parametreLevel){
		this.parametreLevel = parametreLevel;
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

	private void On_animation_finish(string anim_name) {
		if(anim_name=="mort"){
			GameOver gameOver = GetNode<GameOver>("../Camera2D/GameOver");
			gameOver.setVisible();
		}
		else if (anim_name=="hit"){
			isInvincible=false;
		}
		
	}
	
	private void goRight(Vector2 velocity,Vector2 direction)
	{
		sprite.FlipH = false;

		if (velocity.X<0)
		{
			currentVitess *= parametreLevel.Friction;
		}

		currentVitess = Mathf.Clamp(currentVitess + direction.X * parametreLevel.Acceleration,-parametreLevel.VitesseMax,parametreLevel.VitesseMax);

	}

	private void goLeft(Vector2 velocity,Vector2 direction)
	{
		sprite.FlipH = true;

		if (velocity.X>0)
		{
			currentVitess *= parametreLevel.Friction;
		}
		
		currentVitess = Mathf.Clamp(currentVitess + direction.X * parametreLevel.Acceleration,-parametreLevel.VitesseMax,parametreLevel.VitesseMax);
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += parametreLevel.Gravity * (float)delta;

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
				velocity.Y = parametreLevel.jumpBase*1.5f;
				currentVitess = -parametreLevel.jumpBase*2;
			}
			else if (isOnRightWall()) {
				lastWallRightJump= DateTime.UtcNow;
				sprite.FlipH = true;
				velocity.Y = parametreLevel.jumpBase*1.5f;
				currentVitess = parametreLevel.jumpBase*2;
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
