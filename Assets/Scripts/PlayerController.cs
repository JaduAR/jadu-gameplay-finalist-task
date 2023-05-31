using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private float rigidBodyForceValue;
    [SerializeField] private float maxRigidBodyForceValue;

    private float defaultRigidBodyForceValue;

    [Header("Player Animator Names")]
    [SerializeField] private string playerIdleAnimatorName;
    [SerializeField] private string jumpAnimatorName;
    [SerializeField] private string jumpKickAnimatorName;

    public float MaxRigidBodyForceValue => maxRigidBodyForceValue;

    public float DefaultRigidBodyForceValue => defaultRigidBodyForceValue;

    public float RigidBodyForceValue
    {
        get => rigidBodyForceValue;
        set => rigidBodyForceValue = value;
    }

    private void Awake()
    {
        defaultRigidBodyForceValue = rigidBodyForceValue;
    }

    public void Jump()
    {
        rigidBody.AddForce(Vector3.up * rigidBodyForceValue, ForceMode.VelocityChange);

        if(rigidBody.velocity == Vector3.zero)
           playerAnimator.Play(jumpAnimatorName);
    }

    public void JumpKick()
    {
        if(rigidBody.velocity.y > 0 || rigidBody.velocity.y < 0)
        {
            playerAnimator.Play(jumpKickAnimatorName);
        }
    }

    private void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
    }

    public void ResetRigidBodyForceValue()
    {
        rigidBodyForceValue = defaultRigidBodyForceValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<MeshCollider>())
        {
            playerAnimator.Play(playerIdleAnimatorName);

            ResetVelocity();
            ResetRigidBodyForceValue();
        }
    }
}