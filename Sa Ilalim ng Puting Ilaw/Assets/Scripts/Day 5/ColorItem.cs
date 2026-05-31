using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This line guarantees that the GameObject has an AudioSource component attached
[RequireComponent(typeof(AudioSource))]
public class ColorItem : MonoBehaviour
{
    public GameObject interactText;
    public int colorID; // 0 to 4

    [Header("SFX Settings")]
    public AudioClip interactionSFX; // Drag this specific pot's sound here

    private ColorPuzzleManager puzzleManager;
    private AudioSource audioSource;
    private bool isPlayerNearby = false;
    private Coroutine soundStopCoroutine;

    void Start()
    {
        puzzleManager = Object.FindFirstObjectByType<ColorPuzzleManager>();

        // Grab the AudioSource component on this pot
        audioSource = GetComponent<AudioSource>();

        // Optimize audio source settings for programmatic control
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (puzzleManager != null)
        {
            // Only play audio and process input if it's actually the player's turn
            if (puzzleManager.currentState == ColorPuzzleManager.GameState.PlayerTurn)
            {
                puzzleManager.PlayerSelectedColor(colorID);
                PlayPotSound();
                StartCoroutine(BounceFeedback());
            }
        }
    }

    void PlayPotSound()
    {
        if (interactionSFX != null && audioSource != null)
        {
            // If the player presses 'F' rapidly, stop the previous cutoff routine 
            // so a new 2-second timer can start fresh.
            if (soundStopCoroutine != null)
            {
                StopCoroutine(soundStopCoroutine);
            }

            audioSource.clip = interactionSFX;
            audioSource.Play();

            // Start the timer to stop the sound after 2 seconds
            soundStopCoroutine = StartCoroutine(StopSoundAfterDelay(2.0f));
        }
        else
        {
            Debug.LogWarning($"Pot {colorID} is missing an Audio Clip asset!");
        }
    }

    IEnumerator StopSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Gently fade out or abruptly stop. audioSource.Stop() will stop it instantly.
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.SetActive(false);
        }
    }

    IEnumerator BounceFeedback()
    {
        transform.localScale *= 1.1f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale /= 1.1f;
    }
}