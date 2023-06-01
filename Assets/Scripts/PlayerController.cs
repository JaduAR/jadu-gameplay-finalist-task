using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField]
    private AnimationManager _animMgr;
    // 0 - Idle / JumpRecover(Land), 1 - JumpAscend / JumpIdle, 2 - JumpDescend, 3 - JumpKick

    [Header("Character Movement/Physics")]
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float maxJumpTime = 0.3f;
    [SerializeField]
    private float jumpAmount = 2;
    private float jumpTime;
    private bool isJumping;

    [Header("Ground Detection")]
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float groundDetectionLength;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private GameObject _groundDetector;
    private RaycastHit hit;

    private void Start()
    {
        _animMgr = GetComponent<AnimationManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DetectGround();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    /// <summary>
    /// Hold down the Jump button to switch to the jumping animation.
    /// The longer the button is pressed, the high the avatar jumps within some max limit.
    /// </summary>
    public void Jump()
    {
        // Apply the jump force while the jump button is held down
        if ((isJumping) && (jumpTime < maxJumpTime))
        {
            rb.AddForce(rb.velocity.x, jumpAmount, rb.velocity.z, ForceMode.Force);
            jumpTime += Time.deltaTime;

            //Jump up/idle animation
            if (_animMgr.currentAnimationClip != 1)
            {
                _animMgr.PlayAnimationInteger(1);
            }
        }
        //if jumping and timer reaches end...
        else if (jumpTime >= maxJumpTime)
        {
            isJumping = false;

            //If jumping animation, switch to descending animation
            if (_animMgr.currentAnimationClip == 1)
            {
                _animMgr.PlayAnimationInteger(2);
            }
        }
    }

    public void JumpPressed()
    {
        // Check if the jump button is pressed
        if ((!isJumping) && (isGrounded))
        {
            isJumping = true;
            jumpTime = 0;
        }
    }
    public void JumpReleased()
    {
        // Check if the jump button is released
        if (isJumping)
        {
            isJumping = false;
        }
    }


    //Use a raycast to detect if this character is touching the ground
    public void DetectGround()
    {
        if (Physics.Raycast(_groundDetector.transform.position, transform.TransformDirection(Vector3.down), out hit, groundDetectionLength, _groundLayer))
        {
            Debug.DrawRay(_groundDetector.transform.position, transform.TransformDirection(Vector3.down) * groundDetectionLength, Color.yellow);
            isGrounded = true;

            //if grounded and not jumping, then Idle
            if(!isJumping)
            {
                if (_animMgr.currentAnimationClip != 0)
                {
                    _animMgr.PlayAnimationInteger(0);
                }
            }
        }
        else
        {
            Debug.DrawRay(_groundDetector.transform.position, transform.TransformDirection(Vector3.down) * groundDetectionLength, Color.red);
            isGrounded = false;
        }
    }


    /// <summary>
    /// Kick is active, but it will not do anything if tapped on it's own.  Kick only if Jumping == true.
    /// </summary>
    public void JumpKick()
    {
        //JumpKick animation
        if (_animMgr.currentAnimationClip != 3)
        {
            _animMgr.PlayAnimationInteger(3);
        }
    }














    public void Kick()
    {
    }
    public void Crouch()
    {
    }
    public void Punch()
    {
    }
    public void Shield()
    {
    }
}
