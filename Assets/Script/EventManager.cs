using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event Action<Vector2> MouseDownEvent;
    public event Action StateChangeEvent;
    public event Action GameOverEvent;
    public event Action DestoryAllCannonBallEvent;
    public event Action watchedRewardAdEvent;


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
    public void TriggerMouseDownEvent(Vector2 mousePosition)
    {
        MouseDownEvent?.Invoke(mousePosition);
    }
    public void TriggerWatchedRewardAdEvent()
    {
        watchedRewardAdEvent?.Invoke();
    }
}
