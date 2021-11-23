using Godot;
using System;

public class Player : KinematicBody2D
{
    const float speed = 300;
    Vector2 velocity = new Vector2();
    Tween tween;

    private Vector2 puppetPosition;
    [Puppet]
    public Vector2 PuppetPosition {
        get => puppetPosition;
        set{
            puppetPosition = value;

            tween.InterpolateProperty(this, "global_position", GlobalPosition, PuppetPosition, 0.1f);
            tween.Start();
        }
    }

    [Puppet]
    double puppetRotation;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {        
        tween = GetNode<Tween>("Tween"); 
        PuppetPosition = new Vector2();
    }

    public override void _Process(float delta)
    {
        if (IsNetworkMaster()){
            var x_input = Input.GetActionStrength("right") - Input.GetActionStrength("left");
            var y_input = Input.GetActionStrength("down") - Input.GetActionStrength("up");

            velocity = new Vector2(x_input, y_input).Normalized() * speed;

            MoveAndSlide(velocity);
            
            LookAt(GetGlobalMousePosition());
        }
        else {
        }
    }

    public void _on_NetworkTickRate_timeout() {
        if (IsNetworkMaster()){
            RsetUnreliable(nameof(PuppetPosition), GlobalPosition);
        }
    }
}
