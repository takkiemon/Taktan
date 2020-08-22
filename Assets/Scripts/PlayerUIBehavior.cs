﻿using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIBehavior : NetworkBehaviour
{
    [Header("Game Stats")]
    [SyncVar]
    public int health;
    [SyncVar]
    public int score;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public bool allowMovement;
    [SyncVar]
    public bool isReady;

    public bool isDead => health <= 0;
    public TextMesh nameText;

    // Update is called once per frame
    void Update()
    {
        nameText.text = playerName;

        // movement for local player
        if (!isLocalPlayer)
            return;

        //Set local players name color to green
        nameText.color = Color.green;

        if (!allowMovement)
            return;

        if (isDead)
            return;
    }

    public void SendReadyToServer(string playername)
    {
        if (!isLocalPlayer)
            return;

        CmdReady(playername);
    }

    [Command]
    void CmdReady(string playername)
    {
        if (string.IsNullOrEmpty(playername))
        {
            playerName = "PLAYER" + Random.Range(1, 99);
        }
        else
        {
            playerName = playername;
        }

        isReady = true;
    }
}
