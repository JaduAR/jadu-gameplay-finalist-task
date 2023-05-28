using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

/// <summary>
/// A custom on-screen button that handles "sliding" between buttons
/// </summary>
public class CustomScreenButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    [SerializeField]
    public bool EnableSlideInput = false;

    static bool s_wasPointerDown = false;
    static CustomScreenButton s_lastButton = null;

    public void OnPointerUp(PointerEventData eventData)
    {
        s_lastButton?.SendValueToControl(0.0f);
        s_wasPointerDown = false;
        s_lastButton = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        s_wasPointerDown = true;
        s_lastButton = this;
        SendValueToControl(1.0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (EnableSlideInput && s_wasPointerDown && s_lastButton != this)
        {
            SendValueToControl(1.0f);
            s_lastButton.SendValueToControl(0.0f);
            s_lastButton = this;
        }
    }

    [InputControl(layout = "Button")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }
}