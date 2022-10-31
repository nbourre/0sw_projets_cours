using Godot;
using System;
using System.Collections.Generic;

public class FallState : PlayerState
{
    public override void Enter(Dictionary<string, bool> message = null)
    {
        _player.animPlayer.Play("Fall");
    }

    public override void PhysicsUpdate(float delta)
    {
        // Horizontal movement
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
        
        // Landing
        if (_player.IsOnFloor())
        {
            _stateMachine.TransitionTo("Idle");
        }
    }
}
