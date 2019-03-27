using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_CardColor { Red, Blue, Yellow, Green, Black, None }

public class Card : ScriptableObject {
    #region Variable
    [SerializeField] private int m_CardID;
    [SerializeField] private E_CardColor m_CardColor;

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
    public int CardID
    {
        get
        {
            return m_CardID;
        }

        set
        {
            m_CardID = value;
        }
    }

    #endregion
}
