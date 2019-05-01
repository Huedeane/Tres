using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chatbox : MonoBehaviour
{
    InputField InputField;

    private void Start()
    {
        InputField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {

        if (InputField.text != "" && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            Player.localPlayer.CmdChatMessage(InputField.text);
            InputField.text = "";
            InputField.Select();
            InputField.ActivateInputField();
        }

    }
}
