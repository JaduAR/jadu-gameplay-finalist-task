using System;
using Core;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// The Bridge between the UI and Character animation system.
    /// Also useful for any future system that needs to know about character actions the user selects.
    /// </summary>
    public class UserInputEvents : MonoBehaviour
    {
        public static Action<UserInputActions> Action;
    
        private UserInputActions _currentAction = UserInputActions.None;

        public void Jump()
        {
            SetCurrentAction(UserInputActions.Jump);
        }

        public void Kick()
        {
            //Only allowing a Kick if currently Jumping.
            if(_currentAction == UserInputActions.Jump)
                SetCurrentAction(UserInputActions.Kick);
        }

        private void SetCurrentAction(UserInputActions action)
        {
            _currentAction = action;
            Action?.Invoke(action);
            Action?.Invoke(UserInputActions.UserHold);
        }

        public void Release()
        {
            Action?.Invoke(UserInputActions.UserRelease);
            _currentAction = UserInputActions.None;
        }
    }
}
