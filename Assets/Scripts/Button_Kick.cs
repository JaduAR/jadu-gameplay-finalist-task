using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Only while Jump is being pressed, the jump kick can be activated - we can achieve this by checking if the jump is pressed and then hovering over the kick button
/// </summary>
public class Button_Kick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private PlayerController _playerController;

    public bool isJumping = false;

    //Set reference definitions on Reset or when this script is assigned.
    private void Reset()
    {
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        if (_playerController == null)
        {
            _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }


    //Detect if the Cursor/touch starts to pass over the GameObject
    /// <summary>
    /// IF THE JUMP BUTTON IS BEING PRESSED DOWN AND WE HOVER OVER KICK, TELL PLAYER TO JUMP KICK
    /// </summary>
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(isJumping)
        {
            _playerController.JumpKick();
            Debug.Log("JUMP KICK");
        }
    }
    //Detect when the cursor/touch leaves this button
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isJumping = false;
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
    }

    //Detect if held down
    public void OnPointerDown(PointerEventData pointerEventData)
    {
    }

    //Detect if released
    public void OnPointerUp(PointerEventData pointerEventData)
    {
    }
}