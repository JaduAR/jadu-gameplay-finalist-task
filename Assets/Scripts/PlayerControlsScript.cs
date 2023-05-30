using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerControlsScript : MonoBehaviour
{
    public float jumpForce = 5;
    public float gravity = -9.81f;
    float velocity;
    private Animator anim;

    public Button jumpButton;
    public Button kickButton;
    public Sprite jumpSprite;
    public Sprite kickSprite;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //simulate constant tug of gravity
        velocity += gravity * Time.deltaTime;
        //stop falling if you hit the "floor"
        if (transform.localPosition.y <= 0 && velocity < 0)
        {
            velocity = 0;
            ResetFloor();
        }
        //can only jump if you're standing on the ground
        if (Input.GetKeyDown(KeyCode.Space) && transform.localPosition.y == 0)
        {
            velocity = jumpForce;
            anim.SetBool("isGrounded", false);
            anim.ResetTrigger("Kick");
            anim.SetTrigger("Jump");
        }
        //if you let go of the jump button before reaching peak height, you start falling early
        if (Input.GetKeyUp(KeyCode.Space) && velocity > 0)
        {
            velocity = 0;
        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);

        //if you slide to the kick button while holding jump, do a jump kick
        if (Input.GetKeyDown(KeyCode.C) /*hold confirm on jump button goes here*/)
        {
            anim.ResetTrigger("Jump");
            anim.SetTrigger("Kick");
        }
    }

    private void ResetFloor()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        anim.SetBool("isGrounded", true);
    }
}