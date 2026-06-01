using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [Header("Fade")]
    public Image fadeImage;
    public float fadeDuration = 2f;

    [Header("Credits")]
    public GameObject endCreditsPanel;
    public GameObject fadePanel;
    [Header("Scene")]
    public string mainMenuScene = "MainMenu";

    private bool creditsShowing = false;

    void Start()
    {
        // Hide credits
        endCreditsPanel.SetActive(false);

        // Make fade image transparent
        //Color c = fadeImage.color;
        //c.a = 0f;
        //fadeImage.color = c;
    }

    // Call from your ending button
    public void ShowEnding()
    {
        fadePanel.SetActive(true);
        StartCoroutine(FadeToCredits());

    }

    IEnumerator FadeToCredits()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            Color c = fadeImage.color;
            c.a = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = c;

            yield return null;
        }

        // Ensure fully black
        Color finalColor = fadeImage.color;
        finalColor.a = 1f;
        fadeImage.color = finalColor;

        // Show credits AFTER fade completes
        endCreditsPanel.SetActive(true);

        creditsShowing = true;
    }

    void Update()
    {
        // Click anywhere after credits are shown
        if (creditsShowing && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}