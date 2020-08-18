using Godot;
using System;

public class Label : Godot.Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    private float _accum = 0;
    private Vector2 currentPos;

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        _accum += delta;
        Text = _accum.ToString();
        
        currentPos = RectPosition;

        currentPos.x = currentPos.x + (int)(100f * delta);

        SetPosition(currentPos);
    }

    public override void _PhysicsProcess(float delta)
    {
        
    }
}
