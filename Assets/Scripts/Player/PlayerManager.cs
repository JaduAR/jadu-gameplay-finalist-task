using System;
using Core;
using UI;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// PlayerManager is a bridge between User events and other systems.
    /// Initially this is the bridge to the Animation system.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerManager : MonoBehaviour
    {
        public Transform RootJoint;
    
        [SerializeField] private float _maxHeight;
    
        private Animator _animator;

        private void Awake()
        {
            UserInputEvents.Action += OnUserInput;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _animator.SetFloat(Constants.MaxHeight, _maxHeight);
        }

        private void OnDestroy()
        {
            UserInputEvents.Action -= OnUserInput;
        }
    
        private void OnUserInput(UserInputActions action)
        {
            switch (action)
            {
                case UserInputActions.Jump:
                    JumpAction();
                    break;
                case UserInputActions.Crouch:
                    //TODO waiting on Crouch design
                    break;
                case UserInputActions.Punch:
                    //TODO waiting on Punch design
                    break;
                case UserInputActions.Kick:
                    KickAction();
                    break;
                case UserInputActions.Shield:
                    //TODO waiting on Shield design
                    break;
                case UserInputActions.UserHold:
                    HoldAction();
                    break;
                case UserInputActions.UserRelease:
                    ReleaseAction();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void JumpAction()
        {
            _animator.SetTrigger(Constants.Jump);
        }

        private void KickAction()
        {
            _animator.SetTrigger(Constants.JumpKick);
        }

        private void HoldAction()
        {
            _animator.SetBool(Constants.JumpHold, true);
        }
    
        private void ReleaseAction()
        {
            _animator.SetBool(Constants.JumpHold, false);
        }
    }
}