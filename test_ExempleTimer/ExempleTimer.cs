using Godot;
using System;

public partial class ExempleTimer : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += _on_timer_timeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	
	private void _on_timer_timeout()
	{
		var sprite = GetNode<Sprite2D>("Icon");
		sprite.Visible = !sprite.Visible;
	}
}


