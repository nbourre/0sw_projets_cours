using Godot;
using System;

public class Player : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    Vector2 Velocity {get; set;}


    const int ACCELERATION = 100;
    const int FRICTION = 100;
    const int MAX_SPEED = 200;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public Vector2 GetInput()
    {
        var input_vector = Vector2.Zero;
        input_vector.x = Input.GetActionStrength("ui_right") 
                            - Input.GetActionStrength("ui_left");
        input_vector.y = Input.GetActionStrength("ui_down") 
                            - Input.GetActionStrength("ui_up");
        
        input_vector = input_vector.Normalized();
        return input_vector;
    }

    public override void _PhysicsProcess(float delta)
    {
        var input_vector = GetInput();
        if (input_vector != Vector2.Zero) {
            Velocity += input_vector * ACCELERATION;
            Velocity = Velocity.Clamped(MAX_SPEED);
        } else {
            Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION);
        }
        MoveAndSlide(Velocity);
    }
}
