using Godot;
using System;
using System.Collections.Generic;

public partial class world : Node2D
{
    Explosion explosion;
    Node2D bombsContainer;
    PackedScene bombScene;
    List<Bomb> bombs;
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        explosion = GetNode<Explosion>("EffectsLayer/Explosion");
        bombs = new List<Bomb>();
        
        bombScene = ResourceLoader.Load<PackedScene>("res://effects/Bomb.tscn");

        bombsContainer = GetNode<Node2D>("bombs");
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
        // TODO : Validate position before adding

        var createdBomb = (Bomb)bombScene.Instantiate();
        createdBomb.Position = position;
        createdBomb.OnExplode += explosion._on_bomb_OnExplode;
        
        bombsContainer.CallDeferred("add_child", createdBomb);
        bombs.Add(createdBomb);

        GD.Print($"Nb children : {bombsContainer.GetChildCount()}");
    } 



    

    public override void _Process(double delta)
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
