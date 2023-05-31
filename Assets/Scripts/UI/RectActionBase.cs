using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class RectActionBase : MonoBehaviour
    {
        protected RectTransform _rectTransform;
        protected Canvas _canvas;

        public virtual void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = _rectTransform.GetComponentInParent<Canvas>();
        }
    }
}