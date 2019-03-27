using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_CardColor { Red, Blue, Yellow, Green, Black, None }

public class Card : ScriptableObject
{
    #region Variable
    [SerializeField] private int m_CardID;
    [SerializeField] private E_CardColor m_CardColor;
    [SerializeField] private Sprite m_CardImage;
    #endregion

    #region Getter & Setter
    #endregion
}

public class NormalCard : Card
{
    [SerializeField] private int m_CardNumber;

    public NormalCard()
    {

    }

}

public enum E_CardEffect { Reverse, Draw, Skip }
public class SpecialCard : Card
{
    [SerializeField] private E_CardEffect m_CardEffect;

    public SpecialCard()
    {

    }

}
