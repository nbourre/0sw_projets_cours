using Godot;
using System;

public class sayHello : Panel
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode("Button").Connect("pressed", this, nameof(_OnButtonPressed));
        
	}
	
	public void _OnButtonPressed() {
		GetNode<Label>("Label").Text = "Bonjour!";
	}
}
