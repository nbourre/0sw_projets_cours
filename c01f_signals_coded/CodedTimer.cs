using Godot;
using System;

public partial class CodedTimer : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += OnTimerTimeout;
		
		// Code lorsque converti à partir d'un projet 3.x
		//GetNode("Timer").Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
	}

	public void OnTimerTimeout() {
		var sprite = GetNode<Sprite2D>("Sprite2D");
		sprite.Visible = !sprite.Visible;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
