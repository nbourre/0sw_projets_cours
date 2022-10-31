using Godot;
using System.Collections.Generic;

public class RunState : PlayerState
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

        // We move the run-specific input code to the state.
        // A good alternative would be to define a `get_input_direction()` function on the `Player.gd`
        // script to avoid duplicating these lines in every script.
        var inputDirectionX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

        _player.Motion.x += _player.ACCEL * inputDirectionX;
        _player.Motion.y += _player.GRAVITY * delta;

        _player.Motion.x = Mathf.Clamp(_player.Motion.x, -_player.MAXSPEED, _player.MAXSPEED);

        _player.Motion = _player.MoveAndSlide(_player.Motion, Vector2.Up);

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
    }

    public override void Enter(Dictionary<string, bool> message = null)
    {
        _player.animPlayer.Play("Run");
    }
}
