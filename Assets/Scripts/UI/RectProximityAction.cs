using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class RectProximityAction : RectActionBase
    {
        [SerializeField] private float _proximityRadius = 0.5f;
        [SerializeField] private UnityEvent _event;
        
        private bool _isPointerInside;

        public void PointerEnter()
        {
            _isPointerInside = true;
        }
        
        public void PointerExit()
        {
            _isPointerInside = false;
        }

        public void Update()
        {
            if(!_isPointerInside) return;
            if (UnityEngine.Camera.main == null) return;
            
            Vector2 screenPoint = Input.mousePosition;
            
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _canvas.transform as RectTransform, screenPoint,
                UnityEngine.Camera.current,
                out Vector3 worldPoint);

            // Check distance
            float distance = Vector2.Distance(worldPoint, _rectTransform.position) / _canvas.scaleFactor;
            if (distance < _proximityRadius)
                _event.Invoke();
                
        }
    }
}
