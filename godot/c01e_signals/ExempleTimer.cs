using Godot;
using System;

public partial class ExempleTimer : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void OnTimerTimeout() {
		var sprite = GetNode<Sprite2D>("Sprite2D");
	    sprite.Visible = !sprite.Visible;
  
	}
}
