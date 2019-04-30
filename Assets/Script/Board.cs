using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable 0618

public class Board : NetworkBehaviour {

    #region Variable
    [SerializeField] private GameManager m_Manager;
    [SerializeField] private Deck m_BoardDeck;
    [SerializeField] private Pile m_BoardPile;

    [Header("Board Variable")]
    private bool m_FinishDealing;
    #endregion

    #region Getter & Setter
    public Deck BoardDeck
    {
        get
        {
            return m_BoardDeck;
        }

        set
        {
            m_BoardDeck = value;
        }
    }
    public Pile BoardPile
    {
        get
        {
            return m_BoardPile;
        }

        set
        {
            m_BoardPile = value;
        }
    }
    public GameManager Manager
    {
        get
        {
            return m_Manager;
        }

        set
        {
            m_Manager = value;
        }
    }
    public bool FinishDealing
    {
        get
        {
            return m_FinishDealing;
        }

        set
        {
            m_FinishDealing = value;
        }
    }
    #endregion

    #region Private Method
    #endregion

    #region Public Method
    public void Awake()
    {
        //ShuffleDeck();
    }

    public void UpdateCardList()
    {
        
    }

    public void DrawCard()
    {

    }

    [ClientRpc]
    public void RpcSetTurn()
    {
        StartCoroutine(SetTurn());
    }

    [ClientRpc]
    public void RpcDealCard(string paramPlayer, int paramAmount, float paramDuration)
    {
        FinishDealing = false;

        List<Player> playerList = new List<Player>();
        string[] playerArray = paramPlayer.Split(',');

        Manager.UpdatePlayerList();
        Debug.Log(Manager.PlayerList.Count);

        foreach (Player player in Manager.PlayerList)
        {
            foreach (string playerA in playerArray)
            {
                if (playerA.Equals(player.ToString()))
                {
                    playerList.Add(player);
                }
            }
        }

        StartCoroutine(DealCard_IE(playerList, paramAmount, paramDuration));
        
    }
   
    private IEnumerator DealCard_IE(List<Player> playerList, int amount, float waitTime)
    {
        for (int x = 0; x < amount; x++)
        {
            foreach (Player player in playerList)
            {
                Card card = BoardDeck.GetTopCard();

                if (player.isLocalPlayer){
                    card.Reveal();
                    card.IsInteractable = true;
                }                    
                else
                {
                    card.Hide();
                    card.IsInteractable = false;
                }
                   

                card.MoveCard(player.GetComponentInChildren<Hand>().gameObject);
            }
            yield return new WaitForSeconds(waitTime);
        }
        FinishDealing = true;
    }

    private IEnumerator SetTurn()
    {
        yield return new WaitUntil(() => FinishDealing == true);
        Manager.UpdatePlayerList();
        foreach (Player player in Manager.PlayerList)
        {
            Debug.Log(player.playerNum);
            Debug.Log(player.isLocalPlayer);
            if (player.isLocalPlayer)
                player.playerHand.SetHighlight();
        }
        Manager.PlayerList[0].myTurn = true;
    }
    #endregion
}
