using Godot;
using System;

public class Menu : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void onStartButtonPressed() {
        GetTree().ChangeScene("res://testScene.tscn");
    }

    public void onQuitButtonPressed() {
        GetTree().Quit();
    }

    public void onOptionsButtonPressed() {
        var options = GD.Load<PackedScene>("res://OptionsMenu.tscn").Instance();
        GetTree().CurrentScene.AddChild(options);
    }

}
