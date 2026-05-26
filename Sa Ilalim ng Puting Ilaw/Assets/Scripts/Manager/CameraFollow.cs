using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}