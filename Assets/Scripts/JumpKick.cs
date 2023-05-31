using UnityEngine;
using UnityEngine.EventSystems;

public class JumpKick : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private PlayerController playerController;

    public void OnPointerEnter(PointerEventData data)
    {
        playerController.JumpKick();
    }
}