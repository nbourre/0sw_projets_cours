using Godot;
using System;

public class CodedTimer : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode("Timer").Connect("timeout", this, nameof(OnTimerTimeout));
    }

    public void OnTimerTimeout() {
        var sprite = GetNode<Sprite>("Sprite");
        sprite.Visible = !sprite.Visible;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
