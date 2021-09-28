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

    private AnimationPlayer animationPlayer;
    private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback animationState;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        animationTree = (AnimationTree)GetNode("AnimationTree");
        animationState = (AnimationNodeStateMachinePlayback) animationTree.Get("parameters/playback");
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
            animationTree.Set("parameters/Idle/blend_position", input_vector);
            animationTree.Set("parameters/Run/blend_position", input_vector);
            animationState.Travel("Run");
            Velocity = Velocity.MoveToward(input_vector * MAX_SPEED, ACCELERATION);
        } else {            
            animationState.Travel("Idle");
            Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION);
        }

        Velocity = MoveAndSlide(Velocity);
    }

}
