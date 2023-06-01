using System.Collections;
using UnityEngine;

public class PlayerCharacterControl : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _anim;
    [SerializeField] private Collider _coll;
    [SerializeField] private Rigidbody _rb;

    [Header("Objects")]
    [SerializeField] private UIControl _uiControl;

    [Header("Gameplay Feel")]
    [SerializeField] private float _jumpVelocity;
    [SerializeField] private float _maxJumpHeight;

    [Header("Current State")]
    private bool _isCrouched;
    private bool _isDefended;
    private bool _isJumping;
    private bool _isJumpDrag;
    private bool _isKicking;
    private bool _isPunching;

    private bool _canKick = true;
    private bool _onGround = true;

    private bool CanJump => _onGround;

    #region Button Events

    public void BtnEvt_Crouch() { }

    public void BtnEvt_Defend() { }

    public void BtnEvt_Jump() 
    {
        if (_isJumping || !CanJump)
        {
            return;
        }

        StartCoroutine(Jump_Co());
    }

    public void BtnEvt_JumpEnd()
    {
        if (!_isJumping)
        {
            return;
        }

        _isJumping = false;
    }

    public void BtnEvt_Kick() { }

    public void BtnEvt_Punch() { }

    public void DragEvt_Jump() 
    {
        if (_isJumpDrag)
        {
            return;
        }

        _isJumpDrag = true;
    }

    public void DragEvt_JumpEnd()
    {
        if (!_isJumpDrag)
        {
            return;
        }
        _isJumpDrag = false;
    }

    public void PtrEvt_Kick()
    {
        //must be in the air, dragging jump button, and not kicked yet this jump
        if (_onGround || !_isJumpDrag || !_canKick)
        {
            return;
        }

        _anim.SetTrigger("Kick");
        _canKick = false;
    }

    #endregion

    private IEnumerator Jump_Co()
    {
        _isJumping = true;
        float startHeight = transform.position.y;
        float maxHeight = startHeight + _maxJumpHeight;
        _onGround = false;
        _anim.SetTrigger("Jump");

        //Jump
        while (_isJumping && transform.position.y <= maxHeight)
        {
            Vector3 velo = _rb.velocity;
            _rb.velocity = new Vector3(velo.x, _jumpVelocity, velo.z);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //OnGround after colliding with ground
        if (!_onGround && collision.gameObject.name.Contains("Ground"))
        {
            _canKick = true;
            _onGround = true;
            _uiControl.JumpEnd();
            _anim.SetTrigger("Land");
        }
    }
}
