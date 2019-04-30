using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class GameManager : NetworkBehaviour {

    #region Variable  
    [SerializeField] private Board m_GameBoard;
    [SerializeField] private GameObject m_PlayerListRef;
    [SerializeField] private GameObject m_StartGameButton;
    [SerializeField] private List<Player> m_PlayerList;

    [Header("Network Variable")]
    [SyncVar]
    private int m_GameDuration;
    [SyncVar]
    private int m_DeckSeed;

    
    
    #endregion

    #region Getter & Setter
    public Board GameBoard
    {
        get
        {
            return m_GameBoard;
        }

        set
        {
            m_GameBoard = value;
        }
    }
    public List<Player> PlayerList
    {
        get
        {
            return m_PlayerList;
        }

        set
        {
            m_PlayerList = value;
        }
    }
    public int GameDuration
    {
        get
        {
            return m_GameDuration;
        }

        set
        {
            m_GameDuration = value;
        }
    }
    public int DeckSeed
    {
        get
        {
            return m_DeckSeed;
        }

        set
        {
            m_DeckSeed = value;
        }
    }

    public GameObject PlayerListRef
    {
        get
        {
            return m_PlayerListRef;
        }

        set
        {
            m_PlayerListRef = value;
        }
    }

    public GameObject StartGameButton
    {
        get
        {
            return m_StartGameButton;
        }

        set
        {
            m_StartGameButton = value;
        }
    }
    #endregion

    #region Private Method

    public void UpdatePlayerList()
    {
        PlayerList = new List<Player>(PlayerListRef.transform.GetComponentsInChildren<Player>());
    }
    #endregion

    #region Public Method
    public void StartGame()
    {
        UpdatePlayerList();

        if (isServer)
        {
            DeckSeed = Random.Range(1, 1000000);
            GameBoard.BoardDeck.RpcShuffleDeck(DeckSeed);
            GameBoard.BoardPile.RpcRandomStartingCard(DeckSeed);

            string convertedString = RpcStringConverter<Player>.ConvertString(PlayerList);
            
            GameBoard.RpcDealCard(convertedString, 5, 1f);
            GameBoard.RpcSetTurn();
        }

    }

    public void EndGame()
    {

    }

    public void SetPlayerSequence()
    {

    }


    
    #endregion
}
