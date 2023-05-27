using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ {
    public class ResetAnimatorBool : StateMachineBehaviour
    {
        public string[] boolsToResetName;
        public bool[] boolsToResetStatus;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            for(int i = 0; i < boolsToResetName.Length; i++) {
                animator.SetBool(boolsToResetName[i], boolsToResetStatus[i]);
            }
        }
    }
}
