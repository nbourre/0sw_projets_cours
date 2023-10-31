using Godot;
using System;
using System.Data;

// Source : https://www.youtube.com/watch?v=OUvpe9FMkLI&t=339s
public partial class viking : CharacterBody2D
{
    bool isDebugging = true;
    float MAX_VELOCITY;
    float MAX_JUMP = -300;
    float MAXFALLSPEED = 200;
    float HALT_SPEED = 0.325F;
    int ACCELERATION = 10;
    int gravity = 20;

    const double debuggingRate = 0.5f;
    double debuggingAcc = 0;

    bool facing_right = true;  
    Sprite2D currentSprite;
    Vector2 velocity = Vector2.Zero;

    Vector2 inputVector = Vector2.Zero;
    AnimationTree animTree;

    AnimationNodeStateMachinePlayback stateMachine;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        initValues();
        currentSprite = GetNode<Sprite2D>("Sprite2D");     
        animTree = GetNode<AnimationTree>("AnimationTree");

        animTree.Active = true;   
        stateMachine = (AnimationNodeStateMachinePlayback)animTree.Get("parameters/playback");
    }

    void initValues()
    {
        MAX_VELOCITY = 80 * Scale.X;
        MAX_JUMP = -300 * Scale.Y;
        MAXFALLSPEED = 200 * Scale.Y;
        ACCELERATION = 10 * (int)Scale.X;
        gravity = 20 * (int)Scale.Y;
        HALT_SPEED = 0.325F * Scale.X;
    }

    public Vector2 GetInput()
    {
        var input_vector = Vector2.Zero;
        input_vector.X = Input.GetActionStrength("ui_right") 
                            - Input.GetActionStrength("ui_left");
        input_vector.Y = Input.GetActionStrength("ui_down") 
                            - Input.GetActionStrength("ui_up");
        
        input_vector = input_vector.Normalized();

        return input_vector;
    }


    // TODO : Continuer la vidéo :https://www.youtube.com/watch?v=WrMORzl3g1U
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        debuggingAcc += delta;

        inputVector = GetInput();

        if (IsOnFloor()) {
            velocity.Y = 0;
            if (Input.IsActionJustPressed("jump")) {
                velocity.Y = MAX_JUMP;
                GD.Print("Jumping");
                GD.Print($"Velocity.Y : {velocity.Y}");
            } else {
                if (inputVector != Vector2.Zero) {
                    stateMachine.Travel("run");
                } else {
                    stateMachine.Travel("idle");
                }
            }
        }

        velocity.Y += gravity;

        if (velocity.Y > MAXFALLSPEED) {
            velocity.Y = MAXFALLSPEED;
        }

        if (inputVector.X > 0) {
            facing_right = true;
            currentSprite.FlipH = false;
        } else  if (inputVector.X < 0){
            facing_right = false;
            currentSprite.FlipH = true;
        }

        if (inputVector == Vector2.Zero) {
            velocity = velocity.Lerp(Vector2.Zero, 0.2f);
        } else {
            velocity.X += ACCELERATION * inputVector.X;
        }

        if (!IsOnFloor()){
            if (velocity.Y < 0) {
                GD.Print("rising");
                stateMachine.Travel("jump");
            } 
            if (velocity.Y > 0) {
                GD.Print("falling");
                stateMachine.Travel("fall");
            }
        }

        if (Math.Abs(velocity.X) < .01f) {
            velocity.X = 0;
        }

        velocity.X = Math.Clamp(velocity.X, -MAX_VELOCITY, MAX_VELOCITY);

        Velocity = velocity;

        MoveAndSlide();
       

        if (debuggingAcc >= debuggingRate) {
            debuggingAcc -= debuggingRate;
            
            debugging();
        }
    }

    void debugging() {
        if (!isDebugging) return;

        //sdaGD.Print($"Position.Y : {Position.Y}");
        GD.Print($"Velocity : ({velocity.X}, {velocity.Y})");
        //GD.Print($"IsOnFloor() : {IsOnFloor()}");
    }


}
