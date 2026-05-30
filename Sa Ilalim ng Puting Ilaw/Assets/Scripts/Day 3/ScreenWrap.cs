using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    public float leftEdge = -12f;
    public float rightEdge = 12f;

    public int loopsNeeded = 3;

    private int loopCount = 0;

    public GameObject objectToAppear;

    void Start()
    {
        // Hide the object at the beginning
        objectToAppear.SetActive(false);
    }

    void Update()
    {
        // Going off the left side
        if (transform.position.x < leftEdge)
        {
            transform.position = new Vector3(
                rightEdge,
                transform.position.y,
                transform.position.z
            );

            AddLoop();
        }

        // Going off the right side
        if (transform.position.x > rightEdge)
        {
            transform.position = new Vector3(
                leftEdge,
                transform.position.y,
                transform.position.z
            );

            AddLoop();
        }
    }

    void AddLoop()
    {
        loopCount++;

        if (loopCount >= loopsNeeded)
        {
            objectToAppear.SetActive(true);
        }
    }
}