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

    }

    void Start () {
        //Set Up Player
        

        NetworkManager netMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();
        netMan.GetComponent<NetworkManagerHUD>().showGUI = false;

        GameObject playerList = GameObject.Find("Player List");
        transform.SetParent(playerList.transform, false);
        transform.localPosition = Vector3.zero;

        int localPlayerNum = playerNum;

        if (isLocalPlayer)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {

            //Get other player number
            if (netMan.numPlayers == 2){
                int rightPlayerNum = Mathf.Abs((localPlayerNum + 1)) % 4;
                Transform rightPlayerSlot = playerList.transform.GetChild(rightPlayerNum);
                rightPlayerSlot.eulerAngles = new Vector3(0, 0, 90);
            }
            
            /*
            int topPlayerNum = (localPlayerNum + 2) % 4;
            int leftPlayerNum = (localPlayerNum + 3) % 4;

            //Get player slot from list using player number
            
            Transform topPlayerSlot = playerList.transform.GetChild(topPlayerNum);
            Transform leftPlayerSlot = playerList.transform.GetChild(leftPlayerNum);

            //Adjust rotation depending on local player location
            
            topPlayerSlot.eulerAngles = new Vector3(0, 0, 180);
            leftPlayerSlot.eulerAngles = new Vector3(0, 0, -90);
            */

        }

    }

    void Update()
    {
        GameObject.Find("Player Name").GetComponentInChildren<Text>().text = playerName;
    }

}
