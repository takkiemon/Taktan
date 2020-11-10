using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    InputField nameField;

    public string SetName()
    {
        return nameField.text;
    }
}
