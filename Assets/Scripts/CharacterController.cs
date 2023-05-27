using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float maxJumpHeight;
    [SerializeField]
    private float jumpSpeed;

    private bool isJumping = false;
    private bool canJump = true;
    private bool canKick = false;

    private float jumpAscendTime;
    private float jumpDescendTime;

    public void BeginJump()
    {
        if (!canJump) return;

        Debug.Log("Begin Jump");
        isJumping = true;
        canKick = true;
        canJump = false;

        UpdateJumpAnimation();

        StartCoroutine(Co_AnimateTransformJump());
    }

    public void EndJump()
    {
        Debug.Log("End Jump");

        ResetJumpKick();

        UpdateJumpAnimation();
    }

    private void UpdateJumpAnimation()
    {
        animator.SetBool("IsJumping", isJumping);
    }

    public void ResetJumpKick()
    {
        isJumping = false;
        canJump = true;
        canKick = false;

        animator.SetFloat("JumpValue", 0);
    }

    public void AttemptKick()
    {
        if (/*!isJumping || */!canKick) return;

        Debug.Log("Kick");

        animator.SetTrigger("Kick");

        canKick = false;
        isJumping = false;

        UpdateJumpAnimation();
    }

    private IEnumerator Co_AnimateTransformJump()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        endPos.y = maxJumpHeight;

        float currentTime = 0;
        float per;

        jumpAscendTime = maxJumpHeight / jumpSpeed;

        Debug.Log($"Ascend Time: {jumpAscendTime}");

        while (isJumping && currentTime < jumpAscendTime)
        {
            per = currentTime / jumpAscendTime;
            transform.position = Vector3.Lerp(startPos, endPos, Easings.EaseOutCirc(per));

            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;

            animator.SetFloat("JumpValue", per);
        }

        if(isJumping)
        {
            transform.position = endPos;
            //animator.SetFloat("JumpValue", 1);
        }

        //while(isJumping)
        //{
        //    yield return null;
        //}

        canKick = false;
        isJumping = false;

        UpdateJumpAnimation();

        currentTime = 0;

        float fallHeight = Mathf.Abs(startPos.y - transform.position.y);

        jumpDescendTime = Mathf.Sqrt((fallHeight * 2) / Mathf.Abs(Physics.gravity.y));

        endPos = transform.position;

        while (currentTime < jumpDescendTime)
        {
            per = currentTime / jumpDescendTime;

            transform.position = Vector3.Lerp(endPos, startPos, Easings.EaseInCirc(per));

            yield return null;

            currentTime += Time.deltaTime;

            animator.SetFloat("JumpValue", Mathf.Clamp01(1 - per));
        }

        transform.position = startPos;

        animator.SetTrigger("Recover");

        EndJump();
    }
}
