using Godot;
using System;

public class Network_setup : Control
{
    Control multiplayer_configure;    
    LineEdit server_ip_address;
    Label device_ip_address;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        multiplayer_configure = GetNode<Control>("Multiplayer_configure");
        server_ip_address = GetNode<LineEdit>("Server_ip_address");
        device_ip_address = GetNode<Label>("Device_ip_address");
    }

    public void _on_Create_server_pressed() {
        
    }

    public void _on_Join_server_pressed() {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
