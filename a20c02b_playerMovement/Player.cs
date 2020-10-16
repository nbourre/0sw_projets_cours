using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int Speed = 200;
    public Vector2 Velocity = Vector2.Zero;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void GetInput()
    {
        Velocity = new Vector2();

        if (Input.IsActionPressed("ui_right"))
            Velocity.x += 1;

        if (Input.IsActionPressed("ui_left"))
            Velocity.x -= 1;

        if (Input.IsActionPressed("ui_down"))
            Velocity.y += 1;

        if (Input.IsActionPressed("ui_up"))
            Velocity.y -= 1;

        Velocity = Velocity.Normalized() * Speed;
    }

    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        
        Velocity = MoveAndSlide(Velocity);
    }

}
