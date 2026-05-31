using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossController : MonoBehaviour
{
    [Header("Movement")]
    public float moveStep = 1.5f;
    public float gameOverXPosition = -5.0f;

    [Header("Audio Settings")]
    public AudioClip bossMoveSFX;
    private AudioSource audioSource;

    void Start()
    {
        // Grab and configure the AudioSource component on the boss
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }
    public void MoveCloser()
    {
        Vector3 newPosition = transform.position;
        newPosition.x -= moveStep; // March strictly left
        transform.position = newPosition;

        if (bossMoveSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(bossMoveSFX);
        }
        else
        {
            Debug.LogWarning("Boss is moving, but 'Boss Move SFX' is missing in the Inspector!");
        }
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