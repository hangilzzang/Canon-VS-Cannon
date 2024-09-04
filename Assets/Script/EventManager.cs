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

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 클릭입력 받으면
        {
            MouseDownEvent?.Invoke();
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
}
