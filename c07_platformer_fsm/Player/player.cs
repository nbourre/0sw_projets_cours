using Godot;
using System;

public class Player : KinematicBody2D
{
    Vector2 UP = new Vector2(0, -1);
    public int GRAVITY = 400;
    public int MAXFALLSPEED = 200;
    public int MAXSPEED = 100;
    public int JUMPFORCE = 200;

    public int ACCEL = 10;
    Vector2 vZero = new Vector2();

    public bool facing_right = true;

    float dir = 0;
    float delta = 0;

    public Vector2 Motion = new Vector2();

    Sprite currentSprite;
    public AnimationPlayer animPlayer;
        // Called when the node enters the scene tree for the first time.
    
    public StateMachine stateMachine;
    public override void _Ready()
    {
        currentSprite = GetNode<Sprite>("Sprite");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        stateMachine = GetNode<StateMachine>("StateMachine");
    }

    public override void _PhysicsProcess(float delta)
    {
        currentSprite.FlipH = !facing_right;
    }    
}
