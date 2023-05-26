using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{
    private Animator anim;
    public bool jumping;
    private Rigidbody rb;
    public float jumpAmount = 20;
    private float jumpTime , buttonTime=0.3f;
    public Slider slide;
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (jumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpAmount, rb.velocity.z);
            jumpTime += Time.deltaTime;
        }
        if (jumpTime > buttonTime)
        {
            jumping = false;
            anim.SetBool("KickTime", false);

        }
        if (slide.value == 1)
            Kick(); 
    }

    public void Jump()
    {
        anim.SetTrigger("Jump");
        jumping = true;
        jumpTime = 0;
    }

    public void Descending()
    {
        jumping = false;
        anim.SetBool("KickTime", false);
    }

    public void Kick()
    {
        //if(jumping &&  jumpTime < buttonTime)
        //{
        anim.SetBool("KickTime", true);
            Debug.Log("Kick");
            anim.SetTrigger("Kick");
        //}
    }
}
