using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_ZoneType { Hand, Table }
public enum E_FaceType { Front, Back }
public enum E_CardOption { Play }

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
    [SerializeField] private GameObject m_CardOptionGroup;
    [SerializeField] private GameObject m_CardOverlay;
    #endregion

    #region Getter & Setter

    #endregion
    public void MoveCard()
    {

    }

    public void PlayCard()
    {


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
