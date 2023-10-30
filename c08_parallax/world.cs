using Godot;
using System;

public partial class world : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _PhysicsProcess(double delta)
    {
        handleInput();

    }

    private void handleInput() {
        // If user press escape key quit the game
        if (Input.IsActionJustPressed("ui_cancel")) {
            GetTree().Quit();
        }
    }

}
