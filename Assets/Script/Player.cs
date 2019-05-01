using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 0618

public class Player : NetworkBehaviour
{
    public static Player localPlayer;

    [SyncVar]
    public string playerName;

    [SyncVar]
    public int playerNum, playerScore;

    [SyncVar]
    public bool myTurn;

    [SyncVar]
    public string player1Status = "Waiting", player2Status = "Waiting";

    [SyncVar(hook = "OnChangeTextMessage")]
    string textMessage;

    public NetworkManager netMan;
    public GameObject playerCommand;
    public Hand playerHand;
    public Text nameText;
    public Text messageText;
    public IEnumerator currentMessage;

    public override void OnStartServer()
    {
        netMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();

        int n = netMan.numPlayers;
        playerScore = 0;
        playerNum = n;
        playerName = "Player " + n;

        myTurn = false;

        if (n == 1)
        {

        }

        if (n == 2)
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().StartGameButton.SetActive(true);
        }

    }

    void Start () {
        //Set Up Player

        netMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();
        netMan.GetComponent<NetworkManagerHUD>().showGUI = false;

        GameObject playerList = GameObject.Find("Player List");
        transform.SetParent(playerList.transform, false);
        transform.localPosition = Vector3.zero;

        int localPlayerNum = playerNum;
        nameText.text = "P" + playerNum;

        if (isLocalPlayer)
        {
            localPlayer = this;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            foreach (Transform child in playerCommand.transform)
            {
                if (child.name == "Play Button" || child.name == "Extra Button" 
                    || child.name == "Draw Button" || child.name == "Quit Button")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        if (isServer)
        {
            
        }

    }

    private void Update()
    {
        if (isServer)
        {
            if (netMan.numPlayers != 2)
                GameObject.FindGameObjectWithTag("ChatInput").GetComponent<InputField>().interactable = false;
            if (netMan.numPlayers == 2 && !GameObject.FindGameObjectWithTag("ChatInput").GetComponent<InputField>().IsInteractable())
                CmdServerCommand("Enable Chat");
        }

    }

    void OnChangeTextMessage(string msg)
    {
        GameObject.FindGameObjectWithTag("Chatbox").GetComponent<TextMeshProUGUI>().text += msg + "\n";
    }

    [Command]
    public void CmdChatMessage(string message)
    {
        textMessage = playerName + ": " + message;
    }

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

    public override string ToString()
    {
        return playerNum.ToString();
    }

}
