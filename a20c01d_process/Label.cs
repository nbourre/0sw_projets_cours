using Godot;
using System;

public class Label : Godot.Label
{
    public override void _Ready()
    {
        GD.Print($"{nameof(Label)} ready");
        //GetNode("Button").Connect("pressed", this, nameof(OnButtonPressed));
    }
    public override void _EnterTree()
    {
        GD.Print($"{nameof(Label)} enter tree");
    }
    bool test = true;

    private float _accum = 0;
    private Vector2 currentPos;

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (test) {
            GD.Print($"{nameof(Label)} process");
            test = false;
        }
        _accum += delta;
        // Text = _accum.ToString();
        
        currentPos = RectPosition;

        currentPos.x = currentPos.x + (int)(100f * delta);

        SetPosition(currentPos);

        Text = RectPosition.x.ToString();
    }

}
