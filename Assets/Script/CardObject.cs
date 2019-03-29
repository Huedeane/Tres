using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum E_ZoneType { Hand, Deck, Pile }
public enum E_FaceType { Front, Back }
public enum E_CardOption { Play, Draw }

public class CardObject : MonoBehaviour {

    #region Variable
    [Header("Card Data")]
    [SerializeField] private int m_CardObjectId;
    [SerializeField] private Card m_CardData;
    [SerializeField] private E_ZoneType m_CardZone;


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

    #endregion
    public void MoveCard()
    {

    }

    public void PlayCard()
    {


    }

    private void Awake()
    {
        m_CardOptionSet = new HashSet<E_CardOption>();
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {

            switch (child.name)
            {
                case "Card Option Group":
                    m_CardOptionGroup = child.gameObject;
                    break;
                case "Card Overlay":
                    m_CardOverlay = child.gameObject;
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
            GetComponent<Image>().sprite = m_FrontImage;
        else
            GetComponent<Image>().sprite = m_BackImage;
    }

    //Update Option available to card
    private void UpdateCardOption()
    {
        //Clear Card Option
        m_CardOptionSet.Clear();

        //Add Card Option to HashSet depending on their location
        switch (m_CardZone)
        {
            case E_ZoneType.Deck:
                m_CardOptionSet = new HashSet<E_CardOption>()
                {
                    E_CardOption.Play
                };
                break;
            case E_ZoneType.Hand:
                m_CardOptionSet = new HashSet<E_CardOption>()
                {
                    E_CardOption.Play
                };
                break;
            case E_ZoneType.Pile:
                m_CardOptionSet = new HashSet<E_CardOption>()
                {
                    E_CardOption.Play
                };
                break;
        }
    }

    //Display Card Options
    private void ShowOption()
    {
        //Clear and update with option the card can do
        UpdateCardOption();

        //Prevent the Player from clicking the card again
        GetComponent<Button>().enabled = false;

        //Adjust the RectTransform height of the card option group
        float optionCount = m_CardOptionSet.Count;
        float optionHeight = m_CardOptionButtonPrefab.GetComponent<RectTransform>().rect.height;
        float optionSpacing = m_CardOptionGroup.GetComponent<VerticalLayoutGroup>().spacing;
        m_CardOptionGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, optionCount * (optionHeight + optionSpacing));

        //Add Trigger Event for each option
        foreach (E_CardOption cardOption in m_CardOptionSet)
        {
            //Create a button from prefab
            GameObject newButtonObject = Instantiate(m_CardOptionButtonPrefab, transform.position, transform.rotation) as GameObject;
            //Set the button parent
            newButtonObject.transform.SetParent(m_CardOptionGroup.transform, false);

            //Set name of Button and text of button
            string cardOptionString = cardOption.ToString().Replace("_", " ");
            newButtonObject.GetComponentInChildren<TextMeshProUGUI>().SetText(cardOptionString);
            newButtonObject.name = cardOptionString;

            //Add Method to Button OnClick
            Button button = newButtonObject.GetComponent<Button>();
            button.onClick.AddListener(() => CloseOption());


            switch (cardOption)
            {
                case E_CardOption.Play:
                    button.onClick.AddListener(()=> Move(cardOption));
                    break; 
            }
        }

    }
    private void CloseOption()
    {
        
        //Set the button to be able to click on again
        GetComponent<Button>().enabled = true;

        //Clear all the option available to the card
        m_CardOptionSet.Clear();
        if (m_CardOptionGroup.transform.childCount != 0)
        {
            foreach (Transform child in m_CardOptionGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }

        //Set Height of CardOptionGroup back to 0
        m_CardOptionGroup.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
    }
    

    IEnumerator MoveCard_IE()
    {
        RectTransform cardRT = GetComponent<RectTransform>();

        //Variable for Lerp
        Vector2 currentPostion = cardRT.anchoredPosition;
        Vector2 targetPostion = new Vector2(0, 0);
        float t = 0;

        //Anchor the image so that it centered when moving back to origin
        cardRT.anchorMax = new Vector2(0.5f, 0.5f);
        cardRT.anchorMin = new Vector2(0.5f, 0.5f);

        //Move the Card Back to origin over a period of 1s
        while (currentPostion != targetPostion)
        {
            t += Time.deltaTime / 1f;
            currentPostion = cardRT.anchoredPosition;
            cardRT.anchoredPosition = Vector2.Lerp(currentPostion, targetPostion, t);
            yield return null;
        }
    }

}
