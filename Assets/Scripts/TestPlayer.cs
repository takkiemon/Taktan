using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnRockCountChanged))]
    int rockCount = 0;
    int playerID;
    public GameObject canvasPrefab;
    private GameObject playerCanvas;

    private void Start()
    {
        if (isLocalPlayer)
        {
            playerCanvas = Instantiate(canvasPrefab, this.gameObject.transform);
            playerCanvas.GetComponent<PlayerCanvas>().SetPlayerScript(this);
            playerCanvas.GetComponent<PlayerCanvas>().SetButtons();
            playerCanvas.SetActive(false);
        }
    }

    void HandleMovement()
    {
        if(isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }

    struct OrderMessage // this would be used to send all the choices a player made in one command, so all the decisions would be sent at once instead of trickling in. This design choice is made to possibly prevent cheating by the person who hosts.
    {
        int playerID;
        int noOfActions;

    }

    private void Update()
    {
        HandleMovement();

        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Rock();
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Paper();
            }
            else if(Input.GetKeyDown(KeyCode.E))
            {
                Scissors();
            }
        }
    }

    [Command]
    public void Rock()
    {
        Debug.Log("Mune ni Rocks!");
    }

    [Command]
    public void Paper()
    {
        Debug.Log("Beetje Peppah!");
    }

    [Command]
    public void Scissors()
    {
        Debug.Log("schaar");
    }

    [TargetRpc]
    public void ReplyRock()
    {
        Debug.Log("Received Rock from Server!");
    }

    [ClientRpc]
    public void TooHigh()
    {
        Debug.Log("Too darn high!");
    }

    public void OnRockCountChanged(int oldCount, int newCount)
    {
        Debug.Log($"{oldCount} Rocks increased to {newCount} Rocks.");
    }
}
