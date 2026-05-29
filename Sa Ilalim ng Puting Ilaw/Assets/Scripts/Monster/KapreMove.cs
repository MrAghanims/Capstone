using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KapreMove : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Fade In")]
    public float fadeDuration = 2f;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public Vector2 moveDirection = Vector2.right;

    [Header("Scene Transition")]
    public Image fadeImage; // UI black image
    public float sceneFadeDuration = 2f;
    public string nextSceneName;

    private bool canMove = false;
    private bool triggered = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Start invisible
        Color color = sr.color;
        color.a = 0f;
        sr.color = color;

        // Start black image invisible
        Color fadeColor = fadeImage.color;
        fadeColor.a = 0f;
        fadeImage.color = fadeColor;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            Color color = sr.color;
            color.a = alpha;
            sr.color = color;

            yield return null;
        }

        Color finalColor = sr.color;
        finalColor.a = 1f;
        sr.color = finalColor;

        canMove = true;
    }

    void Update()
    {
        if (canMove && !triggered)
        {
            rb.velocity = moveDirection * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            StartCoroutine(EndSequence(collision.gameObject));
        }
    }

    IEnumerator EndSequence(GameObject player)
    {
        canMove = false;

        // Stop monster
        rb.velocity = Vector2.zero;

        // Stop player movement
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.velocity = Vector2.zero;
        }

        // Disable player movement script
        MonoBehaviour movementScript = player.GetComponent<PlayerMovement>();

        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        // Stop animations
        if (anim != null)
        {
            anim.enabled = false;
        }

        Animator playerAnim = player.GetComponent<Animator>();

        if (playerAnim != null)
        {
            playerAnim.enabled = false;
        }

        // Fade to black
        float timer = 0f;

        while (timer < sceneFadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(0f, 1f, timer / sceneFadeDuration);

            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}