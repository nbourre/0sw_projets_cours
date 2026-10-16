using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public int GRAVITY = 400;
	public int MAXFALLSPEED = 200;
	public int MAXSPEED = 100;
	public int JUMPFORCE = 200;

	public int ACCEL = 10;
	Vector2 vZero = new Vector2();

	public bool facing_right = true;

	public bool HitFlag { get; set; } = false;

	private Vector2 _hitVelocity;
	public Vector2 HitVelocity
	{
		get => _hitVelocity;
		set
		{
			_hitVelocity = value;
			HitFlag = true;
		}
	}

	float dir = 0;
	float delta = 0;

	public Vector2 Motion = new Vector2();

	public Sprite2D currentSprite;
	public AnimationPlayer animPlayer;
		// Called when the node enters the scene tree for the first time.
	
	public StateMachine stateMachine;
	public override void _Ready()
	{
		currentSprite = GetNode<Sprite2D>("Sprite2D");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		stateMachine = GetNode<StateMachine>("StateMachine");
	}

	public override void _PhysicsProcess(double delta)
	{
		currentSprite.FlipH = !facing_right;
		
	}

	public void ApplyForce(Vector2 force)
	{
		Motion += force;
	}

	public void HasBeenHit(Vector2 velocity)
	{
		HitVelocity = velocity;
	}    
}
