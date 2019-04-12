using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum E_ZoneType { Hand, Deck, Pile }
public enum E_FaceType { Front, Back }
public enum E_PostionType {Top, Bottom }
public enum E_CardOption { Play, Draw }

public class CardObject : MonoBehaviour, IPointerExitHandler
{

    #region Variable
    [Header("Card Data")]
    [SerializeField] private int m_CardObjectId;
    [SerializeField] private E_CardColor m_CardColor;
    [SerializeField] private E_CardType m_CardType;
    [SerializeField] private E_ZoneType m_CardLocation;

    [Header("Sprite")]
    [SerializeField] private Sprite m_FrontImage;
    [SerializeField] private Sprite m_BackImage;
    [SerializeField] private Sprite m_OverlayTransparent;
    [SerializeField] private Sprite m_OverlayHighlighted;
    [SerializeField] private Sprite m_OverlayAvailable;

    [Header("Option Button")]
    [SerializeField] private GameObject m_CardOptionButtonPrefab;
    [SerializeField] private HashSet<E_CardOption> m_CardOptionSet;

    [Header("Child Object")]
    private GameObject m_CardOptionGroup;
    private GameObject m_CardOverlay;
    #endregion

    #region Getter & Setter
    public int CardObjectId
    {
        get
        {
            return m_CardObjectId;
        }

        set
        {
            m_CardObjectId = value;
        }
    }
   

    public Sprite FrontImage
    {
        get
        {
            return m_FrontImage;
        }

        set
        {
            m_FrontImage = value;
        }
    }
    public Sprite BackImage
    {
        get
        {
            return m_BackImage;
        }

        set
        {
            m_BackImage = value;
        }
    }
    public Sprite OverlayTransparent
    {
        get
        {
            return m_OverlayTransparent;
        }

        set
        {
            m_OverlayTransparent = value;
        }
    }
    public Sprite OverlayHighlighted
    {
        get
        {
            return m_OverlayHighlighted;
        }

        set
        {
            m_OverlayHighlighted = value;
        }
    }
    public Sprite OverlayAvailable
    {
        get
        {
            return m_OverlayAvailable;
        }

        set
        {
            m_OverlayAvailable = value;
        }
    }
    public GameObject CardOptionButtonPrefab
    {
        get
        {
            return m_CardOptionButtonPrefab;
        }

        set
        {
            m_CardOptionButtonPrefab = value;
        }
    }
    public HashSet<E_CardOption> CardOptionSet
    {
        get
        {
            return m_CardOptionSet;
        }

        set
        {
            m_CardOptionSet = value;
        }
    }
    public GameObject CardOptionGroup
    {
        get
        {
            return m_CardOptionGroup;
        }

        set
        {
            m_CardOptionGroup = value;
        }
    }
    public GameObject CardOverlay
    {
        get
        {
            return m_CardOverlay;
        }

        set
        {
            m_CardOverlay = value;
        }
    }
    #endregion

    public void MoveCard()
    {

    }

    public void PlayCard()
    {


    }

    private void Awake()
    {
        CardOptionSet = new HashSet<E_CardOption>();
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {

            switch (child.name)
            {
                case "Card Option Group":
                    CardOptionGroup = child.gameObject;
                    break;
                case "Card Overlay":
                    CardOverlay = child.gameObject;
                    break;
            }
        }
    }

    private void Update()
    {
        UpdateCoverImage();
    }

    //Display Front/Back depending on rotatio of card
    private void UpdateCoverImage()
    {
        
        if (Mathf.Abs(GetComponent<RectTransform>().rotation.y) <= .70)
            GetComponent<Image>().sprite = FrontImage;
        else
            GetComponent<Image>().sprite = BackImage;
    }

    //Update Option available to card
    private void UpdateCardOption()
    {
        //Clear Card Option
        CardOptionSet.Clear();

        //Add Card Option to HashSet depending on their location
        switch (m_CardLocation)
        {
            case E_ZoneType.Deck:
                CardOptionSet = new HashSet<E_CardOption>()
                {
                    E_CardOption.Play
                };
                break;
            case E_ZoneType.Hand:
                CardOptionSet = new HashSet<E_CardOption>()
                {
                    E_CardOption.Play
                };
                break;
            case E_ZoneType.Pile:
                CardOptionSet = new HashSet<E_CardOption>()
                {
                    E_CardOption.Play
                };
                break;
        }
    }

    public void Move(GameObject gameObject)
    {
        StartCoroutine(MoveCard_IE(gameObject));
    }


    //Move card back to (0,0) and Rotate card depending on move type
    IEnumerator MoveCard_IE(GameObject cardZone)
    {
        cardZone.GetComponent<Hand>().AddCard(this.gameObject);
        RectTransform cardRT = GetComponent<RectTransform>();

        //Variable for position lerp
        Vector2 currentPostion = cardRT.anchoredPosition;
        Vector2 targetPostion = new Vector2(0, 0);

        //Variable for scale lerp
        Vector3 currentScale = cardRT.localScale;
        Vector3 targetScale = cardZone.GetComponent<RectTransform>().localScale;

        //Variable for rotation lerp
        Quaternion currentRotation = cardRT.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);

        Debug.Log(currentRotation);
        Debug.Log(targetRotation);

        //Anchor the image so that it centered when moving back to origin
        cardRT.anchorMax = new Vector2(0.5f, 0.5f);
        cardRT.anchorMin = new Vector2(0.5f, 0.5f);

        //Time
        float time = 0f;
        float timeToFinish = 2f;

        //Move the Card Back to origin over the time it takes to finish
        while (currentPostion != targetPostion)
        {
            time += Time.deltaTime / timeToFinish;

            currentPostion = cardRT.anchoredPosition;
            currentRotation = cardRT.rotation;
            currentScale = cardRT.localScale;

            

            cardRT.anchoredPosition = Vector2.Lerp(currentPostion, targetPostion, time);
            cardRT.Rotate(new Vector3(0, 0, 30));
            cardRT.rotation = Quaternion.Lerp(currentRotation, targetRotation, time);
            cardRT.localScale = Vector3.Lerp(currentScale, targetScale, time);

            yield return null;
        }

    }

    //Called when mouse pointer leaves area
    public void OnPointerExit(PointerEventData eventData)
    {
        //When pointer leave pointer area, then close option
        //CloseOption();
    }

}
