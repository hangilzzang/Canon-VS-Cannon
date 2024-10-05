using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDownScript : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        EventManager.instance.TriggerMouseDownEvent(mousePosition);
    }
}
