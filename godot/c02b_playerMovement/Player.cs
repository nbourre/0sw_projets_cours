using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private float Speed = 300.0f;
	public override void _PhysicsProcess(double delta)
	{
		// Velocity is a property of RigidBody2D.
		Vector2 velocity = Velocity;

		// You should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.
			GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
