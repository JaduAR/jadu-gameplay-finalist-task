using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{
    public float walkSpeed;

    [Header("Jumping Properties")]
    public float jumpForce;
    public bool IsJumping;
    public bool IsGrounded;
    public bool jumpButtonDown;
    public bool kickButtonDown;

    private float jumpTimer = 0.0f;
    private float kickTimer = 0.0f;

    public Rigidbody rigidbody;
    public Animator animator;
    public LayerMask groundLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        HandleMovement();
        HandleTimers();
        HandleAnimation();
    }

    private void HandleMovement(){
        IsGrounded = IsPlayerGrounded();
        animator.SetBool("isGrounded", IsGrounded);
        if(jumpButtonDown && IsGrounded && !IsJumping){
            Jump();
        }
        if(!jumpButtonDown && IsJumping){
            IsJumping = false;
            if(rigidbody.velocity.y > 0) rigidbody.velocity = Vector3.zero;
        }
        if(kickButtonDown && !IsGrounded && kickTimer <= 0.0f){
            Kick();
        }
    }

    private void HandleTimers(){
        if(jumpTimer > 0.0f){
            jumpTimer -= Time.deltaTime;
        }
        if(kickTimer > 0.0f){
            kickTimer -= Time.deltaTime;
        }
    }

    private void HandleAnimation(){
        animator.SetFloat("yVelocity", rigidbody.velocity.y);
        animator.SetBool("IsJumping", jumpButtonDown);
    }

    public void Jump(){
        if(IsJumping) return;
        animator.Play("Jump_Asc");
        rigidbody.AddForce(jumpForce * Vector3.up);
        IsJumping = true;
        jumpTimer = 0.7f;
        kickTimer = 0.0f;
    }

    private void Kick(){
        animator.Play("JumpKick");
        kickTimer = 0.8f;
    }

    private bool IsPlayerGrounded(){
        RaycastHit hit;
        bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.01f, groundLayerMask);
        if(IsJumping && jumpTimer <= 0 && grounded){
            IsJumping = false;
        }
        return grounded;
    }

    public void OnJumpButton(bool state){
        jumpButtonDown = state;
    }

    public void OnKickButton(bool state){
        kickButtonDown = state;
    }
}
