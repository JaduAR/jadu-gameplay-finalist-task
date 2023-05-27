using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField]
    private Transform targetRoot;
    [SerializeField]
    private Transform heightTarget;
    [SerializeField]
    private float heightOffset;
    [SerializeField]
    private float followSpeed;

    Vector3 targetPosition = Vector3.zero;

    private void LateUpdate()
    {
        if(!targetRoot) return;

        targetPosition.x = targetRoot.position.x;
        targetPosition.z = targetRoot.position.z;
        targetPosition.y = heightTarget.position.y + heightOffset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }
}
