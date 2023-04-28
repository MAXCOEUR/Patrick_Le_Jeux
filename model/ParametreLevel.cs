using Godot;
using System;

public partial class ParametreLevel
{
    public float jumpBase;
    public float jumpHold;
    public float maxJumpTime;
    public float VitesseMax;
    public float Friction;
    public float Acceleration;
    public float Gravity;

    public override string ToString()
    {
        return string.Format(
            "jumpBase: {0}\njumpHold: {1}\nmaxJumpTime: {2}\nVitesseMax: {3}\nFriction: {4}\nAcceleration: {5}\nGravity: {6}",
            jumpBase, jumpHold, maxJumpTime, VitesseMax, Friction, Acceleration, Gravity
        );
    }


    public ParametreLevel()
    {
        jumpBase = -500.0f;
        jumpHold = -10.0f;
        maxJumpTime = 500;
        VitesseMax = 300.0f;
        Friction = 0.80f;
        Acceleration = 10.0f;
        Gravity = 980.0f;
    }
    public void setDed()
    {
        jumpBase = 0;
        jumpHold = 0;
        maxJumpTime = 0;
        VitesseMax = 0;
        Friction = 0;
        Acceleration = 0;
        Gravity = 0;
    }
}
