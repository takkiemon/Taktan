using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestRPSManager : NetworkManager
{
    List<NetworkConnection> playerList;
    public override void OnStartServer()
    {
        Debug.Log("Server Started!");
        playerList = new List<NetworkConnection>();
        playerList.Clear();
    }

    public override void OnStopServer()
    {
        Debug.Log("Server Stopped!");
        playerList.Clear();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Connected to Server!");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Disconnected from Server!");
    }
}