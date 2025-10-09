using Godot;
using System;

public partial class Node2D : Godot.Node2D
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
		

	   
	}

	public override void _UnhandledKeyInput(InputEvent @event)
	{
		base._UnhandledKeyInput(@event);

		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed) {
				var lbl_Control = (Label)GetNode("lbl_Control");
				
				lbl_Control.Text = $"Key : {Enum.GetName(typeof(KeyList), eventKey.Keycode)}";
			}
		}
	}
}
