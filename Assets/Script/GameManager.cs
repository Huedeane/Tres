using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class GameManager : NetworkBehaviour {

    #region Variable
    [Header("Regular Variable")]
    [SerializeField] private Board m_GameBoard;
    private List<Player> m_PlayerList;

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
    #endregion

    #region Private Method
    public void UpdatePlayerList()
    {
        PlayerList.Clear();
        foreach (Transform child in GameObject.Find("Player List").transform)
        {
            PlayerList.Add(child.GetComponent<Player>());
        }       
    }
    #endregion

    #region Public Method
    public void StartGame()
    {
        if (isServer)
        {
            DeckSeed = Random.Range(1, 1000000);
        }

        GameBoard.BoardDeck.ShuffleDeck(DeckSeed);

        foreach (Player player in PlayerList)
        {

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
