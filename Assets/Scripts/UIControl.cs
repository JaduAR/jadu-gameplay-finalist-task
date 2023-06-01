using System.Collections;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private CanvasGroup _jumpKick;

    [Header("Appearance")]
    [SerializeField] private float _jumpKickFadeTime; //time from alpha = 0 -> 1
    private float _jumpKickTargetAlpha;

    #region Button Events

    public void BtnEvt_Jump()
    {
        _jumpKickTargetAlpha = 1;
        StartCoroutine(SetJumpKickOpacity());
    }

    public void BtnEvt_JumpEnd()
    {
        JumpEnd();
    }

    #endregion

    public void JumpEnd()
    {
        _jumpKickTargetAlpha = 0;
        StartCoroutine(SetJumpKickOpacity());
    }

    private IEnumerator SetJumpKickOpacity()
    {
        while (_jumpKick.alpha != _jumpKickTargetAlpha)
        {
            float alphaChange = Time.deltaTime / _jumpKickFadeTime;
            float alpha = _jumpKick.alpha;
            alpha += _jumpKick.alpha < _jumpKickTargetAlpha ? alphaChange : -alphaChange;
            alpha = Mathf.Clamp(alpha, 0, 1);
            _jumpKick.alpha = alpha;
            yield return null;
        }
    }
}
