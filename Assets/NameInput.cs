using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public TestPlayer player;
    public InputField nameField;

    public void SetName()
    {
        player.name = GetName();
    }

    public string GetName()
    {
        return nameField.text;
    }
}
