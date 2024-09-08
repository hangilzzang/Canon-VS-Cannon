using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event Action MouseDownEvent;
    public event Action StateChangeEvent;
    public event Action GameOverEvent;
    public event Action DestoryAllCannonBallEvent;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerStateChanged()
    {
        StateChangeEvent?.Invoke();
    }
    public void TriggerGameOverEvent()
    {
        GameOverEvent?.Invoke();
    }
    public void TriggerDestoryAllCannonBallEvent()
    {
        DestoryAllCannonBallEvent?.Invoke();
    }
    public void TriggerMouseDownEvent()
    {
        MouseDownEvent?.Invoke();
    }
}
