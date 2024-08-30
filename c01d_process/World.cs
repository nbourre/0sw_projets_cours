using Godot;
using System;

/// <summary>
/// Comparing _Process and _PhysicsProcess
/// </summary>
public partial class World : Node2D
{
    Label labelA;
    Label labelB;

    float speedA = 1f;
    float speedB = 1f;

    public override void _Ready()
    {
        labelA = GetNode<Label>("LabelA");
        labelB = GetNode<Label>("LabelB");
    }

    public override void _Process(double _delta)
    {
        float delta = (float)_delta;
        labelA.Text = $"_Process: {delta}";
        labelA.Position += new Vector2(speedA, 0);

        if (labelA.Position.X + labelA.GetRect().Size.X > GetViewportRect().Size.X || labelA.Position.X < 0)
        {
            speedA = -speedA;
            
            if (speedA > 0)
                labelA.HorizontalAlignment = HorizontalAlignment.Right;
            else
                labelA.HorizontalAlignment = HorizontalAlignment.Left;
        }
        
    }

    public override void _PhysicsProcess(double _delta)
    {
        float delta = (float)_delta;
        labelB.Text = $"_PhysicsProcess: {delta}";
        labelB.Position += new Vector2(speedB, 0);

        if (labelB.Position.X + labelB.GetRect().Size.X > GetViewportRect().Size.X || labelB.Position.X < 0)
        {
            speedB = -speedB;

            if (speedB > 0)
                labelB.HorizontalAlignment = HorizontalAlignment.Right;
            else
                labelB.HorizontalAlignment = HorizontalAlignment.Left;
        }
    }
    
}
