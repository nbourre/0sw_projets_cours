using Godot;
using System;

public partial class ParallaxBackground : Godot.ParallaxBackground
{
    [Export]
    float Cloud_Speed = -15f;

    ParallaxLayer cloudLayer;
    Sprite2D cloudSprite;

    public override void _Ready()
    {        
        cloudLayer = GetNode<ParallaxLayer>("clouds");
        cloudSprite = cloudLayer.GetNode<Sprite2D>("Sprite2D");
    }

    public override void _Process(double delta)
    {
       cloudLayer.MotionOffset = 
        new Vector2(
            cloudLayer.MotionOffset.Y + (Cloud_Speed * (float)delta),
             0);          
    }
}
