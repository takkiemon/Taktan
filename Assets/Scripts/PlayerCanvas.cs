using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    TestPlayer player;
    Button[] RPSButtons;

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
                button.onClick.AddListener(player.Rock);
            }
            else if (button.name == "PaperButton")
            {
                button.onClick.AddListener(player.Paper);
            }
            else if (button.name == "ScissorsButton")
            {
                button.onClick.AddListener(player.Scissors);
            }
        }
    }
}
