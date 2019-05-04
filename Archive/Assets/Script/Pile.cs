using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 0618

public class Pile : NetworkBehaviour, IZone {

    #region Variable
    [SerializeField] private E_ZoneType m_ZoneType;
    private List<GameObject> m_ZoneCardList;

    [Header("Object Reference")]
    [SerializeField] private Text m_ZoneCardColor;
    [SerializeField] private GameObject m_ZoneCardContent;

    private E_CardColor m_TopCardColor;
    #endregion

    #region Getter & Setter
    public E_ZoneType ZoneType
    {
        get
        {
            return m_ZoneType;
        }

        set
        {
            m_ZoneType = value;
        }
    }
    public List<GameObject> ZoneCardList
    {
        get
        {
            return m_ZoneCardList;
        }

        set
        {
            m_ZoneCardList = value;
        }
    }
    public GameObject ZoneCardContent
    {
        get
        {
            return m_ZoneCardContent;
        }

        set
        {
            m_ZoneCardContent = value;
        }
    }

    public Text ZoneCardColor
    {
        get
        {
            return m_ZoneCardColor;
        }

        set
        {
            m_ZoneCardColor = value;
        }
    }

    public E_CardColor TopCardColor
    {
        get
        {
            return m_TopCardColor;
        }

        set
        {
            m_TopCardColor = value;
        }
    }
    #endregion

    public void Start()
    {
        ZoneCardList = new List<GameObject>();
        Card card = ZoneCardContent.transform.GetChild(0).GetComponent<Card>();
        card.Hide();
    }

    public Card GetTopCard()
    {
        Card TopCard = ZoneCardContent.transform.GetChild(ZoneCardContent.transform.childCount - 1).GetComponent<Card>();
        TopCard.CardColor = TopCardColor;

        return TopCard;
    }

    [ClientRpc]
    public void RpcRandomStartingCard(int seed)
    {
        Random.InitState(seed);
        Card card = ZoneCardContent.transform.GetChild(0).GetComponent<Card>();
        card.CardColor = (E_CardColor)Random.Range(0, 4);
        card.CardType = (E_CardType)Random.Range(0, 9);
        card.UpdateImage();
        card.Reveal();
    }

    [ClientRpc]
    public void RpcSetColor(E_CardColor cardColor)
    {
        TopCardColor = cardColor;
        switch (cardColor)
        {
            case E_CardColor.Blue:
                ZoneCardColor.text = "Blue";
                ZoneCardColor.color = Color.blue;
                break;
            case E_CardColor.Yellow:
                ZoneCardColor.text = "Yellow";
                ZoneCardColor.color = Color.yellow;
                break;
            case E_CardColor.Green:
                ZoneCardColor.text = "Green";
                ZoneCardColor.color = Color.green;
                break;
            case E_CardColor.Red:
                ZoneCardColor.text = "Red";
                ZoneCardColor.color = Color.red;
                break;
            case E_CardColor.Wild:
                ZoneCardColor.text = "Wild";
                ZoneCardColor.color = Color.black;
                break;
        }
    }

    private void UpdateCardList()
    {
        //Clear out the Card List
        ZoneCardList.Clear();

        //Remake the Card List and update card location
        if (transform.childCount > 0)
        {
            
            foreach (Transform child in transform)
            {
                ZoneCardList.Add(child.gameObject);
            }
        }

        if (ZoneCardList.Count == 0)
        {
           Player.localPlayer.CmdServerCommand("End Game 2");
        }

    }

    public void AddCard(GameObject cardObject)
    {
        cardObject.transform.SetParent(ZoneCardContent.transform, true);
        cardObject.GetComponent<RectTransform>().rotation = GetComponent<RectTransform>().rotation;
        RpcSetColor(cardObject.GetComponent<Card>().CardColor);
        UpdateCardList();
    }
}
