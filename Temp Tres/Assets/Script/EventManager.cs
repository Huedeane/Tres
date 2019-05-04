using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_EventName
{
    Player_Join
};



public class EventManager : MonoBehaviour
{
    #region Variable
    private static EventManager Instance;
    private Dictionary<E_EventName, Action<EventParam>> m_EventDictionary;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        EventDictionary = new Dictionary<E_EventName, Action<EventParam>>();
    }

    public static void StartListening(E_EventName eventName, Action<EventParam> listener)
    {
        if (Instance == null) return;

        Action<EventParam> thisEvent;
        if (Instance.EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            Instance.EventDictionary[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            Instance.m_EventDictionary.Add(eventName, thisEvent);
        }

    }

    public static void StopListening(E_EventName eventName, Action<EventParam> listener)
    {
        if (Instance == null) return;

        Action<EventParam> thisEvent;
        if (Instance.EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            Instance.EventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(E_EventName eventName)
    {
        if (Instance == null) return;

        Action<EventParam> thisEvent;
        if (Instance.EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(new EventParam(eventName));
        }
    }

    public static void TriggerEvent(E_EventName eventName, EventObject eventObject)
    {
        if (Instance == null) return;

        Action<EventParam> thisEvent;
        if (Instance.EventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(new EventParam(eventName, eventObject));
        }
    }

    #region Getter & Setter
    public Dictionary<E_EventName, Action<EventParam>> EventDictionary { get => m_EventDictionary; set => m_EventDictionary = value; }
    #endregion
}

[Serializable]
public class EventObject
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
    public List<string> TypeString { get => m_TypeString; set => m_TypeString = value; }
    public List<bool> TypeBool { get => m_TypeBool; set => m_TypeBool = value; }
    public List<int> TypeInt { get => m_TypeInt; set => m_TypeInt = value; }
    public List<float> TypeFloat { get => m_TypeFloat; set => m_TypeFloat = value; }
    public List<Transform> TypeTransform { get => m_TypeTransform; set => m_TypeTransform = value; }
    public List<GameObject> TypeGameObject { get => m_TypeGameObject; set => m_TypeGameObject = value; }
    #endregion

}

[Serializable]
public class EventParam
{
    #region Variable
    private E_EventName m_EventName;
    private EventObject m_EventObject;
    #endregion

    public EventParam(E_EventName m_EventName)
    {
        this.EventName = m_EventName;
        this.EventObject = new EventObject();
    }

    public EventParam(E_EventName m_EventName, EventObject m_EventObject)
    {
        this.EventName = m_EventName;
        this.EventObject = m_EventObject;
    }

    #region Getter & Setter
    public E_EventName EventName { get => m_EventName; set => m_EventName = value; }
    public EventObject EventObject { get => m_EventObject; set => m_EventObject = value; }
    #endregion
}
