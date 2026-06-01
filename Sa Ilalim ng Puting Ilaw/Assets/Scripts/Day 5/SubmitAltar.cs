using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitAltar : MonoBehaviour
{
    public GameObject interactText;
    private ColorPuzzleManager puzzleManager;
    private bool playerNearby = false;

    void Start()
    {
        puzzleManager = Object.FindFirstObjectByType<ColorPuzzleManager>();
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Answer Submitted!");
            puzzleManager.SubmitAnswer();
            HideInteractText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;

            if (interactText != null)
                interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;

            if (interactText != null)
                interactText.SetActive(false);
        }
    }

    public void HideInteractText()
    {
        if (interactText != null)
        {
            interactText.SetActive(false);
        }

        playerNearby = false;
    }
}