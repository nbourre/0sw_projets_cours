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

    enum PlayerState
    {
        MOVE, ROLL, ATTACK        
    }

    PlayerState state = PlayerState.MOVE;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        animationTree = (AnimationTree)GetNode("AnimationTree");
        animationState = (AnimationNodeStateMachinePlayback) animationTree.Get("parameters/playback");

        animationTree.Active = true;
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
        switch (state)
        {
            case PlayerState.MOVE :
                move_state(delta);
                break;
            case PlayerState.ROLL:
                break;
            case PlayerState.ATTACK:
                attack_state(delta);            
                break;

            
            default:
                break;
        }
    }

    private void  move_state(float delta) {
        var input_vector = GetInput();

        if (input_vector != Vector2.Zero) {
            animationTree.Set("parameters/Idle/blend_position", input_vector);
            animationTree.Set("parameters/Run/blend_position", input_vector);
            animationTree.Set("parameters/Attack/blend_position", input_vector);
            animationState.Travel("Run");
            Velocity = Velocity.MoveToward(input_vector * MAX_SPEED, ACCELERATION);
        } else {            
            animationState.Travel("Idle");
            Velocity = Velocity.MoveToward(Vector2.Zero, FRICTION);
        }

        Velocity = MoveAndSlide(Velocity);

        
        if (Input.IsActionJustPressed("attack")) {
            state = PlayerState.ATTACK;
            animationTree.Set("parameters/Attack/blend_position", input_vector);
        }
    }

    private void attack_state(float delta) {
        animationState.Travel("Attack");
        Velocity = Vector2.Zero;        
    }

    private void attack_animation_finished() {
        state = PlayerState.MOVE;
    }

    private void roll_state(float delta) {
        //var input_vector = GetInput();



    }

}
