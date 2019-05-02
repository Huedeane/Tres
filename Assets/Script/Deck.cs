using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

#pragma warning disable 0618

public class Deck : NetworkBehaviour
{
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

    private void Awake()
    {
        m_ZoneCardList = new List<GameObject>();
        
        foreach (Transform card in ZoneCardContent.transform)
        {
            m_ZoneCardList.Add(card.gameObject);
        }

    }

    private void Update()
    {
        m_ZoneAmountText.GetComponent<Text>().text = ZoneCardContent.transform.childCount.ToString();
    }

    public Card GetTopCard()
    {
        return ZoneCardContent.transform.GetChild(ZoneCardContent.transform.childCount - 1).GetComponent<Card>();
    }

    [ClientRpc]
    public void RpcShuffleDeck(int seed)
    {
        Random.InitState(seed);
        foreach (Transform card in ZoneCardContent.transform)
        {
            card.transform.SetSiblingIndex(Random.Range(0, m_ZoneCardList.Count));
        }

        m_ZoneCardList.Clear();

        foreach (Transform card in ZoneCardContent.transform)
        {
            m_ZoneCardList.Add(card.gameObject);
        }
    }

    [ClientRpc]
    public void RpcSetId()
    {
        int x = 0;

        foreach (Transform card in ZoneCardContent.transform)
        {
            card.GetComponent<Card>().CardObjectId = x;
            x++;
        }
    }


}
