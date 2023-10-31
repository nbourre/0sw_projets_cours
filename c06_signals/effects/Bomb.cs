using Godot;
using System;

public partial class Bomb : Node2D
{
    [Signal]
    public delegate void OnExplodeEventHandler(Vector2 position);
    
    CollisionShape2D collisionShape2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        collisionShape2D = GetNode<CollisionShape2D>("Area2D/CollisionShape2D");
    }


    public void onArea2DInputEvent(Node viewport, InputEvent ev, int shape_idx) {

        var btn = ev as InputEventMouseButton;
        //GD.Print($"Input event : {ev}");
        
        if (btn != null && btn.ButtonIndex == MouseButton.Left && ev.IsPressed()) {
            GD.Print("Clicked!");
            Explode();
        }
    }

    public void Explode() {

        EmitSignal(nameof(OnExplode), Position);
        this.QueueFree();
    }
}
