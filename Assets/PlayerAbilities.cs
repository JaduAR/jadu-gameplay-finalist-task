using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region
//Created by Alberto Alvarado Jr
#endregion
public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private Slider slide;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    private Animator anim;
    private bool jumping,readyToJump;
    private Rigidbody rb;
    private float jumpAmount = 2f;
    private float jumpTime, kickTime, buttonTime = 0.3f;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //if jumping then adds force upwards and activates timers for kick and jump time
        if (jumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpAmount, rb.velocity.z);
            jumpTime += Time.deltaTime;
            kickTime += Time.deltaTime;
        }
        //sets the amount of time you're allowed to continue ascending
        if (jumpTime > buttonTime)
            jumping = false;

        //activates kick
        if (slide.value == 1 && kickTime < buttonTime)
            Kick();

    }

    public void Jump()
    {
        if (IsGrounded() && readyToJump)
        {
            readyToJump = false;
            anim.SetTrigger("Jump");
            jumping = true;
            jumpTime = 0;
        }
    }

    public void Descending()
    {
        //player falling
        jumping = false;
        kickTime = 0;
    }
    public void ReadyToJump()
    {
        readyToJump = true;
    }

    public void Kick()
    {
        //certain amount of time the player can kick when in the air.
        if(!IsGrounded())
            anim.SetTrigger("Kick");
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }
 }
    

