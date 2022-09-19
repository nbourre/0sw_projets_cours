using Godot;
using System;

public class TestPanel : Panel
{

    public override void _Ready()
    {
        GD.Print($"{nameof(TestPanel)} ready");
        GetNode("Button").Connect("pressed", this, nameof(OnButtonPressed));
    }

    public override void _EnterTree()
    {
        GD.Print($"{nameof(TestPanel)} enter tree");
    }

    bool test = true;
    public override void _Process(float delta)
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
