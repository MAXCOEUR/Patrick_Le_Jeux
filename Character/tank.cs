using Godot;
using System;
using System.Reflection;
using System.Threading;
using System.Timers;

public partial class tank : Enemies
{

    protected Patrick patrick;
    protected Sprite2D canon;
    protected Sprite2D tankSprite;

    protected System.Timers.Timer timerFire;
    Vector2 direction = new Vector2(1, 0);
    Vector2 distance = new Vector2(1, 0);

    PackedScene missileScene;


    public override void _Ready()
    {
        base._Ready();



        patrick = GetNode<Patrick>("../../../Patrick");
        canon = sprite.GetNode<Sprite2D>("Canon");
        tankSprite = sprite.GetNode<Sprite2D>("Tank");
        directionDeplacment = new Vector2(0, 0);

        Random random = new Random();
        int intervalSeconds = random.Next(3,7);
        int intervalMillis = intervalSeconds * 1000;

        timerFire = new System.Timers.Timer(intervalMillis);
        timerFire.Elapsed += (timerSender, timerEvent) => send(timerSender, timerEvent);
        timerFire.AutoReset = true;
        timerFire.Enabled = true;

        if (ResourceLoader.Exists("res://Objet/missile.tscn"))
        {
            missileScene = (PackedScene)ResourceLoader.Load("res://Objet/missile.tscn");
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
        switch (i)
        {
            case 0:
                directionDeplacment = new Vector2(0, 0);
                annimation.Play("mort");
                break;
            case 1:
                //normal
                break;
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
        if (direction.X < 0)
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
        if (distance.Length() < 1280)
        {
            missile missile = (missile)missileScene.Instantiate();
            missile.Position = Position;


            missile.setdirection(direction, this);
            GetParent().AddChild(missile);
            
        }

    }
}
