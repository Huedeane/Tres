using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    #region Variable
    //private List<Card> m_BoardDeck;
    //private List<Card> m_BoardPile;
    public List<GameObject> handList;
    #endregion

    #region Getter & Setter
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
        foreach (GameObject zone in handList) {
            GameObject cardObject = GameObject.Find("Card Content").transform.GetChild(GameObject.Find("Card Content").transform.childCount - 1).gameObject;
            cardObject.GetComponent<Card>().MoveCard(zone);
        }
    }

    #endregion
}
