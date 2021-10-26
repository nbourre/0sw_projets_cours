using Godot;
using System;

public class Explosion : Particles2D
{
    AudioStreamPlayer2D sfx;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sfx = GetNode<AudioStreamPlayer2D>("sfx");
    }

    public void start() {
        sfx.Play();
        
        Emitting = true;
    }

    public void _on_bomb_OnExplode (Vector2 position) {
        GD.Print("Boom!");
        
        Position = new Vector2(position.x, position.y);
        start();
    }



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
