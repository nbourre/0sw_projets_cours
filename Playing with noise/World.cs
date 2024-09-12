using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;
using System.Linq;

public partial class World : Node2D
{

    Vector2 windowSize;
    Vector2 startPoint;
    Array<Vector2> points = new();

    FastNoiseLite noise = new();
    Camera2D camera;
    Label InfoLabel;

    public override void _Ready()
    {
        camera = GetViewport().GetCamera2D();
        InfoLabel = GetNode<Label>("Info");

        GD.Randomize();
        
        GD.Print("Camera: " + camera.Position);

        windowSize = GetViewport().GetVisibleRect().Size;

        startPoint = new Vector2(0, windowSize.Y / 2);
        points.Add(startPoint);
        
        GD.Print("Window size: " + windowSize);

        noise.Seed = GD.RandRange(0, 1500);
        noise.NoiseType = FastNoiseLite.NoiseTypeEnum.Perlin;
    }

    public override void _PhysicsProcess(double delta)
    {
        GeneratePath();

        camera.Position = points[points.Count - 1];
        
        InfoLabel.Position = camera.Position;
        InfoLabel.Text = "Camera: " + camera.Position;
        InfoLabel.Text += "\nPoints: " + points.Count;
        QueueRedraw();
    }

    public void GeneratePath()
    {
        // Générer un nouveau point de bruit
        Vector2 newPoint = new Vector2(points[points.Count - 1].X + 1, points[points.Count - 1].Y + noise.GetNoise1D(points.Count * 1f) * 10);
        points.Add(newPoint);
    }

    public override void _Draw()
    {
        if (points.Count < 2)
            return;


        // Faire un effet de déplacement de la caméra en fonction de la position du dernier point

        DrawPolyline(points.TakeLast((int)windowSize.X).ToArray(), Colors.White, 2f);

        base._Draw();
    }
}
