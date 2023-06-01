using UnityEngine;

/// <summary>
/// Player Controller that handles the players different states
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float ascendSpeed = 5f; // Speed at which the character ascends
    public float descendSpeed = 5f; // Speed at which the character descends
    public float maxAscendHeight = 10f; // Maximum height the character can ascend
    public float kickDuration = 1f; // Duration of the kick animation

    private bool isAscending = false;
    private bool isJumping = false;
    private float currentAscendHeight = 0f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isJumping)
        {
            if (isAscending)
            {
                if (currentAscendHeight < maxAscendHeight)
                {
                    // Ascend the character
                    Vector3 moveDirection = Vector3.up * ascendSpeed * Time.deltaTime;
                    GetComponent<CharacterController>().Move(moveDirection);
                    currentAscendHeight += moveDirection.y;
                }
                else
                {
                    StartDescending();
                }
            }
            else
            {
                // Descend the character
                Vector3 moveDirection = Vector3.down * descendSpeed * Time.deltaTime;
                GetComponent<CharacterController>().Move(moveDirection);
                currentAscendHeight += moveDirection.y;

                if (currentAscendHeight <= 0f)
                {
                    FinishJump();
                }
            }
        }
    }

    /// <summary>
    /// Start the jump sequence
    /// </summary>
    public void StartJump()
    {
        if (!isJumping)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Ascend", true);
            isJumping = true;
            isAscending = true;
        }
    }

    /// <summary>
    /// Start descending the player
    /// </summary>
    public void StartDescending()
    {
        if (isAscending)
        {
            // Trigger the Descending animation
            animator.SetBool("Ascend", false);
            animator.SetBool("Descend", true);
            isAscending = false;
        }
    }

    /// <summary>
    /// Finish out the jump sequence
    /// </summary>
    public void FinishJump()
    {
        // Reset jump flags and height
        animator.SetBool("Descend", false);
        animator.SetBool("Idle", true);

        isJumping = false;
        isAscending = false;
    }

    /// <summary>
    /// player performs a kick
    /// </summary>
    public void Kick()
    {
        // Check if the character is currently jumping and ascending
        if (isJumping && isAscending)
        {
            animator.SetTrigger("JumpKick");
        }
    }
}

