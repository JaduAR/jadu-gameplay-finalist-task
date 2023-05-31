using Core;
using UnityEngine;

namespace Player
{
    public class PlayerJumpHeightBehavior : StateMachineBehaviour
    {
        [SerializeField] private bool _isAscending = true;
        [SerializeField] private float _startSpeed = 0f;

        private bool _isAscendingInternalSwitch;
        private float _velocity = 3f;
    
        private GameObject _gameObject;
        private CharacterController _controller;
        private Transform _rootJoint;
        private Transform _baseTransform;
    
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layer)
        {
            _isAscendingInternalSwitch = _isAscending;
            _velocity = _startSpeed;
        
            _gameObject = animator.gameObject;
            _controller = _gameObject.GetComponent<CharacterController>();
            _rootJoint = _gameObject.GetComponent<PlayerManager>()?.RootJoint;
            _baseTransform = _gameObject.transform;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_rootJoint == null) return;
        
            if (_isAscending && _isAscendingInternalSwitch)
                Ascending(animator, _controller);
            else
                Descending(_baseTransform, _controller);
        
            animator.SetFloat(Constants.AddedHeight, _baseTransform.position.y); // Used for state condition check
        }
    
        private void Ascending(Animator animator, CharacterController controller)
        {
            float maxHeight = animator.GetFloat(Constants.MaxHeight);
            Vector3 currentPosition = _rootJoint.position;
        
            if (currentPosition.y < maxHeight) // Animate up
            {
                _velocity -= Mathf.Sqrt(-1f / maxHeight * Physics.gravity.y) * Time.deltaTime;
                _velocity = Mathf.Max(_velocity, 0.01f);
                controller.Move(Vector3.up * _velocity * Time.deltaTime);
            }
            else // Set the sate for Descending
            {
                _isAscendingInternalSwitch = false;
                _velocity = 0f;
                animator.SetBool(Constants.JumpHeld, false); // Force changing state
            }
        }

        private void Descending(Transform baseTransform, CharacterController controller)
        {
            if (baseTransform.position.y > 0.1f) // Animate down
            {
                _velocity -= 2 * Physics.gravity.y * Time.deltaTime;
                controller.Move(Vector3.down * _velocity * Time.deltaTime);
            }
            else // Ensure position is reset.
            {
                Vector3 newPosition = new Vector3(baseTransform.position.x, 0f, baseTransform.position.z);
                baseTransform.position = newPosition;
            }
        }
    }
}
