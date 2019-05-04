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
    [SyncVar] private string m_PlayerColor;
    [SyncVar] private int m_PlayerNum, m_PlayerScore;
    [SyncVar] private bool m_MyTurn;

    [SyncVar(hook = "OnChangeTextMessage")]
    private string m_TextMessage;


    private NetworkManager m_NetworkMan;
    private GameObject m_PlayerCommand;
    private GameObject m_PlayerHand;
    private GameObject m_PlayerDebugMessage;
    private IEnumerator m_CurrentDebugMessage;

    public void OnEnable()
    {
        
    }

    public override void OnStartServer()
    {
        
    }


    public void OnChangeTextMessage(string message)
    {

    }


    #region Getter & Setter
    public string PlayerName { get => m_PlayerName; set => m_PlayerName = value; }
    public string PlayerColor { get => m_PlayerColor; set => m_PlayerColor = value; }
    public int PlayerNum { get => m_PlayerNum; set => m_PlayerNum = value; }
    public int PlayerScore { get => m_PlayerScore; set => m_PlayerScore = value; }
    public bool MyTurn { get => m_MyTurn; set => m_MyTurn = value; }
    public string TextMessage { get => m_TextMessage; set => m_TextMessage = value; }
    public NetworkManager NetworkMan { get => m_NetworkMan; set => m_NetworkMan = value; }
    public GameObject PlayerCommand { get => m_PlayerCommand; set => m_PlayerCommand = value; }
    public GameObject PlayerHand { get => m_PlayerHand; set => m_PlayerHand = value; }
    public GameObject PlayerDebugMessage { get => m_PlayerDebugMessage; set => m_PlayerDebugMessage = value; }
    public IEnumerator CurrentDebugMessage { get => m_CurrentDebugMessage; set => m_CurrentDebugMessage = value; }
    #endregion
}
