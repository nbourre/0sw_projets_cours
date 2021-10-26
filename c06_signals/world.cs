using Godot;
using System;
using System.Collections.Generic;

public class world : Node2D
{
    Explosion explosion;
    Node2D bombs;
    PackedScene bombScene;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        explosion = GetNode<Explosion>("EffectsLayer/Explosion");
        //bombs = new List<Bomb>();
        bombScene = GD.Load<PackedScene>("res://effects/Bomb.tscn");

        bombs = GetNode<Node2D>("bombs");
    }

    public override void _Input(InputEvent @event)
    {
        var e = @event;

        if (e is InputEventMouseButton mouseButton) {
            if (mouseButton.IsPressed()) {
                GD.Print("Mouse click at", mouseButton.Position);
                AddBomb(mouseButton.Position);
            }
            
        }

        base._Input(@event);
    }

    private void AddBomb(Vector2 position) {
        var createdBomb = (Bomb)bombScene.Instance();
        createdBomb.Position = position;
        bombs.AddChild(createdBomb, true);
    } 

    public override void _Process(float delta)
    {
        
        // if (Input.IsMouseButtonPressed((int)ButtonList.Left)) {
        //     GD.Print("New bomb!");
        //     var b = new Bomb();
        //     b.Position = new Vector2 (GetLocalMousePosition().x, GetLocalMousePosition().y);
        //     AddChild(b);
        //     bombs.Add(b);
        // }
        
        base._Process(delta);
    }
}
