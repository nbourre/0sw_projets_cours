using Godot;
using System;
using System.Linq;

// Source : https://www.youtube.com/watch?v=lpkaMKE081M

public class Network : Node
{
    private int DEFAULT_PORT = 28960;
    private int MAX_CLIENTS = 6;

    public string IPAddress { get; set; }

    private static NetworkedMultiplayerENet server;
    private static NetworkedMultiplayerENet client;

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

        // Get the IP with access to the internet
        // Source : https://stackoverflow.com/questions/13725844/how-to-get-ip-address-of-network-adapter-used-to-access-the-internet
        System.Net.IPAddress ip = System.Net.Dns.GetHostEntry(System.Environment.MachineName)
            .AddressList.Where(i => i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();

        GD.Print($"Device with internet : {ip.ToString()}");

        IPAddress = ip.ToString();

        GD.Print(IPAddress);

        GetTree().Connect("connected_to_server", this, nameof(OnConnectedToServer));
        GetTree().Connect("server_disconnected", this, nameof(OnDisconnectedFromServer));
    }


    public void CreateServer() {
        server = new NetworkedMultiplayerENet();
        server.CreateServer(DEFAULT_PORT, MAX_CLIENTS);

        GetTree().NetworkPeer = server;
    }

    public void JoinServer() {
        client = new NetworkedMultiplayerENet();
        client.CreateClient(IPAddress, DEFAULT_PORT);

        GetTree().NetworkPeer = client;
    }

    private void OnDisconnectedFromServer()
    {
        GD.Print("Peer disconnected");
    }

    private void OnConnectedToServer()
    {
        GD.Print("Peer connected");
    }

}
