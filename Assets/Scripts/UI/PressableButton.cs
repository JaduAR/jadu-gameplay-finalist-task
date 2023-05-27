using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressableButton : Selectable, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public UnityEvent onHover;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        onPress?.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        onRelease?.Invoke();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        onHover?.Invoke();
    }
}
