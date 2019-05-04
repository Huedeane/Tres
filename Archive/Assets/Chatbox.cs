using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 0618

public class Chatbox : NetworkBehaviour
{
    public InputField InputField;

    [SyncVar]
    public bool CanInteract = false;

    [Command]
    public void CmdToggleChat()
    {
        CanInteract = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        InputField.interactable = CanInteract;

        if (InputField.text != "" && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)))
        {
            Player.localPlayer.CmdChatMessage(InputField.text);
            InputField.text = "";
            InputField.Select();
            InputField.ActivateInputField();
        }

    }
}
