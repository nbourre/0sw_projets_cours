using Godot;
using System;

public class Mario : Godot.KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Récupération du noeud enfant nommé AnimatedSprite
        var currentSprite = (AnimatedSprite)GetNode("AnimatedSprite");
        
        // Gestion des entrées
        if (Input.IsActionPressed("ui_right")) {
            currentSprite.FlipH = false;
            currentSprite.Play("run");
        } else if (Input.IsActionPressed("ui_left")) {
            currentSprite.FlipH = true;
            currentSprite.Play("run");
        } else {
            currentSprite.Play("stop");
        }
    }

}
