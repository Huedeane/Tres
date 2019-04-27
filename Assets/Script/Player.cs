using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 0618

public class Player : NetworkBehaviour
{

    [SyncVar]
    public string playerName;

    [SyncVar]
    public int playerNum, playerScore;

    [SyncVar]
    public bool myTurn;


    public Hand playerHand;

    public Text nameText;
    public Text messageText;
    public IEnumerator currentMessage;

    public override void OnStartServer()
    {
        NetworkManager netMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();

        int n = netMan.numPlayers;
        playerScore = 0;
        playerNum = n;
        playerName = "P" + n;

        if (n == 1)
            myTurn = true;
        else
            myTurn = false;

        Debug.Log(n);

        if (n == 2)
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().StartGame();
        }

        
    }

    void Start () {
        //Set Up Player
        

        NetworkManager netMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();
        
        netMan.GetComponent<NetworkManagerHUD>().showGUI = false;

        GameObject playerList = GameObject.Find("Player List");
        transform.SetParent(playerList.transform, false);
        transform.localPosition = Vector3.zero;

        int localPlayerNum = playerNum;
        nameText.text = playerName;

        if (isLocalPlayer)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        

        /*
        for (int x = 1; x <= playerList.transform.childCount - 1; x++)
        {

            int playerNum = GetPreviousPlayer(localPlayerNum, x, playerList.transform.childCount);
            Debug.Log("playerNum: " + playerNum + " num: " + x);
            Transform playerSlot = playerList.transform.GetChild(playerNum - 1);
            transform.eulerAngles = new Vector3(0, 0, x * -90);

        }
        */

    }

    /*
    int GetPreviousPlayer(int num, int minus, int max)
    {
        int x = (num - minus) % 4;
        if (x == 0)
            return max;
        else
            return x;
    }
    */

    [Command]
    public void CmdServerCommand(string cmd)
    {
        //send cmd to all clients
        RpcClientCommand(cmd);
    }

    [Command]
    public void CmdServerCommandTurn(string cmd)
    {
        //If it's not your turn return void
        if (myTurn == false)
            return;

        //Get the other player
        Player otherPlayer = getNextPlayer();

        //Set the turn to other player
        myTurn = false;
        otherPlayer.myTurn = true;

        //send cmd to all clients
        RpcClientCommand(cmd);
    }

    [ClientRpc]
    public void RpcClientCommand(string cmd)
    {
        //Process Client Side
        GetComponent<PlayerCommand>().processCmd(cmd);
    }

    public Player getNextPlayer()
    {
        int nextIndex = (transform.GetSiblingIndex() + 1) % transform.parent.childCount;
        return transform.parent.GetChild(nextIndex).GetComponent<Player>();
    }

    public void sendMessage(string message)
    {
        
        if(currentMessage != null)
            StopCoroutine(currentMessage);

        currentMessage = sendMessage_IE(message);
        StartCoroutine(currentMessage);
    }

    public IEnumerator sendMessage_IE(string message)
    {
        messageText.text = message;
        Color c = messageText.color;
        c.a = 1;
        messageText.color = c;

        //Time
        float time = 0f;
        float timeToFinish = 2f;

        //Alpha
        float currentAlpha = messageText.color.a;
        float targetAlpha = 0f;

        while (currentAlpha != targetAlpha)
        {
            time += Time.deltaTime / timeToFinish;

            currentAlpha = messageText.color.a;

            Color alphaColor = messageText.color;
            alphaColor.a = Mathf.Lerp(currentAlpha, targetAlpha, time);
            messageText.color = alphaColor;

            yield return null;
        }

    }

}
