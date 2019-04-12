using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    #region Variable
    //private List<Card> m_BoardDeck;
    //private List<Card> m_BoardPile;
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

    }
    public void ShuffleDeck()
    {
        //Find Deck GameObject
        //Loop through all child gameObject of the deck and add it to a list of gameobject
        //Do a loop and randomize the sibling index of every gameobject

        
        //Shuffle Sibling Position
        GameObject Deck = GameObject.Find("Deck");
        List<GameObject> Decklist = new List<GameObject>();

        
        foreach (Transform Card in Deck.transform)
        {
            Decklist.Add(Card.gameObject);
        }
        

        foreach (GameObject Card in Decklist)
        {
            Card.transform.SetSiblingIndex(Random.Range(0, Decklist.Count));
        }
        
    }
    #endregion
}
