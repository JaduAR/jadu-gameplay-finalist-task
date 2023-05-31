using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    /// <summary>
    /// Slide expand and rotate of UI RectTransform.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class RectSlide : RectActionBase
    {
        [SerializeField] RectTransform _stretchRect;
        [SerializeField] float _minWidth = 0;
        [SerializeField] float _maxWidth = 100;

        public void Drag(BaseEventData data)
        {
            PointerEventData pointerData = (PointerEventData)data;
            Vector2 screenPoint = pointerData.position;

            if (UnityEngine.Camera.main == null) return;
            
            // Point at pointer
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _canvas.transform as RectTransform, screenPoint,
                UnityEngine.Camera.current, 
                out Vector3 worldPoint);
                
            Vector3 direction = worldPoint - _rectTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rectTransform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
                
            // Stretch to pointer
            float distance = Vector2.Distance(worldPoint, _rectTransform.position) / _canvas.scaleFactor;
            if (distance > _minWidth)
                _stretchRect.sizeDelta = new Vector2(
                    Mathf.Min(_maxWidth, distance + _rectTransform.rect.width),
                    _rectTransform.rect.height);
            _stretchRect.gameObject.SetActive(distance > _minWidth);
        }

        public void Reset()
        {
            _stretchRect.sizeDelta = new Vector2(0, 0);
            _stretchRect.gameObject.SetActive(false);
        }
    }
}
