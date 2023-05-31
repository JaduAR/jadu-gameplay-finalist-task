using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector3 offset;
    CanvasGroup canvasGroup;
    public string destinationTag = "DropArea";
    public bool isInteractible = true;
    public bool JumpKick = true;

    public float JumpBtnRadius = 200;

    //private float speed = 10.0f;
    //private Vector2 target;
    //private Vector2 position;

    private RaycastResult raycastResult;
    private Transform PreviousePosition;
    
    private float distance;
    Vector3 startPosition;

    void Awake()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        //PreviousePosition = gameObject.transform;
    }
    private void Start()
    {
        startPosition = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isInteractible)
        {
            distance = Vector3.Distance(startPosition, transform.position);
            if (distance< JumpBtnRadius)
            {
                transform.position = Input.mousePosition + offset;
            }

            raycastResult = eventData.pointerCurrentRaycast;
            if (raycastResult.gameObject?.tag == destinationTag)
            {
                Debug.Log("Tag Detected");
                //transform.position = raycastResult.gameObject.transform.position;
                PlayerManager.Instance.EnableJumpKickBG(1,0);
                if (JumpKick)
                {
                    PlayerManager.Instance.PerformJumpKick();
                    JumpKick = false;
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInteractible)
        {
            offset = transform.position - Input.mousePosition;
            canvasGroup.alpha = 0.5f;
            canvasGroup.blocksRaycasts = false;
            PlayerManager.Instance.JumpUp();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInteractible)
        {
            //RaycastResult raycastResult = eventData.pointerCurrentRaycast;
            //if (raycastResult.gameObject?.tag == destinationTag)
            //{
            //    Debug.Log("Tag Detected");
            //    transform.position = raycastResult.gameObject.transform.position;
            //}
            //transform.position = raycastResult.gameObject.transform.position;
            JumpKick = true;
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            transform.position = startPosition;
            PlayerManager.Instance.EnableJumpKickBG(0, 1);
            //transform.position = PreviousePosition.position;
            //PlayerManager.Instance.ReturnToPos(gameObject.transform);
            PlayerManager.Instance.JumpDown();
        }
    }
    
}