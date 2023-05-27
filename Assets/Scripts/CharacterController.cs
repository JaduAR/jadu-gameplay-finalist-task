using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float jumpHeight;

    private bool isJumping = false;
    private bool hasKicked = false;

    public void Jump()
    {
        animator.SetTrigger("Jump");
    }

    public void BeginJump()
    {
        Debug.Log("Begin Jump");
        isJumping = true;

        UpdateJumpAnimation();
    }

    public void EndJump()
    {
        Debug.Log("End Jump");
        isJumping = false;

        UpdateJumpAnimation();
    }

    private void UpdateJumpAnimation()
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void ResetKick()
    {
        hasKicked = false;
    }

    public void AttemptKick()
    {
        if (!isJumping || hasKicked) return;

        Debug.Log("Kick");

        animator.SetTrigger("Kick");

        hasKicked = true;
        isJumping = false;

        UpdateJumpAnimation();
    }
}
