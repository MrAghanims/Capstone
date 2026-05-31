using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorItem : MonoBehaviour
{
    public GameObject interactText;
    public int colorID; // 0 to 4
    private ColorPuzzleManager puzzleManager;
    private bool isPlayerNearby = false;

    void Start()
    {
        // Automatically finds the manager in your scene
        puzzleManager = Object.FindFirstObjectByType<ColorPuzzleManager>();
    }

    void Update()
    {
        // If the player is nearby and presses the F key
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (puzzleManager != null)
        {
            puzzleManager.PlayerSelectedColor(colorID);
            Debug.Log($"Interacted with Pot {colorID} using 'F' key!");

            // Optional visual feedback: Make the pot bounce slightly when pressed
            StartCoroutine(BounceFeedback());
        }
    }

    // Detect when the player walks up to the pot
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.SetActive(true);
            // You could show a "Press F" UI prompt here
        }
    }

    // Detect when the player walks away from the pot
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.SetActive(false);
        }
    }

    System.Collections.IEnumerator BounceFeedback()
    {
        transform.localScale *= 1.1f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale /= 1.1f;
    }
}