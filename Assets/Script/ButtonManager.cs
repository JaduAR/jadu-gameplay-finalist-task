using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Button contrroler handles all the input of the on screen buttons
/// </summary>
public class ButtonManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Button btnKick;
    private Button btnJump;
    private bool isDragging = false;
    private bool isSlideDetected = false;
    private bool isJump = false;

    public RectTransform rectKick; // Reference to RectTransform of kick button
    public RectTransform rectJump; // Reference to RectTransform of jump button   
    public Image imgJumpKick; // Reference to the image to be shown
    public PlayerController playerController; // Reference to PlayerController

    private void Start()
    {
        btnKick = rectKick.GetComponent<Button>();
        btnJump = rectJump.GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Called when a button is pressed down
        if (eventData.pointerCurrentRaycast.gameObject == btnJump.gameObject)
        {
            playerController.StartJump();
            isJump = true;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Called when a button is released
        if (eventData.pointerCurrentRaycast.gameObject == btnJump.gameObject && isJump == true)
        {
            playerController.StartDescending();
            isJump = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Called when the pointer exits the area of a button
        if (eventData.pointerCurrentRaycast.gameObject != btnJump.gameObject && isJump == true)
        {
            playerController.StartDescending();
            isJump = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Called when the dragging starts
        isDragging = RectTransformUtility.RectangleContainsScreenPoint(rectJump, eventData.position);
        isSlideDetected = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Called when the dragging is ongoing
        if (isDragging && !isSlideDetected)
        {
            Vector2 currentPointerPosition = eventData.position;

            // Check if the current position of the drag is within button2
            if (RectTransformUtility.RectangleContainsScreenPoint(rectKick, currentPointerPosition))
            {
                playerController.Kick();
                isSlideDetected = true;
                HideButtons();
                ShowImage();
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Called when the dragging ends
        if (isSlideDetected)
        {
            ShowButtons();
            HideImage();
        }

        isDragging = false;
        isSlideDetected = false;
    }

    private void HideButtons()
    {
        btnJump.gameObject.SetActive(false);
    }

    private void ShowButtons()
    {
        btnJump.gameObject.SetActive(true);
    }

    private void ShowImage()
    {
        imgJumpKick.gameObject.SetActive(true);
    }

    private void HideImage()
    {
        imgJumpKick.gameObject.SetActive(false);
    }
}