using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1 : MonoBehaviour
{
    public GameObject interactText;
    private bool playerInRange = false;


    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            CollectItem();
           
        }
    }

    void CollectItem()
    {
        Destroy(gameObject);
        FindObjectOfType<SceneTransition>()
    .StartTransition("You wait for the Kapre for a while and notice the smell of smoke...");
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