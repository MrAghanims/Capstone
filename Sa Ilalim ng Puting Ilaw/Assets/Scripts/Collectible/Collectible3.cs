using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible3 : MonoBehaviour
{
    public GameObject interactText;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            CollectItem();
            CollectionManager3.Instance.AddCollectible();
        }
    }

    void CollectItem()
    {
        Debug.Log("Item collected!");

        // Hide UI
        interactText.SetActive(false);

        // Remove item
        Destroy(gameObject);
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