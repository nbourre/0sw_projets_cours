using Godot;
using System;

public partial class ParallaxBackground : Godot.ParallaxBackground
{
    [Export]
    float Cloud_Speed = -10f;

    ParallaxLayer cloudLayer;
    Sprite2D cloudSprite;

    public override void _Ready()
    {        
        cloudLayer = GetNode<ParallaxLayer>("clouds");
        cloudSprite = cloudLayer.GetNode<Sprite2D>("Sprite2D");
    }

    public override void _Process(double delta)
    {
        // Move the cloud automatically
        cloudLayer.SetMotionOffset(
            new Vector2(
                cloudLayer.GetMotionOffset().X + (Cloud_Speed * (float)delta),
                0));      
    }
}
