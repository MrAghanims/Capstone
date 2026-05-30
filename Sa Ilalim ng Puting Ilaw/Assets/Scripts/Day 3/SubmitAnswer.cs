using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitAnswer : MonoBehaviour
{
    public GameObject interactText;
    public QuestionManager questionManager;

    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            questionManager.SubmitAnswer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            interactText.SetActive(false);
        }
    }
}