using Godot;
using System;

public class TestButton : Button
{
  public override void _Ready()
  {
    GD.Print($"{nameof(TestButton)} ready");
    //GetNode("Button").Connect("pressed", this, nameof(OnButtonPressed));
  }
  public override void _EnterTree()
  {
    GD.Print($"{nameof(TestButton)} enter tree");
  }
  bool test = true;
  public override void _Process(float delta)
  {
    if (test) {
      GD.Print($"{nameof(TestButton)} process");
      test = false;
    }
  }
}
