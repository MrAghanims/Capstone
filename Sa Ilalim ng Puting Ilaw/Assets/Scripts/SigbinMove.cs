using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigbinMove : MonoBehaviour
{
    public Transform player;

    public float speed = 2f;

    public float chaseRange = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

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
            rb.velocity = Vector2.zero;
        }
    }
}