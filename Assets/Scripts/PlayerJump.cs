using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerJump : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private PlayerController playerController;

    private bool hasJumped;

    public void OnUpdateSelected(BaseEventData data)
    {
        if(hasJumped)
        {
            IncrementJumpForce();
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        hasJumped = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        hasJumped = false;
    }

    public void OnPointerExit(PointerEventData data)
    {
        hasJumped = false;
    }

    private void IncrementJumpForce()
    {
        playerController.RigidBodyForceValue += Time.deltaTime;

        if(playerController.RigidBodyForceValue >= playerController.MaxRigidBodyForceValue)
        {
            playerController.RigidBodyForceValue = playerController.MaxRigidBodyForceValue;

            return;
        }

        playerController.Jump();
    }
}