using Godot;
using System;

public partial class Instruction
{
    Vector2 top_left;
    Vector2 bot_right;
    string action;
    public Instruction(Vector2 top_left,Vector2 bot_right,string action){
        this.top_left=top_left;
        this.bot_right=bot_right;
        this.action=action;
    }

    public Vector2 getTop_left(){
        return top_left;
    }
    public Vector2 getBot_right(){
        return bot_right;
    }
    public string getAction(){
        return action;
    }

}
