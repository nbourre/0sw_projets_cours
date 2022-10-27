using Godot;
using System;

public class player : KinematicBody2D
{

    enum State {
        STATE_JUMPING,
        STATE_IDLE,
        STATE_RUNNING,
        STATE_FALLING
    };
    
    State currentState = State.STATE_IDLE;

    Vector2 UP = new Vector2(0, -1);
    const int GRAVITY = 20;
    const int MAXFALLSPEED = 200;
    const int MAXSPEED = 100;
    const int JUMPFORCE = 300;

    const int ACCEL = 10;
    Vector2 vZero = new Vector2();

    bool facing_right = true;


    Vector2 motion = new Vector2();

    Sprite currentSprite;
    AnimationPlayer animPlayer;
        // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentSprite = GetNode<Sprite>("Sprite");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(float delta)
    {
        var dir = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

        motion.x += ACCEL * dir;
        motion.y += GRAVITY;

        if (facing_right) {
            currentSprite.FlipH = false;
        } else {
            currentSprite.FlipH = true;
        }

        switch(currentState) {
            case State.STATE_FALLING:
                if (IsOnFloor()) {
                    currentState = State.STATE_IDLE;
                    animPlayer.Play("Idle");
                }
                break;
            case State.STATE_IDLE:                
                if (dir != 0) {
                    currentState = State.STATE_RUNNING;
                    animPlayer.Play("Run");
                }
                if (Input.IsActionJustPressed("ui_jump")) {
                    currentState = State.STATE_JUMPING;
                    motion.y = -JUMPFORCE;
                    animPlayer.Play("jump");
                }
                if (!IsOnFloor()) {
                    if (motion.y > 0) {
                        currentState = State.STATE_FALLING;
                        animPlayer.Play("fall");
                    }
                }                
                break;
            case State.STATE_JUMPING:
                if (motion.y >= 0) {
                    currentState = State.STATE_FALLING;
                    animPlayer.Play("fall");
                } else {
                    animPlayer.Play("jump");
                }                
                break;
            case State.STATE_RUNNING:                
                if (dir > 0) {
                    facing_right = true;
                } else if (dir < 0) {
                    facing_right = false;
                } else {
                    currentState = State.STATE_IDLE;
                    motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);   
                    animPlayer.Play("Idle");
                }
                if (!IsOnFloor()) {
                    if (motion.y > 0) {
                        currentState = State.STATE_FALLING;
                        animPlayer.Play("fall");
                    }
                }
                if (Input.IsActionJustPressed("ui_jump")) {
                    currentState = State.STATE_JUMPING;
                    motion.y = -JUMPFORCE;
                    animPlayer.Play("jump");
                }                
                break;
            default:
                animPlayer.Play("Idle");
                break;
        }

        motion.x = Mathf.Lerp(motion.x, MAXSPEED * motion.x > 0 ? 1 : -1, (ACCEL * 1f) / MAXSPEED);

        if(motion.y > MAXFALLSPEED) {
            motion.y = MAXFALLSPEED;
        }
        motion = MoveAndSlide(motion, UP);


    }    
}
