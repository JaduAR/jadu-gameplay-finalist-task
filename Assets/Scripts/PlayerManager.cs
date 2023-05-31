using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public CanvasGroup JumpKickBG;
    public CanvasGroup JumpBtn;

    public Animator PlayerAnimator;
    public string[] AnimTriggers;
    public float jumpforce = 10f;
    public float JumpLimit;
    public Rigidbody rb;

    public float speed = 10.0f;

    [HideInInspector]
    public Vector2 target;

    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void JumpUp()
    {
        ResetTriggers();
        PlayerAnimator.SetTrigger("Jump");
        rb.AddForce(Vector3.up * PlayerManager.Instance.jumpforce, ForceMode.Impulse);
        StartCoroutine(AddForce());
    }
    public void JumpDown()
    {
        ResetTriggers();
        PlayerAnimator.SetTrigger("JumpDown");
        //rb.constraints = RigidbodyConstraints.None;
        StopAllCoroutines();
    }
    public void PerformJumpKick()
    {
        ResetTriggers();
        PlayerAnimator.SetTrigger("JumpKick");
    }
    public void ReturnToPos(Transform Btn)
    {
        float step = speed * Time.deltaTime;
        Btn.position = Vector2.MoveTowards(Btn.position, target, step);
    }
    public void EnableJumpKickBG(int JumpKickBG_Alpha, int JumpBtn_Alpha)
    {
        JumpKickBG.alpha = JumpKickBG_Alpha;
        JumpBtn.alpha = JumpBtn_Alpha;
    }
    private void ResetTriggers()
    {
        for (int i = 0; i < AnimTriggers.Length; i++)
        {
            PlayerAnimator.ResetTrigger(AnimTriggers[i]);
        }
    }
    IEnumerator AddForce()
    {
        yield return new WaitForSeconds(0.1f);
        if (gameObject.transform.position.y<=JumpLimit)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            StartCoroutine(AddForce());
        }
        else
        {
            //rb.useGravity = false;
            //rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        
    }
}
