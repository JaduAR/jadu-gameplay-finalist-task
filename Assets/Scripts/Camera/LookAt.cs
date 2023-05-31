using UnityEngine;

namespace Camera
{
    /// <summary>
    /// Look At controller.
    /// Use the Follow Weight how close it tracks teh target.
    /// </summary>
    public class LookAt : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _minDistance = 0.1f;
        [Range(0f, 1f)]
        [Tooltip("0 doesn't move, 1 tracks target exactly")]
        [SerializeField] private float _followWeight = 0.5f;
        [SerializeField] private bool _pitch, _yaw, _roll;
    
        private Vector3 _targetPosition;

        private void Start()
        {
            _targetPosition = _target.position + _offset;
        }

        void Update()
        {
            Vector3 newTargetPosition = new(
                _pitch ? _target.position.x : _targetPosition.x,
                _yaw ? _target.position.y : _targetPosition.y,
                _roll ? _target.position.z : _targetPosition.z
            );
        
            if(Vector3.Distance(_targetPosition, newTargetPosition) < _minDistance) return;
            _targetPosition = Vector3.Lerp(_targetPosition, newTargetPosition, _followWeight);
            Vector3 lookAtPosition = _targetPosition + _offset;
            // Debug.Log(_target.position);
            transform.LookAt(lookAtPosition);
        }
    }
}
