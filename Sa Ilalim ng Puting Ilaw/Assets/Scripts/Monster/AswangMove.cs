using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static ColorPuzzleManager;

[RequireComponent(typeof(SpriteRenderer))]
public class AswangMove : MonoBehaviour
{
    public TextMeshProUGUI collectText;
    [Header("Audio")]
    public AudioSource sfxAudioSource;
    public AudioClip stunSound;
    public AudioClip recoverSound;
    public AudioSource monsterAudioSource;

    [Header("Proximity Audio")]
    public float maxHearingDistance = 15f;
    public float minVolume = 0f;
    public float maxVolume = 1f;

    public GameObject gameOverPanel;
    public Transform targetPlayer; // Drag player here
    public float movementSpeed = 2.5f;
    private bool triggered = false;
    public GameObject instructionPanel;

    [Header("Animation Frames")]
    public Sprite[] idleSprites;    // Element 0: Down, 1: Up, 2: Right
    public Sprite[] walkDownFrames;
    public Sprite[] walkUpFrames;
    public Sprite[] walkRightFrames;

    [Header("Animation Settings")]
    public float frameRate = 0.15f; // Time in seconds per frame

    [Header("Stun Settings")]
    public float stunDuration = 3.0f; // How long the monster is frozen
    private bool isStunned = false;
    private float stunTimer;

    [Header("Cooldown Settings")]
    public float spacebarCooldown = 8.0f; // Time in seconds before player can stun again
    public TextMeshProUGUI playerFloatingText; // Drag CooldownText here
    private float cooldownTimer = 0f;
    private bool isCooldownActive = false;

    private SpriteRenderer spriteRenderer;
    private float animationTimer;
    private int currentFrameIndex;
    private Vector2 lastDirection = Vector2.down; // Remembers last direction for idle

    void Start()
    {
        collectText.gameObject.SetActive(false);
        Time.timeScale = 0f;
        if (instructionPanel != null) instructionPanel.SetActive(true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (playerFloatingText != null) playerFloatingText.text = "";
    }
    public void StartGame()
    {      
        Time.timeScale = 1f;
        if (instructionPanel != null)
        {
            collectText.gameObject.SetActive(true);
            instructionPanel.SetActive(false);
        }

 
      
    }
    void Update()
    {
        // 1. Process Cooldown Clock
        if (isCooldownActive)
        {
            cooldownTimer -= Time.deltaTime;

            if (playerFloatingText != null)
            {
                playerFloatingText.text = cooldownTimer.ToString("F1") + "s";
            }

            if (cooldownTimer <= 0)
            {
                cooldownTimer = 0f;
                isCooldownActive = false;

                if (playerFloatingText != null)
                {
                    playerFloatingText.text = "Ready!";
                    StartCoroutine(FadeCooldownText());
                }
            }
        }

        // 2. Listen for Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isStunned && !isCooldownActive)
            {
                StunMonster();
            }
            else if (isCooldownActive)
            {
                // If they try to press it during cooldown, warn them instantly
                ShowCooldownWarning();
            }
        }

        if (isStunned)
        {
            HandleStunnedState();
            return; 
        }

        if (targetPlayer == null) return;

        if (targetPlayer != null && monsterAudioSource != null)
        {
            float distance = Vector2.Distance(transform.position, targetPlayer.position);

            float volumePercent = 1f - Mathf.Clamp01(distance / maxHearingDistance);

            monsterAudioSource.volume = Mathf.Lerp(minVolume, maxVolume, volumePercent);
        }

        // 1. Calculate direction vector pointing to the player
        Vector3 displacement = targetPlayer.position - transform.position;

        if (displacement.magnitude > 0.2f)
        {
            Vector2 moveDirection = displacement.normalized;

            // 2. Physically move the monster forward
            transform.position += (Vector3)moveDirection * movementSpeed * Time.deltaTime;

            // 3. Process the movement animations manually
            HandleWalkingAnimation(moveDirection);

            // Save the last direction to use for the correct idle sprite
            lastDirection = moveDirection;
        }
        else
        {
            // 4. Stand still using the correct directional idle sprite
            HandleIdleAnimation();
        }
    }

    void StunMonster()
    {
        isStunned = true;
        stunTimer = stunDuration;

        isCooldownActive = true;
        cooldownTimer = spacebarCooldown;

        if (sfxAudioSource != null && stunSound != null)
        {
            sfxAudioSource.PlayOneShot(stunSound);
        }
        if (playerFloatingText != null)
        {
            playerFloatingText.color = Color.white;
            playerFloatingText.text = cooldownTimer.ToString("F1") + "s";
        }

        StartCoroutine(FlashRoutine());
    }
    void ShowCooldownWarning()
    {
        // Stop any old text clear timers that might be running
        StopAllCoroutines();

        if (playerFloatingText != null)
        {
            // Display remaining time formatted cleanly to 1 decimal place (e.g., "5.4s")
            playerFloatingText.text = cooldownTimer.ToString("F1") + "s";

            // Start a timer to wipe out the text after 1 second so it doesn't linger forever
            StartCoroutine(ClearWarningText());
        }
    }

    IEnumerator ClearWarningText()
    {
        yield return new WaitForSeconds(1.0f);
        if (playerFloatingText != null && !isCooldownActive)
        {
            playerFloatingText.text = "";
        }
    }

    void HandleStunnedState()
    {
        stunTimer -= Time.deltaTime;

        if (stunTimer <= 0)
        {
            isStunned = false;

            Debug.Log("Monster Recovered!");

            if (sfxAudioSource != null && recoverSound != null)
            {
                sfxAudioSource.PlayOneShot(recoverSound);
            }
        }
    }

    void HandleWalkingAnimation(Vector2 dir)
    {
        Sprite[] activeSequence = walkDownFrames;

        // Determine dominant direction (Is it moving more horizontally or vertically?)
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            activeSequence = walkRightFrames;
            // Flip the right sprite horizontally if moving left
            spriteRenderer.flipX = (dir.x < 0);
        }
        else
        {
            // Moving vertically
            spriteRenderer.flipX = false; // Reset flip
            activeSequence = (dir.y > 0) ? walkUpFrames : walkDownFrames;
        }

        // Play through the frames sequentially based on our frame-rate timer
        if (activeSequence != null && activeSequence.Length > 0)
        {
            animationTimer += Time.deltaTime;
            if (animationTimer >= frameRate)
            {
                animationTimer = 0f;
                currentFrameIndex = (currentFrameIndex + 1) % activeSequence.Length;
                spriteRenderer.sprite = activeSequence[currentFrameIndex];
            }
        }
    }

    void HandleIdleAnimation()
    {
        // Reset animation frame counter
        currentFrameIndex = 0;

        if (idleSprites == null || idleSprites.Length < 3) return;

        // Look at the last direction we walked to decide which idle sprite to look at
        if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
        {
            spriteRenderer.sprite = idleSprites[2]; // Right Idle
            spriteRenderer.flipX = (lastDirection.x < 0); // Flip left if needed
        }
        else
        {
            spriteRenderer.flipX = false;
            spriteRenderer.sprite = (lastDirection.y > 0) ? idleSprites[1] : idleSprites[0]; // Up or Down Idle
        }
    }

    IEnumerator FlashRoutine()
    {
        Color originalColor = spriteRenderer.color;
        Color flashColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.4f); // 40% opacity

        while (isStunned)
        {
            spriteRenderer.color = flashColor;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.15f);
        }

        spriteRenderer.color = originalColor; // Ensure it resets perfectly
    }

    IEnumerator FadeCooldownText()
    {
        Color originalColor = playerFloatingText.color;

        yield return new WaitForSeconds(1f); // Keep "Ready!" visible

        float fadeDuration = 1.5f;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            Color c = originalColor;
            c.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            playerFloatingText.color = c;

            yield return null;
        }

        playerFloatingText.text = "";

        Color resetColor = originalColor;
        resetColor.a = 1f;
        playerFloatingText.color = resetColor;
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