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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            interactText.SetActive(false);
        }
    }
}