using Godot;
using System;

public class Bomb : Node2D
{
    [Signal]
    public delegate void OnExplode(Vector2 position);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void onArea2DInputEvent(Viewport viewport, InputEvent ev, int shape_idx) {

        var btn = ev as InputEventMouseButton;

        if (btn != null && btn.ButtonIndex == (int)ButtonList.Left && ev.IsPressed()) {
            GD.Print("Clicked!");
            Explode();
        }
    }

    public void Explode() {

        EmitSignal(nameof(OnExplode), Position);
        this.QueueFree();
    }
}
