using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.position = Vector3.ProjectOnPlane(target.position, Vector3.up);
    }
}
