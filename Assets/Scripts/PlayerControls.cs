/* Steven Gussman    5/31/23 12:08 AM (WED)    PlayerControls.cs is a script which is wired up
 *                                             to the UI to handle player input via touch / 
                                               clicks*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour{

    // Button GameObjects
    [SerializeField]
    Transform punchButtonTransform, kickButtonTransform, blockButtonTransform,
     jumpButtonTransform, duckButtonTransform;

    // Player Transforms
    [SerializeField]
    Transform playerTransform;
    Vector3 playerInitialPosition;

    // Player animation
    [SerializeField]
    Animator playerAnimator;

    // Jump control variables
    bool isJumping, isFalling;
    float jumpStartTime = -1f, prevJumpDuration, fallStartTime = -1f;
    

    // Called before the first frame update
    void Start(){
        // Store the player's initial position (grounded)
        playerInitialPosition = playerTransform.position;

        // Initialize the player animator parameters
        playerAnimator.SetBool("IsJumping", isJumping);
        playerAnimator.SetBool("IsFalling", isFalling);
    }

    // Called once per frame
    void Update(){         
        // Player is jumping
        if(isJumping){
            /* Time-out after one second of jumping, if the player hasn't manually lifted 
             * the button */
            if(Time.time - jumpStartTime <= 0.333f){
                playerTransform.position += new Vector3(0f, 2f, 0f) * Time.deltaTime;
            }else{
                // Automatically "lift" the player's jump button on timeout
                OnLiftJumpButton();
            }

        /* Player is falling back into place (TO-DO: Would be better handled with
         * collision detection with the ground, rather than time-symmetry) */
        }else if(isFalling){
            /* Timeout after the same duration that you previously jumped, to
             * return the player to the starting position */
            if(Time.time - fallStartTime <= prevJumpDuration){
                playerTransform.position -= new Vector3(0f, 2f, 0f) * Time.deltaTime;
            }else{
                /* Enforce player's position to ground-level (preclude timing glitches
                 * from getting out of hand / persisting) */
                playerTransform.position = playerInitialPosition;
                isFalling = false;
                playerAnimator.SetBool("IsFalling", isFalling);
                prevJumpDuration = 0f;
                fallStartTime = -1f;
            }
        }
    }

    // Event Handlers
    public void OnTouchJumpButton(){
        if(!isJumping){
            // Logic
            isJumping = true;
            playerAnimator.SetBool("IsJumping", isJumping);
            jumpStartTime = Time.time;
            // Visuals
            jumpButtonTransform.localScale *= 1.2f;
            // DEBUG
            print("Jumped!");
        }
    }
    public void OnLiftJumpButton(){
        if(isJumping){
            isJumping = false;
            playerAnimator.SetBool("IsJumping", isJumping);
            prevJumpDuration = Time.time - jumpStartTime;
            jumpStartTime = -1f;
            isFalling = true;
            playerAnimator.SetBool("IsFalling", isFalling);
            fallStartTime = Time.time;
            // Visuals
            jumpButtonTransform.localScale /= 1.2f;
            // DEBUG
            print("Jump done!");
        }
    }
}
