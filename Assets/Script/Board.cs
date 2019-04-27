using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    #region Variable
    [SerializeField] private Deck m_BoardDeck;
    [SerializeField] private Pile m_BoardPile;
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
    public void DealCard()
    {
        /*
        foreach (GameObject zone in handList) {
            GameObject cardObject = GameObject.Find("Card Content").transform.GetChild(GameObject.Find("Card Content").transform.childCount - 1).gameObject;
            cardObject.GetComponent<Card>().MoveCard(zone);
        }
        */
    }

    #endregion
}
