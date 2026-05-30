using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap2 : MonoBehaviour
{
    public GameObject interactText;
    private bool playerInRange = false;
    public GameObject trap;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            CollectItem();
            trap.SetActive(true);
        }
    }

    void CollectItem()
    {
        Destroy(gameObject);
        FindObjectOfType<SceneTransition2>()
    .StartTransition2("You remember what lola said and quickly wear your shirt in reverse...");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactText.SetActive(false);
        }
    }
}