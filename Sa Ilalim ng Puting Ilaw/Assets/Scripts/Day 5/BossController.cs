using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Movement")]
    public float moveStep = 1.5f;
    public float gameOverXPosition = -5.0f;

    public void MoveCloser()
    {
        Vector3 newPosition = transform.position;
        newPosition.x -= moveStep; // March strictly left
        transform.position = newPosition;
    }

    public bool IsTooClose()
    {
        return transform.position.x <= gameOverXPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(gameOverXPosition, -10, 0), new Vector3(gameOverXPosition, 10, 0));
    }
}