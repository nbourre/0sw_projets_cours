using Godot;
using System;

public class Sprite : Godot.Sprite
{
    private int speed = 256;
    private int tileSize = 64;

    private Vector2 lastPosition = new Vector2();
    private Vector2 targetPosition = new Vector2();
    private Vector2 moveDir = new Vector2();

    public override void _Ready() {
        Position = Position.Snapped(new Vector2(tileSize, tileSize));
        lastPosition = Position;
        targetPosition = Position;   
    }

    public override void _Process(float delta)
    {
        Position += speed * moveDir * delta;

        if (Position.DistanceTo(lastPosition) >= (tileSize - speed * delta)) {
            Position = targetPosition;
        }


        if (Position == targetPosition) {
            getMoveDir();
            lastPosition = Position;
            targetPosition += moveDir * tileSize;
        }

        base._Process(delta);
    }

    private void getMoveDir()
    {
        var LEFT = Input.IsActionPressed("ui_left") ? 1 : 0;        
        var RIGHT = Input.IsActionPressed("ui_right") ? 1 : 0;
        var UP = Input.IsActionPressed("ui_up") ? 1 : 0;
        var DOWN = Input.IsActionPressed("ui_down") ? 1 : 0;

        moveDir.x = -LEFT + RIGHT;
        moveDir.y = -UP + DOWN;

        if (moveDir.x != 0 && moveDir.y != 0) {
            moveDir = Vector2.Zero;
        }
    }
}
