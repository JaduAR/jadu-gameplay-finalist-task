using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler,IPointerEnterHandler
{
    private bool held;
    public UnityEvent onPointerEnterEvent;
    public UnityEvent onPointerExitEvent;
    public UnityEvent onPointerDownEvent;
    public UnityEvent onPointerUpEvent;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        held = true;
        onPointerDownEvent.Invoke();
        Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        held = false;
        onPointerUpEvent.Invoke();
        Debug.Log("Up");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnterEvent.Invoke();
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerUp(eventData);
        Debug.Log("Exit");
    }
}