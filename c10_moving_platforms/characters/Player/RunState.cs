using Godot;
using System.Collections.Generic;

public partial class RunState : PlayerState
{
    public override void PhysicsUpdate(float delta)
    {
        // Notice how we have some code duplication between states.That's inherent to the pattern,
        // although in production, your states will tend to be more complex and duplicate code
        // much more rare.
        if (!_player.IsOnFloor())
        {
            _stateMachine.TransitionTo("Fall");
        }

        if (_player.HitFlag)
		{
			_stateMachine.TransitionTo("Hit");
			return;
		}

        // We move the run-specific input code to the state.
        // A good alternative would be to define a `get_input_direction()` function on the `Player.gd`
        // script to avoid duplicating these lines in every script.
        var inputDirectionX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

        _player.Motion.X += _player.ACCEL * inputDirectionX;
        _player.Motion.Y += _player.GRAVITY * delta;

        _player.Motion.X = Mathf.Clamp(_player.Motion.X, -_player.MAXSPEED, _player.MAXSPEED);

        _player.Velocity = _player.Motion;

        _player.MoveAndSlide();
        _player.PlatformVelocity = _player.GetPlatformVelocity();
        

        if (inputDirectionX > 0)
        {
            _player.facing_right = true;            
        } else if (inputDirectionX < 0) {
            _player.facing_right = false;
        }

        if (Input.IsActionJustPressed("ui_jump"))
        {
            var message = new Dictionary<string, bool>()
            {
                { "doJump", true}
            };
            _stateMachine.TransitionTo("Jump", message);
        }
        else if (Godot.Mathf.IsEqualApprox(inputDirectionX, 0.0f))
        {
            _stateMachine.TransitionTo("Idle");
        }

        base.PhysicsUpdate(delta);
    }

    public override void Enter(Dictionary<string, bool> message = null)
    {
        // Display the name of the current state in the console
        // GD.Print("Entering : " + GetType().Name);
        _player.animPlayer.Play("Run");
    }
}
