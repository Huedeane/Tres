using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class InfoConnectionStatus : NetworkBehaviour {

    [SyncVar]
    public string player1Status = "Waiting", player2Status = "Waiting";

    public TextMeshProUGUI Player1;
    public TextMeshProUGUI Player2;

    private void Start()
    {
        this.gameObject.SetActive(true);
    }

    [Command]
    public void CmdPlayerConnected(int x)
    {
        if (x == 1)
        {
            player1Status = "Connected";
        }
        if(x == 2)
        {
            player2Status = "Connected";
        }
    }

    [Command]
    public void CmdPlayerDisconnected(int x)
    {
        if (x == 1)
        {
            player1Status = "Disconnected";
        }
        if (x == 2)
        {
            player2Status = "Disconnected";
        }
    }

    [Command]
    public void CmdSetStatus(int x, string status)
    {
        if (x == 1)
        {
            player1Status = status;
        }
        if (x == 2)
        {
            player2Status = status;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Player1.SetText("Player 1: " + player1Status);
        Player2.SetText("Player 2: " + player2Status);
    }
}
