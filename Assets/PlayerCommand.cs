using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerCommand : MonoBehaviour
{
    Player myPlayer;

    Coroutine messageCoroutine;

    void Start()
    {
        myPlayer = GetComponent<Player>();
    }

    public void processCmd(string cmd)
    {
        Player otherPlayer = myPlayer.getNextPlayer();

        string[] parts = cmd.Split(':');
        string cmdName = parts[0];

        Debug.Log(cmdName);

        switch (cmdName)
        {
            case "Draw Card":
                Deck deck = GameObject.FindGameObjectWithTag("Deck").GetComponent<Deck>();
                Card card = deck.GetTopCard();
                card.CardLocation = E_ZoneType.Hand;
                card.MoveCard(GetComponentInChildren<Hand>().gameObject);

                if (myPlayer.isLocalPlayer)
                {
                    card.Reveal();
                    
                }
                else
                {
                    card.Hide();
                }
                break;
            case "Set Seed":
                int seed = int.Parse(parts[1]);
                Random.InitState(seed);
                Debug.Log("Set Seed " + seed);
                break;
            case "Shuffle Deck":
                Debug.Log("Shuffle Deck");
                
                break;
        }



    }

    public void playCmd()
    {
        if (myPlayer.myTurn == true)
        {
            
        }
        else
        {
            
        }
    }

    public void extraCmd()
    {
        if (myPlayer.myTurn == true)
        {

        }
        else
        {

        }
    }

    public void drawCmd()
    {
        if (myPlayer.myTurn == true)
        {
            myPlayer.CmdServerCommandTurn("Draw Card");
        }
        else
        {
            myPlayer.sendMessage("NOT YOUR TURN!");
        }
    }

    public void quitCmd()
    {
        if (myPlayer.myTurn == true)
        {
            
        }
        else
        {

        }
    }

    public void clickCommand()
    {
        if (myPlayer.myTurn == true)
        {
            
        }
        else
        {
            myPlayer.sendMessage("NOT YOUR TURN!");
        }
    }

}

public enum E_CommandType { Draw, Deal }

public class Command : MonoBehaviour
{
    #region Variable
    private E_CommandType m_CmdType;
    private Param m_CmdParam;
    #endregion

    public Command(E_CommandType m_CmdType)
    {
        this.m_CmdType = m_CmdType;
        this.m_CmdParam = new Param();
    }

    public Command(E_CommandType m_CmdType, Param m_CmdParam)
    {
        this.m_CmdType = m_CmdType;
        this.m_CmdParam = m_CmdParam;
    }

    #region Getter & Setter
    public E_CommandType CmdType
    {
        get
        {
            return m_CmdType;
        }

        set
        {
            m_CmdType = value;
        }
    }
    public Param CmdParam
    {
        get
        {
            return m_CmdParam;
        }

        set
        {
            m_CmdParam = value;
        }
    }
    #endregion
    
}

public class Param
{
    #region Variable
    [SerializeField] private List<string> m_TypeString;
    [SerializeField] private List<bool> m_TypeBool;
    [SerializeField] private List<int> m_TypeInt;
    [SerializeField] private List<float> m_TypeFloat;
    [SerializeField] private List<Transform> m_TypeTransform;
    [SerializeField] private List<GameObject> m_TypeGameObject;
    #endregion

    #region Getter & Setter
    public List<string> TypeString
    {
        get
        {
            return m_TypeString;
        }

        set
        {
            m_TypeString = value;
        }
    }
    public List<bool> TypeBool
    {
        get
        {
            return m_TypeBool;
        }

        set
        {
            m_TypeBool = value;
        }
    }
    public List<int> TypeInt
    {
        get
        {
            return m_TypeInt;
        }

        set
        {
            m_TypeInt = value;
        }
    }
    public List<float> TypeFloat
    {
        get
        {
            return m_TypeFloat;
        }

        set
        {
            m_TypeFloat = value;
        }
    }
    public List<Transform> TypeTransform
    {
        get
        {
            return m_TypeTransform;
        }

        set
        {
            m_TypeTransform = value;
        }
    }
    public List<GameObject> TypeGameObject
    {
        get
        {
            return m_TypeGameObject;
        }

        set
        {
            m_TypeGameObject = value;
        }
    }
    #endregion 

}
