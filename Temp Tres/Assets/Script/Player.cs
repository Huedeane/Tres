using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class Player : NetworkBehaviour
{
    public static Player LocalPlayer;

    #region Sync Variable
    #endregion

    [SyncVar] private string m_PlayerName;
    [SyncVar] private string m_PlayerWildColor;
    [SyncVar] private int m_PlayerNum, m_PlayerScore;
    [SyncVar] private bool m_MyTurn;

    [SyncVar(hook = "OnChangeChatMessage")]
    private string m_ChatMessage;


    private NetworkManager m_NetMan;
    private GameObject m_PlayerCommand;
    private GameObject m_PlayerHand;
    private GameObject m_PlayerDebugMessage;
    private IEnumerator m_CurrentDebugMessage;

    public void OnEnable()
    {
        

      
    }

    public override void OnStartServer()
    {
        NetMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();
        PlayerName = "P" + NetMan.numPlayers;
        PlayerWildColor = "Red";
        PlayerNum = NetMan.numPlayers;
        PlayerScore = 0;
        MyTurn = false;       
    }

    private void Start()
    {
        NetMan = GameObject.Find("Net Man").GetComponent<NetworkManager>();
        NetMan.GetComponent<NetworkManagerHUD>().showGUI = false;

        transform.SetParent(GameObject.Find("Player List").transform, false);
        transform.localPosition = Vector3.zero;


        if (isLocalPlayer)
        {

            LocalPlayer = this;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            foreach (Transform child in PlayerCommand.transform)
            {
                if (child.name == "Play Button" || child.name == "Extra Button" || child.name == "Draw Button" || child.name == "Wild Button")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        EventObject eventObject = new EventObject();
        eventObject.TypeString = new List<string>() { PlayerName };
        EventManager.TriggerEvent(E_EventName.Player_Join, eventObject);
    }


    public void OnChangeTextMessage(string message)
    {

    }


    #region Getter & Setter
    public string PlayerName { get => m_PlayerName; set => m_PlayerName = value; }
    public string PlayerWildColor { get => m_PlayerWildColor; set => m_PlayerWildColor = value; }
    public int PlayerNum { get => m_PlayerNum; set => m_PlayerNum = value; }
    public int PlayerScore { get => m_PlayerScore; set => m_PlayerScore = value; }
    public bool MyTurn { get => m_MyTurn; set => m_MyTurn = value; }
    public string ChatMessage { get => m_ChatMessage; set => m_ChatMessage = value; }
    public NetworkManager NetMan { get => m_NetMan; set => m_NetMan = value; }
    public GameObject PlayerCommand { get => m_PlayerCommand; set => m_PlayerCommand = value; }
    public GameObject PlayerHand { get => m_PlayerHand; set => m_PlayerHand = value; }
    public GameObject PlayerDebugMessage { get => m_PlayerDebugMessage; set => m_PlayerDebugMessage = value; }
    public IEnumerator CurrentDebugMessage { get => m_CurrentDebugMessage; set => m_CurrentDebugMessage = value; }
    #endregion
}
