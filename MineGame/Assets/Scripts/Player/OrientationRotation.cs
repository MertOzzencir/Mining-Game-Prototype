using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationRotation : MonoBehaviour
{
    public Transform Orientation;
    void Update()
    {
        Vector3 forward = Camera.main.transform.forward.normalized;
        Orientation.forward = new Vector3(forward.x,0,forward.z);

    }
}
