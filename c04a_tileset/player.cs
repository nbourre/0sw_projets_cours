using Godot;
using System;

public class player : KinematicBody2D
{
    const int MAX_SPEED = 200;
    const int ACCELERATION = 25;
    const int FRICTION = 25;

    Vector2 input_vector = new Vector2();
    public Vector2 Velocity = Vector2.Zero;
    public Vector2 PreviousPosition = Vector2.Zero;

    [Signal]
    public delegate void OnMoving(Vector2 previous, Vector2 current);

    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    private Vector2 getInput() {
        input_vector *= 0; // Réinitialisation

        input_vector.x = Input.GetActionStrength("ui_right") 
                            - Input.GetActionStrength("ui_left");
        input_vector.y = Input.GetActionStrength("ui_down") 
                            - Input.GetActionStrength("ui_up");
        
        input_vector = input_vector.Normalized();
        return input_vector;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        

        input_vector = getInput();

        if (input_vector != Vector2.Zero) {
            Velocity = Velocity.MoveToward(input_vector * MAX_SPEED, ACCELERATION);
            EmitSignal(nameof(OnMoving), PreviousPosition, this.Position);
        } else {
            Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION);
        }

        Velocity = MoveAndSlide(Velocity);
        PreviousPosition.x =  this.Position.x;
        PreviousPosition.y =  this.Position.y;        
    }


}
