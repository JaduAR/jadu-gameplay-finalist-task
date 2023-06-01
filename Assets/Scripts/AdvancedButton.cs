using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class AdvancedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public UnityEvent onDownEvent;
    public UnityEvent onUpEvent;
    public UnityEvent onSlideEvent;
    public string onSlideRequiredParam; // If set, will make onSlide event conditional
    public Animator anim; // Animator to check the slide param requirement

    public void OnPointerDown(PointerEventData eventData)
    {
        onDownEvent.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onUpEvent.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onSlideRequiredParam != "")
        {
            if (anim.GetBool(onSlideRequiredParam))
            {
                onSlideEvent.Invoke();
            }
            // else ignore the swipe
        }
        else
        {
            onSlideEvent.Invoke();
        }
    }

}
