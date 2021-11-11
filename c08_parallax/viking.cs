using Godot;
using System;

// Source : https://www.youtube.com/watch?v=OUvpe9FMkLI&t=339s
public class viking : KinematicBody2D
{
    bool isDebugging = true;
    float MAX_VELOCITY;
    float MAX_JUMP = -300;
    float MAXFALLSPEED = 200;
    float HALT_SPEED = 0.325F;
    int ACCELERATION = 10;
    int gravity = 20;

    const float debuggingRate = 0.5f;
    float debuggingAcc = 0;

    bool facing_right = true;  
    Sprite currentSprite;
    Vector2 velocity = Vector2.Zero;

    AnimationTree animTree;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        initValues();
        currentSprite = GetNode<Sprite>("Sprite");     
        animTree = GetNode<AnimationTree>("AnimationTree");

        animTree.Active = true;   
    }

    void initValues()
    {
        MAX_VELOCITY = 80 * Scale.x;
        MAX_JUMP = -300 * Scale.y;
        MAXFALLSPEED = 200 * Scale.y;
        ACCELERATION = 10 * (int)Scale.x;
        gravity = 20 * (int)Scale.y;
        HALT_SPEED = 0.325F * Scale.x;
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


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        debuggingAcc += delta;

        var input_vector = GetInput();

        velocity.y += gravity;

        if (velocity.y > MAXFALLSPEED) {
            velocity.y = MAXFALLSPEED;
        }

        if (input_vector.x > 0) {
            facing_right = true;
            currentSprite.FlipH = false;
        } else  if (input_vector.x < 0){
            facing_right = false;
            currentSprite.FlipH = true;
        }

        if (input_vector == Vector2.Zero) {
            velocity = velocity.LinearInterpolate(Vector2.Zero, 0.2f);
            animTree.Set("parameters/movement/current", 0);
        } else {
            velocity.x += ACCELERATION * input_vector.x;
            
        }


        if (IsOnFloor()) {
            velocity.y = 0;
            if (Input.IsActionJustPressed("jump")) {
                velocity.y = MAX_JUMP;
                
                GD.Print("Jumping");
                GD.Print($"Velocity.y : {velocity.y}");
            } else {
                if (input_vector != Vector2.Zero) {
                    animTree.Set("parameters/movement/current", 1);
                }
            }
        }

        if (!IsOnFloor()){
            if (velocity.y < 0) {
                animTree.Set("parameters/in_air/current", 1);
                GD.Print("rising");
            } 
            if (velocity.y > 0) {
                animTree.Set("parameters/in_air/current", 0);
            }
        }

        velocity.x = velocity.Clamped (MAX_VELOCITY).x;

        if (Math.Abs(velocity.x) < .01f) {
            velocity.x = 0;
        }

        velocity = MoveAndSlide(velocity, Vector2.Up);

        if (debuggingAcc >= debuggingRate) {
            debuggingAcc -= debuggingRate;
            
            debugging();
        }
    }

    void debugging() {
        if (!isDebugging) return;

        //sdaGD.Print($"Position.y : {Position.y}");
        GD.Print($"Velocity : ({velocity.x}, {velocity.y})");
        //GD.Print($"IsOnFloor() : {IsOnFloor()}");
    }


}
