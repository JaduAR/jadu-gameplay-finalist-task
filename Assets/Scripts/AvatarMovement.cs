using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class AvatarMovement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    float maxJumpTime = 1.0F;
    [SerializeField]
    float jumpSpeed = 3.0F;
    [SerializeField, Tooltip("At what height should the animator consider the avatar fully airborn.")]
    float jumpTransitionHeight = 0.4F;

    Animator playerAnimator;
    CharacterController controller;
    Vector3 velocity;
    bool grounded;
    bool isJumping;
    IEnumerator jump;

    const float gravity = -9.81F;
    const string kickTrigger = "Kick";
    const string yParameter = "Y";

    #endregion

    #region Unity Lifecycle

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (!isJumping)
            Descend();

        playerAnimator.SetFloat(yParameter, Mathf.Lerp(0.0F, 1.0F, transform.position.y));
    }

    #endregion

    #region Public Methods

    public void Jump()
    {
        jump = JumpCoroutine();
        StartCoroutine(jump);
    }

    public void StopJumping()
    {
        isJumping = false;

        if (jump != null) StopCoroutine(jump);
        jump = null;
    }

    public void Kick()
    {
        if (isJumping)
            playerAnimator.SetTrigger(kickTrigger);
    }

    #endregion

    #region Private Methods

    IEnumerator JumpCoroutine()
    {
        isJumping = true;

        float start = Time.time;
        float end = start + maxJumpTime;
        while (Time.time < end && isJumping)
        {
            controller.Move(jumpSpeed * Time.deltaTime * Vector3.up);

            yield return null;
        }

        StopJumping();
    }

    void Descend()
    {
        grounded = controller.isGrounded;
        if (grounded && velocity.y < 0.0F)
            velocity.y = 0.0F;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    #endregion
}
