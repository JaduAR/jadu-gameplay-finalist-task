using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetBoolTrue(string param)
    {
        anim.SetBool(param, true);
    }

    public void SetBoolFalse(string param)
    {
        anim.SetBool(param, false);
    }

    public void SetTrigger(string param)
    {
        anim.SetTrigger(param);
    }

}
