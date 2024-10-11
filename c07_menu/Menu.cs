using Godot;
using System;

public class Menu : Control
{
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/StartButton").GrabFocus();        
	}

	public void onStartButtonPressed() {
		GetTree().ChangeScene("res://sceneTest.tscn");
	}

	public void onOptionsButtonPressed() {
		var options = GD.Load<PackedScene>("res://optionsMenu.tscn").Instance();
		GetTree().CurrentScene.AddChild(options);
	}

	public void onQuitButtonPressed() {
		GetTree().Quit();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
