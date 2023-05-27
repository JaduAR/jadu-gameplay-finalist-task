using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum JumpState
    {
        Idle,
        Ascending,
        Descending
    }

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float maxJumpHeight;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float jumpCooldown = 1;
    [SerializeField]
    private bool allowKickOnDescend;

    private JumpState jumpstate = JumpState.Idle;

    private bool canKick = false;

    private Coroutine jumpRoutine = null;

    public void ButtonEvent_BeginJump()
    {
        BeginJump();
    }

    private void BeginJump()
    {
        if (jumpstate != JumpState.Idle || jumpRoutine != null) return;

        jumpstate = JumpState.Ascending;

        canKick = true;

        UpdateJumpAnimation();

        jumpRoutine = StartCoroutine(Co_AnimateTransformForJump());
    }

    private void UpdateJumpAnimation()
    {
        animator.SetBool("IsAscending", jumpstate == JumpState.Ascending);
    }

    private IEnumerator Co_AnimateTransformForJump()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos;
        endPos.y = maxJumpHeight;

        float currentTime = 0;
        float per;

        float jumpAscendTime = maxJumpHeight / jumpSpeed;

        while (jumpstate == JumpState.Ascending && currentTime < jumpAscendTime)
        {
            per = currentTime / jumpAscendTime;
            transform.position = Vector3.Lerp(startPos, endPos, Easings.EaseOutCirc(per));

            yield return new WaitForEndOfFrame();

            currentTime += Time.deltaTime;

            animator.SetFloat("JumpValue", per);
        }

        if (jumpstate == JumpState.Ascending)
        {
            transform.position = endPos;
        }

        canKick = allowKickOnDescend;

        jumpstate = JumpState.Descending;

        UpdateJumpAnimation();

        currentTime = 0;

        float fallHeight = Mathf.Abs(startPos.y - transform.position.y);

        float jumpDescendTime = Mathf.Sqrt((fallHeight * 2) / Mathf.Abs(Physics.gravity.y));

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

        jumpRoutine = null;

        Invoke("JumpEnded", jumpCooldown);
    }

    public void ButtonEvent_EndJump()
    {
        EndJump();
    }

    private void EndJump()
    {
        //Debug.Log("End Jump");

        if(jumpstate == JumpState.Ascending )
        {
            jumpstate = JumpState.Descending;
        }

        UpdateJumpAnimation();
    }

    public void AttemptKick()
    {
        if (!canKick) return;

        //Debug.Log("Kick");

        animator.SetTrigger("Kick");

        canKick = false;

        jumpstate = JumpState.Descending;

        UpdateJumpAnimation();
    }

    private void JumpEnded()
    {
        ResetJumpKick();
    }

    private void ResetJumpKick()
    {
        canKick = false;
        jumpstate = JumpState.Idle;

        animator.SetFloat("JumpValue", 0);
    }

}
