using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class to hold actions that can be performed on a RectTransform.
    /// Initially this is the ability to enlarge a RectTransform to a given size, and rest it.
    /// A good place to use DOTween for animating this.
    /// </summary>
    public class RectAction : RectActionBase
    {
        [SerializeField] private Vector2 _largeSize;
        [SerializeField] private float _duration = 1f;
        
        private Vector2 _baseSize;

        private float _timeRemaining = 0;
        private Vector2 _fromSize;
        private Vector2 _toSize;

        public override void Start()
        {
            base.Start();
            _baseSize = _rectTransform.rect.size;
        }

        private void Update()
        {
            if(_timeRemaining <= 0) return;
            _rectTransform.sizeDelta = Vector2.Lerp(_toSize, _fromSize, _timeRemaining / _duration);
            _timeRemaining -= Time.deltaTime;
        }

        public void Enlarge()
        {
            AnimateToSize(_largeSize, _baseSize);
        }

        public void SizeReset()
        {
            AnimateToSize(_baseSize, _largeSize);
        }

        private void AnimateToSize(Vector2 targetSize, Vector2 currentSize)
        {
            _timeRemaining = _duration;
            _toSize = targetSize;
            _fromSize = currentSize;
        }
    }
}
