using Godot;
using System;

public partial class World : Node2D
{
	string os = OS.GetName().ToLower();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("OS: " + os);
		
		switch (os)
		{
			case "windows":
				// Load Windows specific settings
				break;
			case "macos":
				//GD.Print(GetViewport().Size.X);
				break;
			case "x11":
				// Load Linux specific settings
				break;
			default:
				// Load default settings
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
