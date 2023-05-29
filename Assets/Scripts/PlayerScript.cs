using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Animator thisAnim;
    [SerializeField]
    private Slider kickSlider;
    private bool kicking = false;
    // Start is called before the first frame update
    void Start()
    {
        thisAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump(){
        thisAnim.SetBool("Jumping", true);
    }

    public void Unjump(){
        thisAnim.SetBool("Jumping", false);
    }

    public void KickCheck(){
        if(kickSlider.value >= 0.9f && !kicking){
            thisAnim.SetTrigger("Kick");
            kicking = true;
        }
        else if(kickSlider.value < 0.9f){
            kicking = false;
        }
    }
}
