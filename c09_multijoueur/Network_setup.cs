using Godot;
using System;

public class Network_setup : Control
{
    Control multiplayer_configure;    
    LineEdit server_ip_address;
    Label device_ip_address;

    Network network;

    Node2D player;
    Node Players;
    Global Global;

    RandomNumberGenerator rng;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        multiplayer_configure = GetNode<Control>("Multiplayer_configure");
        server_ip_address = GetNode<LineEdit>("Multiplayer_configure/Server_ip_address");
        device_ip_address = GetNode<Label>("CanvasLayer/Device_ip_address");
        network = GetNode<Network>("/root/Network");
        Global = GetNode<Global>("/root/Global");
        player = ResourceLoader.Load<PackedScene>("res://Player.tscn").Instance() as Node2D;
        Players = GetNode<Node>("/root/Players");

        GetTree().Connect("network_peer_connected", this, nameof(_player_connected));
        GetTree().Connect("network_peer_disconnected", this, nameof(_player_disconnected));
        GetTree().Connect("connected_to_server", this, nameof(_connected_to_server));
        
        device_ip_address.Text = network.IPAddress;

        GD.Print($"Label IP : {device_ip_address.Text}");
    }

    public void _player_connected(int id)
    {
        instance_player(id);
        GD.Print("Player connected: " + id);
    }

    public void _player_disconnected(int id)
    {
        var disconnected_player = Players.GetChild(id);

        if (disconnected_player != null)
        {
            GD.Print($"Removing player {id} from Queue");
            disconnected_player.QueueFree();
            
        }

        GD.Print("Player disconnected: " + id);
    }

    public async void _connected_to_server()
    {
        // Attendre 0.1 secondes pour que le serveur soit prêt
        await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
        instance_player(GetTree().GetNetworkUniqueId());
        GD.Print("Connected to server");
    }

    public void _on_Create_server_pressed() {
        multiplayer_configure.Hide();
        network.CreateServer();

        GD.Print("_on_Create_server_pressed");
        // Instanciation de notre joueur
        instance_player(GetTree().GetNetworkUniqueId());
    }

    public void _on_Join_server_pressed() {
        if (server_ip_address.Text != "") {
            multiplayer_configure.Hide();
            network.IPAddress = server_ip_address.Text;
            network.JoinServer();
        }
    }

    public void instance_player(int id) {
        GD.Print($"instance_player({id})");
        var player_instance = Global.InstanceNodeAtLocation(player, Players, 
            new Vector2(
                rng.RandfRange(0, GetViewport().Size.x), 
                rng.RandfRange(0, GetViewport().Size.y)
        ));

        player_instance.Name = id.ToString();
        player_instance.SetNetworkMaster(id);
    }
}
