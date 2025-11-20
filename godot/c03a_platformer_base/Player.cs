using Godot;
using System;


public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	public const float JumpVelocity = -400.0f;

	public int Acceleration = 10;
	public bool FacingRight = true;

	private const float MAX_RUN_SPEED = 200.0f;
	private const float MAX_FALL_SPEED = 400.0f;

	private Sprite2D currentSprite;
	private AnimationPlayer animationPlayer;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		currentSprite = GetNode<Sprite2D>("Sprite2D");      
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");  
	}

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		} else {
			if (Input.IsActionJustPressed("ui_jump")) {
				velocity.Y = JumpVelocity;
			}			
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down").Normalized();

		if (direction.X != 0)
		{
			FacingRight = direction.X > 0;
		}

		if (direction != Vector2.Zero)
		{
			velocity.X += direction.X * Acceleration;
			animationPlayer.Play("running");
		}
		else
		{
			// À chaque frame, on diminue la vélocité de 20%.			
			velocity.X = Mathf.Lerp(velocity.X, 0, 0.2f);
			animationPlayer.Play("idle");			
		}
		
		velocity.X = Math.Clamp(velocity.X, -MAX_RUN_SPEED, MAX_RUN_SPEED);
		velocity.Y = Math.Clamp(velocity.Y, -MAX_FALL_SPEED, MAX_FALL_SPEED);

		if (FacingRight) {
			currentSprite.FlipH = false;
		} else {
			currentSprite.FlipH = true;
		}

		if (!IsOnFloor()) {
			if (velocity.Y > 0)
				animationPlayer.Play("falling");
			else if (velocity.Y <= 0)
			{
				animationPlayer.Play("jumping");
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
