using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    public string nextSceneName;

    private bool waitingForClick = false;

    public void StartFade()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;

            color.a = Mathf.Lerp(0, 1, time / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = 1;
        fadeImage.color = color;

        waitingForClick = true;
    }

    void Update()
    {
        if (waitingForClick && Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}