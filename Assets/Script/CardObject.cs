using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_FaceType { Front, Back }

public class CardObject : MonoBehaviour {

    #region Variable
    [Header("Card Data")]
    [SerializeField] private int m_CardObjectID;
    [SerializeField] private Card m_CardData;

    [Header("Sprite")]
    [SerializeField] private Sprite m_FrontImage;
    [SerializeField] private Sprite m_BackImage;
    [SerializeField] private Sprite m_OverlayTransparent;
    [SerializeField] private Sprite m_OverlayHighlighted;
    [SerializeField] private Sprite m_OverlayAvailable;

    [Header("Card UI Variable")]
    [SerializeField] private RectTransform m_CardRT;
    #endregion

    #region Getter & Setter
    public int CardObjectID
    {
        get
        {
            return m_CardObjectID;
        }

        set
        {
            m_CardObjectID = value;
        }
    }
    public Card CardData
    {
        get
        {
            return m_CardData;
        }

        set
        {
            m_CardData = value;
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
    #endregion

    public void SetFacing()
    {


    }

    public void SetRotation()
    {


    }

    public void FlipImage()
    {


    }

    public void MoveCard_IE()
    {


    }

    IEnumerator MoveCard()
    {
        //Variable for Lerp
        Vector2 currentPostion = m_CardRT.anchoredPosition;
        Vector2 targetPostion = new Vector2(0, 0);
        float t = 0;

        //Anchor the image so that it centered when moving back to origin
        m_CardRT.anchorMax = new Vector2(0.5f, 0.5f);
        m_CardRT.anchorMin = new Vector2(0.5f, 0.5f);

        //Move the Card Back to origin over a period of 1s
        while (currentPostion != targetPostion)
        {
            t += Time.deltaTime / 1f;
            currentPostion = m_CardRT.anchoredPosition;
            m_CardRT.anchoredPosition = Vector2.Lerp(currentPostion, targetPostion, t);
            yield return null;
        }
    }

}
