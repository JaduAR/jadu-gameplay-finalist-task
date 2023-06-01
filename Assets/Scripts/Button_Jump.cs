using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Hold down the Jump button to switch to the jumping animation.
/// The longer the button is pressed, the high the avatar jumps within some max limit.
/// </summary>
public class Button_Jump : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private Button_Kick _buttonKick;
    [SerializeField]
    private Image _jumpKickSliderIMAGE;

    //Set reference definitions on Reset or when this script is assigned.
    private void Reset()
    {
        _buttonKick = GameObject.Find("Btn_Kick").GetComponent<Button_Kick>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _jumpKickSliderIMAGE = GameObject.Find("JumpKick_Slider").GetComponent<Image>();
    }

    
    private void Start()
    {
        if (_playerController == null)
        {
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
        if(_buttonKick == null)
        {
            _buttonKick = GameObject.Find("Btn_Kick").GetComponent<Button_Kick>();
        }
        if(_jumpKickSliderIMAGE == null)
        {
            _jumpKickSliderIMAGE = GameObject.Find("JumpKick_Slider").GetComponent<Image>();
        }
    }


    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
    }

    //Detect if held down
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _playerController.JumpPressed();
        _buttonKick.isJumping = true;
        _jumpKickSliderIMAGE.enabled = true;
    }

    //Detect if released
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _playerController.JumpReleased();
        _buttonKick.isJumping = false;
        _jumpKickSliderIMAGE.enabled = false;
    }
}
