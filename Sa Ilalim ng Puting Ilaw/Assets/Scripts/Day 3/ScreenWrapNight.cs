using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapNight : MonoBehaviour
{
    public QuestionManager questionManager;
    public float leftEdge = -12f;
    public float rightEdge = 12f;



    void Start()
    {
        
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

            questionManager.AddLoop();
        }

        // Going off the right side
        if (transform.position.x > rightEdge)
        {
            transform.position = new Vector3(
                leftEdge,
                transform.position.y,
                transform.position.z
            );

            questionManager.AddLoop();
        }
    }

   
}