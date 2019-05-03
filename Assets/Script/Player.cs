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

    [SyncVar(hook = "OnTurnChange")]
    public bool myTurn;

    [SyncVar(hook = "OnChangeTextMessage")]
    string textMessage;

    public NetworkManager netMan;
    public GameObject playerCommand;
    public Hand playerHand;
    public GameObject playerNameBox;
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
            GameObject.Find("Game Manager").GetComponent<GameManager>().StartGameButton.SetActive(true);
            GameObject.FindGameObjectWithTag("ConnectionStatus").GetComponent<InfoConnectionStatus>().CmdPlayerConnected(1);
            CmdServerMessage("Player 1 Has Connected");
        }

        if (n == 2)
        {
            
            GameObject.FindGameObjectWithTag("ChatInput").GetComponent<Chatbox>().CmdToggleChat();
            GameObject.FindGameObjectWithTag("ConnectionStatus").GetComponent<InfoConnectionStatus>().CmdPlayerConnected(2);
            AudioManager.Instance.PlaySoundEffect(E_SoundEffect.User_Join);
            GameObject.Find("Game Manager").GetComponent<GameManager>().StartGameButton.SetActive(true);
            CmdServerMessage("Player 2 Has Connected");
        }

    }

    void Start() {
        //Set Up Player

        netMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();
        netMan.GetComponent<NetworkManagerHUD>().showGUI = false;

        
        
        GameObject playerList = GameObject.Find("Player List");
        Debug.Log(playerList);
        transform.SetParent(playerList.transform, false);
        transform.localPosition = Vector3.zero;

        int localPlayerNum = playerNum;
        nameText.text = "P" + playerNum;
        playerScore = 0;

        GameObject.Find("Game Manager").GetComponent<GameManager>().playerNum = localPlayerNum;

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


    }

    

    public override void OnNetworkDestroy()
    {

        CmdServerMessage(playerName + " Disconnected");
        GameObject.FindGameObjectWithTag("ConnectionStatus").GetComponent<InfoConnectionStatus>().CmdPlayerDisconnected(playerNum);
        AudioManager.Instance.PlaySoundEffect(E_SoundEffect.User_Left);
    }


    void OnChangeTextMessage(string msg)
    {
        
        GameObject.FindGameObjectWithTag("Chatbox").GetComponent<TextMeshProUGUI>().text += msg + "\n";
    }

    void OnTurnChange(bool isTurn)
    {

        if (isTurn)
        {
            playerNameBox.GetComponentInChildren<Image>().color = Color.red;
            if (isLocalPlayer)
            {              
                playerHand.SetHighlight(true);
            }
            AudioManager.Instance.PlaySoundEffect(E_SoundEffect.Your_Turn);

        }
        else
        {
            playerNameBox.GetComponentInChildren<Image>().color = Color.white;
            playerHand.SetHighlight(false);
        }

        myTurn = isTurn;
    }

    [Command]
    public void CmdChatMessage(string message)
    {
        AudioManager.Instance.PlaySoundEffect(E_SoundEffect.Chat_Message);
        textMessage = playerName + ": " + message;
    }

    [Command]
    public void CmdServerMessage(string message)
    {
        textMessage = message;
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

        //send cmd to all clients
        RpcClientCommand(cmd);
    }

    [ClientRpc]
    public void RpcDisconnectStatus(bool player1Status, bool player2Status)
    {
        if(player1Status)
            GameObject.FindGameObjectWithTag("ConnectionStatus").GetComponent<InfoConnectionStatus>().CmdPlayerDisconnected(1);
        if(player2Status)
            GameObject.FindGameObjectWithTag("ConnectionStatus").GetComponent<InfoConnectionStatus>().CmdPlayerDisconnected(2);
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

        if (currentMessage != null)
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
