using Godot;
using System;

public class Network : Node
{
    private int DEFAULT_PORT = 28960;
    private int MAX_CLIENTS = 6;

    public string IPAddress { get; set; }

    private static NetworkedMultiplayerENet server;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (OS.GetName() == "Windows")
        {
            IPAddress = IP.GetLocalAddresses()[3].ToString();
        } else  if (OS.GetName() == "Android")
        {
            IPAddress = IP.GetLocalAddresses()[0].ToString();
        } else {
            IPAddress = IP.GetLocalAddresses()[3].ToString();
        }

        GD.Print(IPAddress);

        foreach (var ip in IP.GetLocalAddresses())
        {
            if (ip.ToString().Contains("192.168"))
            {
                IPAddress = ip.ToString();
            }
        }

        GD.Print(IPAddress);

        GetTree().Connect("connected_to_server", this, nameof(OnConnectedToServer));
        GetTree().Connect("server_disconnected", this, nameof(OnDisconnectedFromServer));
        GetTree().Connect("network_peer_packet", this, nameof(OnPeerPacket));
    }

    public void CreateServer() {
        server = new NetworkedMultiplayerENet();
        server.CreateServer(DEFAULT_PORT, MAX_CLIENTS);

        GetTree().NetworkPeer = server;
    }

    private void OnPeerPacket()
    {
        GD.Print("Packet received");
    }

    private void OnDisconnectedFromServer()
    {
        GD.Print("Peer disconnected");
    }

    private void OnConnectedToServer()
    {
        GD.Print("Peer connected");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
