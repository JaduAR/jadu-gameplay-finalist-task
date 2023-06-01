using UnityEngine;

/// <summary>
/// Made a camera follow the player so it was easier to view the animations playing
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public Transform target; // Object to follow

    private Vector3 offset;

    private void Start()
    {
        if (target != null)
        {
            // Calculate the initial offset between the camera and the target
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position by adding the offset to the target's position
            Vector3 targetPosition = target.position + offset;

            // Set the camera position to the target position
            transform.position = targetPosition;

            // Keep the camera rotation as it is
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
