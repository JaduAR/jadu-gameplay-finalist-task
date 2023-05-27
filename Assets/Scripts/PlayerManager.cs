using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

namespace TJ
{
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    public EventTrigger jumpButton;
    PlayerInput inputActions;
    bool pointerDown, inAir, canKick, jumpHasBegun, falling;
    float jumpStartTime, maxJumpDuration = 2f;

    [Header("UI")]
    [SerializeField] private TMP_Text maxJumpDurationText;
    [SerializeField] private Image kickButton;
    
    public void OnEnable(){
        if (inputActions == null){
            inputActions = new PlayerInput();
            inputActions.PlayerActions.Primary.performed += i => pointerDown = true;
            inputActions.PlayerActions.Primary.canceled += i => pointerDown = false;
        }
        inputActions.Enable();
    }
    private void Awake()
    {
        // Jump Event
        EventTrigger.Entry jumpEntry = new EventTrigger.Entry();
        jumpEntry.eventID = EventTriggerType.PointerDown;
        jumpEntry.callback.AddListener( (eventData) => { Jump(); } );
        jumpButton.triggers.Add(jumpEntry);

        //End Jump Event
        // EventTrigger.Entry jump = new EventTrigger.Entry();
        // entry.eventID = EventTriggerType.PointerDown;
        // entry.callback.AddListener( (eventData) => { Jump(); } );
        // jumpButton.triggers.Add(entry);

    }

    private void Update()
    {
        inAir = playerAnimator.GetBool("inAir");
        canKick = playerAnimator.GetBool("canKick");
        falling = playerAnimator.GetBool("falling");

        HandleInAirActions();
    }
    private void HandleInAirActions()
    {
        if(!inAir || falling) return;

        if(!pointerDown)
        {
            EndJump();
            return;
        }

        if(!jumpHasBegun)
        {
            jumpHasBegun = true;
            jumpStartTime = Time.time;
        }

        if(jumpStartTime + maxJumpDuration < Time.time)
        {
            Debug.Log($"Jump duration exceeded {maxJumpDuration} seconds");
            
            //reset jump
            jumpHasBegun = false;
            playerAnimator.SetBool("falling", true);
            StartCoroutine(FlashText());
        }
    }

    public void Jump()
    {
        if(!inAir)
        {
            Debug.Log($"Jump");
            playerAnimator.SetBool("inAir", true);
            playerAnimator.CrossFade("Jump",0.2f);
        }
    }
    public void EndJump()
    {
        // jumpHasBegun = false;
        Debug.Log($"End Jump");
        jumpHasBegun = false;
        playerAnimator.SetBool("falling", true);
    }
    public void Kick()
    {
        if(inAir && canKick && pointerDown)
        {
            Debug.Log("Kick");
            jumpHasBegun = false;
            StartCoroutine(FlashButton());
            playerAnimator.CrossFade("Kick",0.2f);
        }
    }
    public void OnDisable(){
        inputActions.Disable();
    }
    private IEnumerator FlashText()
    {
        maxJumpDurationText.enabled = true;
        yield return new WaitForSeconds(1f);
        maxJumpDurationText.enabled = false;
    }
    private IEnumerator FlashButton()
    {
        kickButton.enabled = true;
        yield return new WaitForSeconds(0.25f);
        kickButton.enabled = false;
    }
}
}
