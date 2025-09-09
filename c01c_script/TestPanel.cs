using Godot;
using System;

public partial class TestPanel : Panel
{

    public override void _Ready()
    {
        GD.Print($"{nameof(TestPanel)} ready");
        
        GetNode<Button>("Button").Pressed += OnButtonPressed;
    }

    public override void _EnterTree()
    {
        GD.Print($"{nameof(TestPanel)} enter tree");
    }

    bool test = true;
    public override void _Process(double delta)
    {
        if (test) {
            GD.Print($"{nameof(TestPanel)} process");
            test = false;
        }
    }


    public void OnButtonPressed() {
        GetNode<Label>("Label").Text = "Bonjour!";
    }
}
