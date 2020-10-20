using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int Speed = 200;

    const int MAX_SPEED = 200;
    const int ACCELERATION = 25;
    const int FRICTION = 25;
    public Vector2 Velocity = Vector2.Zero;

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
            // Velocity += input_vector * ACCELERATION * delta;
            // Velocity = Velocity.Clamped(MAX_SPEED * delta);
            Velocity = Velocity.MoveToward(input_vector * MAX_SPEED, ACCELERATION);
        } else {
            Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION);
        }

        Velocity = MoveAndSlide(Velocity);
    }

}
