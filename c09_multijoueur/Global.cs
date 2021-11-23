using Godot;
using System;

public class Global : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public Node InstanceNodeAtLocation(Node2D node, Node parent, Vector2 location)
    {
        Node2D instance = (Node2D)InstanceNode((Node)node, parent);
        instance.GlobalPosition = location;
        return instance;
    }

    public Node InstanceNode (Node node, Node parent)
    {
        Node newNode = node.Duplicate();
        parent.AddChild(parent);
        return newNode;
    }

}
