using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    #region Variable
    [SerializeField] private int m_ZoneId;
    [SerializeField] private E_ZoneType m_ZoneType;
    private List<GameObject> m_ZoneCardList;

    [Header("Zone Child Object")]
    [SerializeField] private GameObject m_ZoneAmountText;
    [SerializeField] private GameObject m_ZoneCardContent;
    #endregion

    #region Getter & Setter
    public int ZoneId
    {
        get
        {
            return m_ZoneId;
        }

        set
        {
            m_ZoneId = value;
        }
    }
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
    public GameObject ZoneAmountText
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

        ShuffleDeck();

        m_ZoneAmountText.GetComponent<TextMeshProUGUI>().SetText(m_ZoneCardList.Count.ToString());
    }

    public void ShuffleDeck()
    {

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


}
