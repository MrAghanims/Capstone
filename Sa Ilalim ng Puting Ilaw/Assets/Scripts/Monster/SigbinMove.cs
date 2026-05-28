using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigbinMove : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Transform player;

    public float speed = 2f;

    public float chaseRange = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // DISTANCE BETWEEN PLAYER AND MONSTER
        float distance =
            Vector2.Distance(transform.position, player.position);

        // ONLY CHASE IF PLAYER IS CLOSE ENOUGH
        if (distance <= chaseRange)
        {
            MusicManager.Instance.StartChase();
           
          
            Vector2 direction =
                (player.position - transform.position).normalized;

            rb.velocity = direction * speed;

            // FLIP LEFT / RIGHT
            if (direction.x > 0)
            {
                sr.flipX = true;
            }
            else if (direction.x < 0)
            {
                sr.flipX = false;
            }
        }
        else
        {
            // STOP MOVING WHEN PLAYER TOO FAR
            MusicManager.Instance.StopChase();
            rb.velocity = Vector2.zero;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            TriggerGameOver();
        }
    }
    void TriggerGameOver()
    {
        triggered = true;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // pause game
    }
}