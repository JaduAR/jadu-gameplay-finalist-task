
using UnityEngine;

namespace Core
{
    public static class Constants
    {
        #region Animator

        public static readonly int Jump = Animator.StringToHash("jump");
        public static readonly int JumpHold = Animator.StringToHash("jump_held");
        public static readonly int JumpKick = Animator.StringToHash("jump_kick");
        public static readonly int MaxHeight = Animator.StringToHash("max_height");
        public static readonly int AddedHeight = Animator.StringToHash("added_height");
        public static readonly int JumpHeld = Animator.StringToHash("jump_held");

        #endregion
    }
}
