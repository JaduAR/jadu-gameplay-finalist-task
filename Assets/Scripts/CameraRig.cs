using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float followSpeed;

    private void LateUpdate()
    {
        if(!target) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
    }
}
