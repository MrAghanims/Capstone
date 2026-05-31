using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPuzzleManager : MonoBehaviour
{
    public AudioClip bossStaySFX;
    private AudioSource audioSource;

    public TextMeshProUGUI scoreboardText;
    public Slider timerSlider;
    public GameObject gameOverPanel;
    public GameObject WinPanel;
    public enum GameState { Generating, Showing, PlayerTurn, Processing, GameOver, GameWon }
    public GameState currentState;

    [Header("Color Settings")]
    public int totalColorsAvailable = 5;
    public List<int> bossSequence = new List<int>();
    public List<int> playerSequence = new List<int>();
    public Color[] actualColors; // Element 0 to 4 matching your pots

    [Header("UI References")]
    public GameObject colorSquarePrefab; // Drag your 'UIColorSquare' prefab here
    public Transform bossUIPanel;        // Drag 'BossSequencePanel' here
    public Transform playerUIPanel;      // Drag 'PlayerSequencePanel' here

    [Header("Timers & Progression")]
    public float timeToShowSequence = 3.0f;
    public float timeToInput = 15.0f; // Giving more time since they have to walk to submit
    private float currentTimer;

    [Header("Win/Loss Conditions")]
    public int successfulMatches = 0;
    public int targetWins = 10;
    public BossController boss;

    private List<GameObject> spawnedBossUI = new List<GameObject>();
    private List<GameObject> spawnedPlayerUI = new List<GameObject>();
    private int totalAllowedFailures;
    private int currentFailures = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        successfulMatches = 0;
        currentFailures = 0;

        // Calculate how many times the boss can move left before crossing the line
        if (boss != null)
        {
            float totalDistance = Mathf.Abs(boss.transform.position.x - boss.gameOverXPosition);
            totalAllowedFailures = Mathf.CeilToInt(totalDistance / boss.moveStep);
        }

        UpdateScoreboardUI();
        StartNextRound();
    }

    void Update()
    {
        if (currentState == GameState.PlayerTurn)
        {
            currentTimer -= Time.deltaTime;

            // Update the visual slider fill percentage
            if (timerSlider != null)
            {
                timerSlider.value = currentTimer / timeToInput;
            }

            // TIMEOUT FIX: Check if time ran out
            if (currentTimer <= 0)
            {
                // Instantly lock the state so this block can't repeat next frame!
                currentState = GameState.Processing;

                if (timerSlider != null) timerSlider.value = 0;
                OnRoundFailed();
            }
        }
    }

    void StartNextRound()
    {
        // Ensure the state resets cleanly
        currentState = GameState.Generating;

        ClearUI(spawnedBossUI);
        ClearUI(spawnedPlayerUI);
        playerSequence.Clear();
        bossSequence.Clear();

        // Explicitly make sure the boss panel is visible before generating the visual squares
        if (timerSlider != null) timerSlider.gameObject.SetActive(false);
        bossUIPanel.gameObject.SetActive(true);

        int sequenceLength = 3;
        for (int i = 0; i < sequenceLength; i++)
        {
            bossSequence.Add(Random.Range(0, totalColorsAvailable));
        }

        StartCoroutine(DisplaySequenceCo());
    }

    IEnumerator DisplaySequenceCo()
    {
        currentState = GameState.Showing;

        // Render Boss Sequence onto the UI Canvas
        foreach (int colorID in bossSequence)
        {
            GameObject square = Instantiate(colorSquarePrefab, bossUIPanel);
            square.GetComponent<Image>().color = actualColors[colorID];
            spawnedBossUI.Add(square);
        }

        yield return new WaitForSeconds(timeToShowSequence);

        // Hide Boss UI (Turn off visibility so player has to memorize it)
        bossUIPanel.gameObject.SetActive(false);

        if (timerSlider != null)
        {
            timerSlider.gameObject.SetActive(true);
            timerSlider.value = 1f;
        }

        currentState = GameState.PlayerTurn;
        currentTimer = timeToInput;
    }

    // Called by pots
    public void PlayerSelectedColor(int colorIndex)
    {
        if (currentState != GameState.PlayerTurn) return;

        // Prevent overflow inputs beyond sequence length
        if (playerSequence.Count >= bossSequence.Count) return;

        playerSequence.Add(colorIndex);

        // Visually show what the player typed on their UI tracking bar
        GameObject square = Instantiate(colorSquarePrefab, playerUIPanel);
        square.GetComponent<Image>().color = actualColors[colorIndex];
        spawnedPlayerUI.Add(square);
    }

    // Called specifically by the new Submit Altar script
    public void SubmitAnswer()
    {
        // CRUCIAL LOCK: Only allow submission if it is explicitly the Player's turn!
        if (currentState != GameState.PlayerTurn) return;

        // Immediately change state so this function cannot run again next frame
        currentState = GameState.Processing;

        // Check lengths
        if (playerSequence.Count != bossSequence.Count)
        {
            OnRoundFailed();
            return;
        }

        // Validate the answers
        for (int i = 0; i < bossSequence.Count; i++)
        {
            if (playerSequence[i] != bossSequence[i])
            {
                OnRoundFailed();
                return;
            }
        }

        OnRoundSuccess();
    }

    void OnRoundSuccess()
    {
        successfulMatches++;
        UpdateScoreboardUI();
        Debug.Log($"Correct! {successfulMatches}/{targetWins}");
        bossUIPanel.gameObject.SetActive(true); // Turn panel visibility back on
        audioSource.PlayOneShot(bossStaySFX);

        if (successfulMatches >= targetWins)
        {
            ClearUI(spawnedBossUI);
            ClearUI(spawnedPlayerUI);
            currentState = GameState.GameWon;
            WinPanel.SetActive(true);
            Debug.Log("VICTORY!");
            Time.timeScale = 0f;
        }
        else
        {
            Invoke("StartNextRound", 1.5f);
        }
    }

    void OnRoundFailed()
    {
        currentFailures++;
        UpdateScoreboardUI();
        Debug.Log("Incorrect combination or timeout!");
        bossUIPanel.gameObject.SetActive(true); // Reveal what the correct answer was
        boss.MoveCloser();

        if (boss.IsTooClose())
        {
            ClearUI(spawnedBossUI);
            ClearUI(spawnedPlayerUI);
            currentState = GameState.GameOver;
            gameOverPanel.SetActive(true);
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
        }
        else
        {
            Invoke("StartNextRound", 1.5f);
        }
    }
    void UpdateScoreboardUI()
    {
        if (scoreboardText != null)
        {
            int remainingToWin = targetWins - successfulMatches;
            int remainingWrongs = totalAllowedFailures - currentFailures;

            // Keeps remaining values locked to 0 minimum so it doesn't display negative numbers on win/loss
            if (remainingToWin < 0) remainingToWin = 0;
            if (remainingWrongs < 0) remainingWrongs = 0;

            // Formats it cleanly into strings across 3 lines
            scoreboardText.text = $"Wins: {successfulMatches} / {targetWins}\n" +
                                  $"Chances Left: {remainingWrongs}";
        }
    }

    void ClearUI(List<GameObject> list)
    {
        foreach (GameObject obj in list) Destroy(obj);
        list.Clear();
    }
}