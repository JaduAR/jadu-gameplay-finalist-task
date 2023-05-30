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
    public Image jumpKickGraphic;

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
        
        //if you let go of the jump button before reaching peak height, you start falling early
        if (Input.touchCount > 0 && velocity > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                jumpButton.GetComponent<Image>().enabled = true;
                jumpKickGraphic.enabled = false;
                velocity = 0;
            }
        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
    }

    public void Jump()
    {
        //can only jump if you're standing on the ground
        if (Input.touchCount > 0 && transform.localPosition.y == 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                jumpButton.GetComponent<Image>().enabled = false;
                jumpKickGraphic.enabled = true;
                velocity = jumpForce;
                anim.SetBool("isGrounded", false);
                anim.ResetTrigger("Kick");
                anim.SetTrigger("Jump");
            }
        }
    }

    public void Kick()
    {
        //if you slide to the kick button without removing your finger from the screen after jumping, perform kick animation
        if (Input.touchCount > 0)
        {
            //these touch phase checks ensure that the kick button cannot be individually pressed, the initating touch must slide to the button
            //from elsewhere on the screen
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                anim.ResetTrigger("Jump");
                anim.SetTrigger("Kick");
            }
        }
    }

    private void ResetFloor()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        anim.SetBool("isGrounded", true);
    }
}