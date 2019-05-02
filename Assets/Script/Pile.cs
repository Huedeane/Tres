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
    [SerializeField] private Text m_ZoneAmountText;
    [SerializeField] private GameObject m_ZoneCardContent;
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
    public Text ZoneAmountText
    {
        get
        {
            return m_ZoneAmountText;
        }

        set
        {
            m_ZoneAmountText = value;
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
    #endregion

    public void Start()
    {
        ZoneCardList = new List<GameObject>();
        Card card = ZoneCardContent.transform.GetChild(0).GetComponent<Card>();
        card.Hide();
    }

    public Card GetTopCard()
    {
        return ZoneCardContent.transform.GetChild(ZoneCardContent.transform.childCount - 1).GetComponent<Card>();
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

    private void Update()
    {
        ZoneAmountText.GetComponent<Text>().text = (ZoneCardContent.transform.childCount - 1).ToString();
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

    }

    public void AddCard(GameObject cardObject)
    {
        cardObject.transform.SetParent(ZoneCardContent.transform, true);
        cardObject.GetComponent<RectTransform>().rotation = GetComponent<RectTransform>().rotation;
        UpdateCardList();
    }
}
