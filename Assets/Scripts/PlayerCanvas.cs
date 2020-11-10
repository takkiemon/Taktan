using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    TestPlayer player;
    Button[] RPSButtons; // RPSButtons[0] = Rock, [1] = Paper, [2] = Scissors

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerScript(TestPlayer playerScript)
    {
        player = playerScript;
    }

    public void SetButtons()
    {
        RPSButtons = GetComponentsInChildren<Button>();
        foreach(Button button in RPSButtons)
        {
            if (button.name == "RockButton")
            {
                RPSButtons[0] = button;
                button.onClick.AddListener(player.Rock);
            }
            else if (button.name == "PaperButton")
            {
                RPSButtons[1] = button;
                button.onClick.AddListener(player.Paper);
            }
            else if (button.name == "ScissorsButton")
            {
                RPSButtons[2] = button;
                button.onClick.AddListener(player.Scissors);
            }
        }
    }
}
