using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPSManager : MonoBehaviour
{
    public int MinimumPlayersForGame = 1;

    public PlayerUIBehavior LocalPlayer;
    public GameObject StartPanel;
    public GameObject GameOverPanel;
    public GameObject HealthTextLabel;
    public GameObject ScoreTextLabel;
    public Text ScoreText;
    public Text PlayerNameText;
    public bool IsGameReady;
    public bool IsGameOver;
    public Text HealthText;
    public Text WinnerNameText;

    public Button Rock, Paper, Scissors;
    public List<PlayerUIBehavior> players = new List<PlayerUIBehavior>();

    void Update()
    {
        if (NetworkManager.singleton.isNetworkActive)
        {
            GameReadyCheck();
            GameOverCheck();

            if (LocalPlayer == null)
            {
                FindLocalPlayer();
            }
            else
            {
                //ShowReadyMenu();
                UpdateStats();
            }
        }
        else
        {
            //Cleanup state once network goes offline
            IsGameReady = false;
            LocalPlayer = null;
            players.Clear();
        }
    }

    void GameReadyCheck()
    {
        if (!IsGameReady)
        {
            //Look for connections that are not in the player list
            foreach (KeyValuePair<uint, NetworkIdentity> kvp in NetworkIdentity.spawned)
            {
                PlayerUIBehavior comp = kvp.Value.GetComponent<PlayerUIBehavior>();

                //Add if new
                if (comp != null && !players.Contains(comp))
                {
                    players.Add(comp);
                }
            }

            //If minimum connections has been check if they are all ready
            if (players.Count >= MinimumPlayersForGame)
            {
                bool AllReady = true;
                foreach (PlayerUIBehavior tank in players)
                {
                    if (!tank.isReady)
                    {
                        AllReady = false;
                    }
                }
                if (AllReady)
                {
                    IsGameReady = true;
                    AllowMovement();

                    //Update Local GUI:
                    StartPanel.SetActive(false);
                    HealthTextLabel.SetActive(true);
                    ScoreTextLabel.SetActive(true);
                }
            }
        }
    }

    void GameOverCheck()
    {
        if (!IsGameReady)
            return;

        //Cant win a game you play by yourself. But you can still use this example for testing network/movement
        if (players.Count == 1)
            return;

        int alivePlayerCount = 0;
        foreach (PlayerUIBehavior player in players)
        {
            if (!player.isDead)
            {
                alivePlayerCount++;

                //If there is only 1 player left alive this will end up being their name
                WinnerNameText.text = player.playerName;
            }
        }

        if (alivePlayerCount == 1)
        {
            IsGameOver = true;
            GameOverPanel.SetActive(true);
            DisableMovement();
        }
    }

    void FindLocalPlayer()
    {
        //Check to see if the player is loaded in yet
        if (ClientScene.localPlayer == null)
            return;

        LocalPlayer = ClientScene.localPlayer.GetComponent<PlayerUIBehavior>();
    }

    void UpdateStats()
    {
        HealthText.text = LocalPlayer.health.ToString();
        ScoreText.text = LocalPlayer.score.ToString();
    }

    public void ReadyButtonHandler()
    {
        LocalPlayer.SendReadyToServer(PlayerNameText.text);
    }

    //All players are ready and game has started. Allow players to move.
    void AllowMovement()
    {
        foreach (PlayerUIBehavior player in players)
        {
            player.allowMovement = true;
        }
    }

    //Game is over. Prevent movement
    void DisableMovement()
    {
        foreach (PlayerUIBehavior player in players)
        {
            player.allowMovement = false;
        }
    }
}
