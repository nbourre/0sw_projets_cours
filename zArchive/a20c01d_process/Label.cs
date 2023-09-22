using Godot;
using System;

public partial class Label : Godot.Label
{

	private double _accum = 0;
	private Vector2 currentPos;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_accum += delta;
		// Text = _accum.ToString();
		
		currentPos = Position;

		currentPos.X = currentPos.X + (int)(100f * delta);

		SetPosition(currentPos);

		Text = Position.X.ToString();
	}
}
