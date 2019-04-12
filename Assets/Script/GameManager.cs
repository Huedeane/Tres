using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Variable
    private int m_GameDuration;
    private List<Player> m_PlayerList;
    private List<GameObject> m_HandZone;
    #endregion

    #region Getter & Setter
    public int GameDuration
    {
        get
        {
            return m_GameDuration;
        }

        set
        {
            m_GameDuration = value;
        }
    }
    public List<Player> PlayerList
    {
        get
        {
            return m_PlayerList;
        }

        set
        {
            m_PlayerList = value;
        }
    }
    #endregion

    #region Private Method
    #endregion

    #region Public Method
    public void StartGame()
    {

    }
    public void EndGame()
    {

    }
    public void SetPlayerSequence()
    {

    }
    #endregion
}
