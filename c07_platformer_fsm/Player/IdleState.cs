using Godot;
using System;
using System.Collections.Generic;

public partial class IdleState : PlayerState
{
	/// <summary>
	/// Upon entering the state, we set the Player node's velocity 
	/// to slow down to zero.
	/// </summary>
	/// <param name="message"></param>
	public override void Enter(Dictionary<string, bool> message = null)
	{        
		// We must declare all the properties we access through `owner` in the `Player.cs` script.
		_player.Motion = _player.Motion.Lerp(Vector2.Zero, 0.2f);

		if (_player.animPlayer != null)
		{
			_player.animPlayer.Play("Idle");
		}        

		// Display the name of the current state in the console
		GD.Print("Entering : " + GetType().Name);
	}

	public override void PhysicsUpdate(float delta)
	{
		_player.Motion = _player.Motion.Lerp(Vector2.Zero, 0.2f);

		_player.Velocity = _player.Motion;
		
		// If you have platforms that break when standing on them, you need that check for the character to fall.
		if (!_player.IsOnFloor())
		{
			_stateMachine.TransitionTo("Fall");
			return;
		}

		if (Input.IsActionJustPressed("ui_jump"))
		{
			_stateMachine.TransitionTo("Jump");
		}
		else if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
		{
			_stateMachine.TransitionTo("Run");
		}
	}
}
