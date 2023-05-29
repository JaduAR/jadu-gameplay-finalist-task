using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButtonScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    private PlayerScript thePlayer;
    [SerializeField]
    private Slider theSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData){
        thePlayer.Jump();
    }

    public void OnPointerUp(PointerEventData eventData){
        thePlayer.Unjump();
    }

    public void OnDrag(PointerEventData eventData){
        theSlider.OnDrag(eventData);
    }
}
