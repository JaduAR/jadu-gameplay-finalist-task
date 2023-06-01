using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// THIS SCRIPT CAN BE CALLED TO SWITCH WHICH ANIMATION STATE IS PLAYING ON THE ANIMATOR DEFINED IN THE INSPECTOR FOR THIS SCRIPT
/// </summary>

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public int currentAnimationClip;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    //Call this script to change which animation int is current set
    public void PlayAnimationInteger(int clipNumber)
    {
        anim.SetInteger("animState", clipNumber);
        currentAnimationClip = clipNumber;
//        Debug.Log(this.name + " is animating on clip " + clipNumber);
    }
}
