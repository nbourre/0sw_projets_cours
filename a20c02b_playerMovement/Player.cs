using Godot;
using System;

public class Player : KinematicBody2D
{
    Vector2 Velocity {get; set;}

    [Export]
    int Speed = 200;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Velocity = new Vector2();        
    }

    public void GetInput() {
        var Velocity = new Vector2();

        if (Input.IsActionPressed("ui_right")) {
            Velocity.x += 1;
        }

        if (Input.IsActionPressed("ui_left")) {
            Velocity.x += -1;
        }

        if (Input.IsActionPressed("ui_down")) {
            Velocity.y += 1;
        }
        if (Input.IsActionPressed("ui_up")) {
            Velocity.y += -1;
        }
        this.Velocity = Velocity.Normalized() * Speed;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        GetInput();
        Velocity = MoveAndSlide(Velocity);
    }
}
