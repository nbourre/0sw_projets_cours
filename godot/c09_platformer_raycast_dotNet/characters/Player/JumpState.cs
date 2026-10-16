using Godot;
using System;
using System.Collections.Generic;

// The GD tutorial has this class named "Jump.gd". However, in C#, 
// the name of a class is also its type, and should always be the node's 
// name to avoid problems down the line.
public partial class JumpState : PlayerState
{
    /// <summary>
    /// If we get a message asking us to jump, we jump.
    /// </summary>
    /// <param name="message"></param>
    public override void Enter(Dictionary<string, bool> message = null)
    {
        _player.Motion.Y = -_player.JUMPFORCE;
        _player.animPlayer.Play("Jump");

       // Display the name of the current state in the console
        // GD.Print("Entering : " + GetType().Name);
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

        if (_player.Motion.Y > 0)
        {
            _stateMachine.TransitionTo("Fall");
        }
    }
}
