using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour, IZone
{

    [Header("Card Holder Prefab")]
    [SerializeField] private List<GameObject> m_HandCardList;
    [SerializeField] private GameObject m_CardHolderPrefab;
    [SerializeField] private float m_CardWidth;
    [SerializeField] private Card m_SelectedCard;
    [SerializeField] private bool m_HasAvailable;

    #region Getter & Setter
    public List<GameObject> HandCardList
    {
        get
        {
            return m_HandCardList;
        }

        set
        {
            m_HandCardList = value;
        }
    }

    public GameObject CardHolderPrefab
    {
        get
        {
            return m_CardHolderPrefab;
        }

        set
        {
            m_CardHolderPrefab = value;
        }
    }

    public float CardWidth
    {
        get
        {
            return m_CardWidth;
        }

        set
        {
            m_CardWidth = value;
        }
    }

    public Card SelectedCard
    {
        get
        {
            return m_SelectedCard;
        }

        set
        {
            m_SelectedCard = value;
        }
    }

    public bool HasAvailable
    {
        get
        {
            return m_HasAvailable;
        }

        set
        {
            m_HasAvailable = value;
        }
    }
    #endregion

    private void ControlSizeWidth()
    {

        float layoutWidth = this.GetComponent<RectTransform>().rect.width;
        float layoutSpacing = this.GetComponent<HorizontalLayoutGroup>().spacing;

        float totalSize = (CardWidth + layoutSpacing) * this.transform.childCount;

        if (totalSize > layoutWidth)
        {
            GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = true;
            GetComponent<HorizontalLayoutGroup>().childControlWidth = true;
        }
        else
        {
            GetComponent<HorizontalLayoutGroup>().childForceExpandWidth = false;
            GetComponent<HorizontalLayoutGroup>().childControlWidth = false;

            foreach (Transform child in transform)
            {
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(CardWidth, 100);

            }
        }
    }

    public void UpdateCardList()
    {
        //Clear out the Card List
        HandCardList.Clear();

        //Remake the Card List and update card location
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                if (child.childCount == 0)
                    Destroy(child.gameObject);
                else
                {
                    Transform innerChild = child.GetChild(0);
                    HandCardList.Add(innerChild.gameObject);
                }
            }
        }

        ControlSizeWidth();
    }

    public void SetHighlight()
    {
        HasAvailable = false;
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                Transform innerChild = child.GetChild(0);
                Card innerCard = innerChild.GetComponent<Card>();
                Pile pile = GameObject.Find("Board").GetComponent<Board>().BoardPile;
                if (innerCard.CheckValid(pile.GetTopCard()))
                {
                    innerCard.ToggleAvailable(true);
                    HasAvailable = true;
                }               
            }
        }
    }

    public void AddCard(GameObject cardObject)
    {
        GameObject newHolder = Instantiate(CardHolderPrefab, transform.position, transform.rotation) as GameObject;
        newHolder.transform.SetParent(this.transform, true);
        newHolder.GetComponent<RectTransform>().rotation = GetComponent<RectTransform>().rotation;
        cardObject.transform.SetParent(newHolder.transform, true);
        UpdateCardList();
    }
}
