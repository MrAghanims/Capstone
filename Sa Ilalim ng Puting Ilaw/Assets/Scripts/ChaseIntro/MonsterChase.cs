using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterChase : MonoBehaviour
{
    public FadeController fadeController;
    public Image flashImage;
    private bool caughtPlayer = false;

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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (caughtPlayer) return;

        if (other.CompareTag("Player"))
        {
            caughtPlayer = true;

            StartCoroutine(CatchSequence());
        }
        CutsceneAutoRun playerController =
    other.GetComponent<CutsceneAutoRun>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }
    }
    IEnumerator CatchSequence()
    {
        // Stop monster movement
        rb.velocity = Vector2.zero;
        speed = 0f;

        // White flash
        flashImage.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(0.08f);

        flashImage.color = new Color(1, 1, 1, 0);

        yield return new WaitForSeconds(0.2f);

        // Fade to black
        fadeController.StartFade();
    }
}