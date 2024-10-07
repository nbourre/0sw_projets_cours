using Godot;
using System;

public partial class player : CharacterBody2D
{
    Vector2 UP = new Vector2(0, -1);
    const int GRAVITY = 20;
    const int MAXFALLSPEED = 200;
    const int MAXSPEED = 100;
    const int JUMPFORCE = 300;

    const float ACCEL = 10;
    Vector2 vZero = new Vector2();

    bool facing_right = true;


    Vector2 motion = new Vector2();

    Sprite2D currentSprite;
    AnimationPlayer animPlayer;
        // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentSprite = GetNode<Sprite2D>("Sprite2D");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        var dir = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

        motion.Y += GRAVITY;

        if(motion.Y > MAXFALLSPEED) {
            motion.Y = MAXFALLSPEED;
        }

        if (facing_right) {
            currentSprite.FlipH = false;
        } else {
            currentSprite.FlipH = true;
        }

        if (Input.IsActionPressed("ui_left")) {
            motion.X -= ACCEL;
            facing_right = false;
            animPlayer.Play("Run");
        } else if (Input.IsActionPressed("ui_right")) {
            motion.X += ACCEL;
            facing_right = true;
            animPlayer.Play("Run");
        } else {
            motion = motion.Lerp(Vector2.Zero, 0.2f);
            animPlayer.Play("Idle");
        }

        if (IsOnFloor()) {
            // On ne regarde qu'un seul fois et non le maintient de la touche
            if (Input.IsActionJustPressed("ui_jump")) {
                motion.Y = -JUMPFORCE;
                GD.Print($"motion.Y = {motion.Y}");
                Console.WriteLine($"motion.Y = {motion.Y}");
            }
        }

        if (!IsOnFloor()) {
            if (motion.Y < 0) {
                animPlayer.Play("jump");
            } else if (motion.Y > 0) {
                animPlayer.Play("fall");
            }
        }

        // Bug : Si on saute, le X est automatiquement réduit, car l'impulsion est trop forte
        // motion.X = motion.LimitLength(MAXSPEED).X;

        motion.X = Mathf.Lerp(motion.X, MAXSPEED * motion.X > 0 ? 1 : -1, ACCEL / MAXSPEED);



        //motion = MoveAndSlide(motion, UP);
        Velocity = motion;
        MoveAndSlide();
    }


    
}
