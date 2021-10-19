using Godot;
using System;

public class world : Node2D
{
    Camera2D camera2D;
    int tileSize = 64;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera2D = GetNode<Camera2D>("player/Camera2D");

        camera2D.LimitLeft = 0 + tileSize;
        camera2D.LimitTop = 0 + tileSize;
        camera2D.LimitRight = 1900 - tileSize;
        camera2D.LimitBottom = 650 - tileSize;
    }

    public void OnPlayerMoving(Vector2 previous, Vector2 current) {
        GD.Print($"({current.x}, {current.y})");
    }
}
