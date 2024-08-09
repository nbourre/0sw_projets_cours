using Godot;
using Godot.Collections;
using System;
using System.Collections;
using System.Linq;

// TODO : Add a generator instead of hardcoding the values

public partial class World : Node2D
{
	Vector2 windowSize;
	Array<Vector2> points = new Array<Vector2>();
	Vector2 startPoint;
	int stepLength = 2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Randomize();
		windowSize = GetViewport().GetVisibleRect().Size;

		startPoint = new Vector2(windowSize.X / 2, windowSize.Y / 2);
		points.Add(startPoint);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int direction = (int)GD.RandRange(0, 100);
		Vector2 nextPoint = new Vector2(0, 0);

		if (direction < 25)
		{
			nextPoint = new Vector2(startPoint.X + stepLength, startPoint.Y);
		}
		else if (direction < 50)
		{
			nextPoint = new Vector2(startPoint.X - stepLength, startPoint.Y);
		}
		else if (direction < 75)
		{
			nextPoint = new Vector2(startPoint.X, startPoint.Y + stepLength);
		}
		else
		{
			nextPoint = new Vector2(startPoint.X, startPoint.Y - stepLength);
		}		

		points.Add(nextPoint);

		startPoint = nextPoint;

		QueueRedraw();
	}

	// This is only called when the node is added to the scene.
	// To redraw the lines, you need to call queue_draw()
    public override void _Draw()
    {
		
		DrawLine(new Vector2(0.0f, 0.0f), windowSize, Colors.Green, 1.0f);
    	DrawLine(new Vector2(windowSize.X, 0f), new Vector2(0f, windowSize.Y), Colors.Green, 2.0f);
    	DrawLine(new Vector2(windowSize.X / 2, 0.0f), new Vector2(windowSize.X / 2, windowSize.Y), Colors.Green, 3.0f);

		if (points.Count > 1)
		{
			DrawPolyline(points.ToArray(), Colors.Red, 2f);
			DrawCircle(points[points.Count - 1], 4.0f, Colors.White);
		}
        base._Draw();
    }
}
