using Godot;
using System;

public class Frog : KinematicBody2D
{
    AnimatedSprite animatedSprite;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        // Gestion des entrées
        if (Input.IsActionPressed("ui_right")) {
            animatedSprite.FlipH = true;
            animatedSprite.Play("jump");
        } else if (Input.IsActionPressed("ui_left")) {
            animatedSprite.FlipH = false;
            animatedSprite.Play("jump");
        } else {
            animatedSprite.Play("idle");
        }
              
    }
}
