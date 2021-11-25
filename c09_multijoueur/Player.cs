using Godot;
using System;

public class Player : KinematicBody2D
{
    const float speed = 300;
    public Vector2 Velocity {get;set;} = new Vector2();
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
    float PuppetRotation = 0.0f;

    [Puppet]
    public Vector2 PuppetVelocity {get; set;}
    

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

            Velocity = new Vector2(x_input, y_input).Normalized() * speed;

            MoveAndSlide(Velocity);
            
            LookAt(GetGlobalMousePosition());
        }
        else {
            RotationDegrees = Mathf.Lerp(RotationDegrees, PuppetRotation, delta * 8f);

            // Permet d'extrapoler la position du puppet lorsque l'on ne reçoit pas de paquet
            if (! tween.IsActive()) {
                MoveAndSlide(PuppetVelocity * speed);
            }
        }
    }

    public void _on_NetworkTickRate_timeout() {
        if (IsNetworkMaster()){
            RsetUnreliable(nameof(PuppetPosition), GlobalPosition);
            RsetUnreliable(nameof(PuppetVelocity), Velocity);
            RsetUnreliable(nameof(PuppetRotation), RotationDegrees);
        }
    }
}
