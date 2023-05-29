using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxJumpTime = 1f;
    [SerializeField] float jumpForce = 3f;

    bool canJump = true;
    bool jumping = false;
    bool onGround = true;
    float jumpTimer = 0;

    Animator animator;
    Rigidbody rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * rb.mass); //increased gravity for smoother jumping
        ProcessJump();
        animator.SetBool("Idle", onGround);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpTimer = 0;
            canJump = true;
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            onGround = false;
        }
    }

    private void ProcessJump()
    {
        if (jumping && canJump)
        {
            jumpTimer += Time.deltaTime;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            if (jumpTimer >= maxJumpTime) canJump = false;
        }
    }

    //jumps OnHold()
    public void Jump()
    {
        if (canJump)
        {
            jumping = true;
        }
        else jumping = false;
    }

    //stops jumping OnRelease()
    public void ReleaseJump()
    {
        jumping = false;
    }

    public void Kick()
    {
        if(jumping) animator.SetTrigger("Kick");
    }
}
