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
        m_Manager.startScoreTrack = true;
        StartCoroutine(SetTurn());
    }

    [ClientRpc]
    public void RpcSetTurnWithNum(int num)
    {
        StartCoroutine(SetTurn(num));
    }



    [ClientRpc]
    public void RpcDealCard(string paramPlayer, int paramAmount, float paramDuration)
    {
        Debug.Log("Dealing Card " + paramPlayer);
        FinishDealing = false;

        List<Player> playerList = new List<Player>();
        string[] playerArray = paramPlayer.Split(',');

        Manager.UpdatePlayerList();

        foreach (Player player in Manager.PlayerList)
        {
            foreach (string playerA in playerArray)
            {
                Debug.Log(playerA);
                if (playerA.Equals(player.ToString()))
                {
                    playerList.Add(player);
                }
            }
        }

        StartCoroutine(DealCard_IE(playerList, paramAmount, paramDuration));
        
    }

    public IEnumerator DealCard_IE(Player player, int amount, float waitTime)
    {
        AudioManager.Instance.PlaySoundEffect(E_SoundEffect.Deal_Card);
        for (int x = 0; x < amount; x++)
        {
            Card card = BoardDeck.GetTopCard();

            if (player.isLocalPlayer)
            {
                card.Reveal();
                card.IsInteractable = true;
            }
            else
            {
                card.Hide();
                card.IsInteractable = false;
            }

            card.MoveCard(player.GetComponentInChildren<Hand>().gameObject);
            yield return new WaitForSeconds(waitTime);
        }
        AudioManager.Instance.DealCard.Stop();
        FinishDealing = true;
    }

    public IEnumerator DealCard_IE(List<Player> playerList, int amount, float waitTime)
    {
        AudioManager.Instance.PlaySoundEffect(E_SoundEffect.Deal_Card);
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
        AudioManager.Instance.DealCard.Stop();
        FinishDealing = true;
    }

    private IEnumerator SetTurn()
    {
           yield return new WaitUntil(() => FinishDealing == true);
        Manager.UpdatePlayerList();
        Manager.PlayerList[0].myTurn = true;
    }

    private IEnumerator SetTurn(int playerNum)
    {
        yield return new WaitUntil(() => FinishDealing == true);
        Debug.Log("Finish Drawing");
        Manager.PlayerList[playerNum - 1].myTurn = true;
        Manager.PlayerList[playerNum - 1].getNextPlayer().myTurn = false;
        if (isLocalPlayer)
        {
            Manager.PlayerList[playerNum - 1].playerHand.SetHighlight(true);
        }
    }
    #endregion
}
