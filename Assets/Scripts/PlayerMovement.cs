using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private CharacterController _character;

    [Header("Jump Settings")]
    [SerializeField]
    [Min(0f)]
    float _initialJumpSpeed;

    [SerializeField]
    [Min(0f)]
    float _gravityScale;

    [SerializeField]
    [Min(0f)]
    float _jumpCancelGravityScale;

    [SerializeField]
    [Min(0f)]
    float _jumpEndSpeed;

    private Vector3 _velocity;
    private bool _wasGrounded = false;
    private bool _jumpCanceled = false;
    private bool _isJumpPressed = false;
    private bool _isKicking = false;

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _isJumpPressed = true;
        }

        if (context.canceled)
        {
            _isJumpPressed = false;
        }
    }

    public void Kick(InputAction.CallbackContext context)
    {
        if (context.started && !_character.isGrounded)
        {
            _animator.SetTrigger("Kick");
            // prevent the kick anim from being interrupted by the landing anim 
            _velocity.y = Mathf.Max(0.0f, _velocity.y);
            _isKicking = true;
            _isJumpPressed = false;
        }
    }

    public void Update()
    {
        if (_character.isGrounded)
        {
            HandleGroundState();
        }
        else
        {
            HandleJumpState();
        }

        _wasGrounded = _character.isGrounded;
        _character.Move(_velocity * Time.deltaTime);
    }

    private void HandleGroundState()
    {
        if (_isJumpPressed)
        {
            _animator.SetTrigger("Jump");
            _jumpCanceled = false;
            _velocity.y = _initialJumpSpeed;
        }
        else if (!_wasGrounded)
        {
            _animator.SetTrigger("Land");
            _isKicking = false;
        }
    }

    private void HandleJumpState()
    {
        if (!_isJumpPressed && !_jumpCanceled)
        {
            _jumpCanceled = true;
            if (!_isKicking)
            {
                _animator.SetTrigger("JumpEnd");
            }
        }

        float gravitySpeed = Mathf.Abs(Physics.gravity.y);

        if (_jumpCanceled && _velocity.y > _jumpEndSpeed)
        {
            gravitySpeed *= _jumpCancelGravityScale;
        }
        else
        {
            gravitySpeed *= _gravityScale;
        }

        _velocity.y -= gravitySpeed * Time.deltaTime;
    }
}
