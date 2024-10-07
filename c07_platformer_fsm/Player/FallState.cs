using Godot;
using System;
using System.Collections.Generic;

public partial class FallState : PlayerState
{
    public override void Enter(Dictionary<string, bool> message = null)
    {
        _player.animPlayer.Play("Fall");

        // Display the name of the current state in the console
        GD.Print("Entering : " + GetType().Name);
    }

    public override void PhysicsUpdate(float delta)
    {
        // Horizontal movement
        var inputDirectionX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");

        _player.Motion.X += _player.ACCEL * inputDirectionX;
        _player.Motion.Y += _player.GRAVITY * delta;

        _player.Motion.X = Mathf.Clamp(_player.Motion.X, -_player.MAXSPEED, _player.MAXSPEED);

        _player.Velocity = _player.Motion;

        _player.MoveAndSlide();

        if (inputDirectionX > 0)
        {
            _player.facing_right = true;            
        } else if (inputDirectionX < 0) {
            _player.facing_right = false;
        }
        
        // Landing
        if (_player.IsOnFloor())
        {
            _stateMachine.TransitionTo("Idle");
        }
    }
}
