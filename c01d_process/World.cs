using Godot;
using System;

/// <summary>
/// Comparing _Process and _PhysicsProcess
/// </summary>
public partial class World : Node2D
{
    Label labelA;
    Label labelB;
    Label labelC;
    Label labelD;

    float speedA = 1f;
    float speedB = 1f;
    float speedC = 60f;
    float speedD = 60f;

    public override void _Ready()
    {
        labelA = GetNode<Label>("LabelA");
        labelB = GetNode<Label>("LabelB");
        labelC = GetNode<Label>("LabelC");
        labelD = GetNode<Label>("LabelD");
    }

    public override void _Process(double _delta)
    {
        float delta = (float)_delta;
        labelA.Text = $"_Process: {delta}";
        labelA.Position += new Vector2(speedA, 0);

        if (labelA.Position.X + labelA.GetRect().Size.X > GetViewportRect().Size.X || labelA.Position.X < 0)
        {
            speedA = -speedA;
        }

        // Gestion de LabelC en prenant en compte le delta
        labelC.Text = $"_Process avec Delta : {delta}";
        labelC.Position += new Vector2(speedC * delta, 0);

        if (labelC.Position.X + labelC.GetRect().Size.X > GetViewportRect().Size.X || labelC.Position.X < 0)
        {
            speedC = -speedC;
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
        }

        // Gestion de LabelD en prenant en compte le delta
        labelD.Text = $"_PhysicsProcess avec Delta : {delta}";
        labelD.Position += new Vector2(speedD * delta, 0);

        if (labelD.Position.X + labelD.GetRect().Size.X > GetViewportRect().Size.X || labelD.Position.X < 0)
        {
            speedD = -speedD;
        }
    }
    
}
