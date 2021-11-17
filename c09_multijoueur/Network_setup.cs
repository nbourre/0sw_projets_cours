using Godot;
using System;

public class Network_setup : Control
{
    Control multiplayer_configure;    
    LineEdit server_ip_address;
    Label device_ip_address;

    Network network;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        multiplayer_configure = GetNode<Control>("Multiplayer_configure");
        server_ip_address = GetNode<LineEdit>("Multiplayer_configure/Server_ip_address");
        device_ip_address = GetNode<Label>("CanvasLayer/Device_ip_address");
        network = GetNode<Network>("/root/Network");

        GetTree().Connect("network_peer_connected", this, nameof(_player_connected));
        GetTree().Connect("network_peer_disconnected", this, nameof(_player_disconnected));
        GetTree().Connect("connected_to_server", this, nameof(_connected_to_server));
        
        device_ip_address.Text = network.IPAddress;

        GD.Print($"Label IP : {device_ip_address.Text}");
    }

    public void _player_connected(int id)
    {
        GD.Print("Player connected: " + id);
    }

    public void _player_disconnected(int id)
    {
        GD.Print("Player disconnected: " + id);
    }

    public void _connected_to_server()
    {
        GD.Print("Connected to server");
    }

    public void _on_Create_server_pressed() {
        multiplayer_configure.Hide();
        network.CreateServer();
    }

    public void _on_Join_server_pressed() {
        if (server_ip_address.Text != "") {
            multiplayer_configure.Hide();
            network.IPAddress = server_ip_address.Text;
            network.JoinServer();
        }
    }
}
