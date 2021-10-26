using Godot;
using System;
using System.Collections.Generic;

public class world : Node2D
{
    Explosion explosion;
    //List<Bomb> bombs;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        explosion = GetNode<Explosion>("EffectsLayer/Explosion");
        //bombs = new List<Bomb>();
    }

    public override void _Process(float delta)
    {
        
        if (Input.IsMouseButtonPressed((int)ButtonList.Left)) {
            GD.Print("New bomb!");
            var b = new Bomb();
            b.Position = new Vector2 (GetLocalMousePosition().x, GetLocalMousePosition().y);
            AddChild(b);
            //bombs.Add(b);
        }
        
        base._Process(delta);
    }
}
