using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition4 : MonoBehaviour
{
    public GameObject fadePanel;

    public Image fadeImage;

    public TextMeshProUGUI transitionText;

    public string nextSceneName;

    public float fadeSpeed = 1f;

    public void StartTransition4(string message)
    {
        StartCoroutine(TransitionCoroutine(message));
    }

    IEnumerator TransitionCoroutine(string message)
    {
        fadePanel.SetActive(true);

        transitionText.text = message;

        // GET COLORS
        Color imageColor = fadeImage.color;
        Color textColor = transitionText.color;

        // START TRANSPARENT
        imageColor.a = 0f;
        textColor.a = 0f;

        fadeImage.color = imageColor;
        transitionText.color = textColor;

        // START BLACK SCREEN FADE FIRST
        while (imageColor.a < 1f)
        {
            imageColor.a += Time.deltaTime * fadeSpeed;

            fadeImage.color = imageColor;

            yield return null;
        }

        // WAIT BEFORE TEXT APPEARS
        yield return new WaitForSeconds(1f);

        // NOW FADE TEXT IN
        while (textColor.a < 1f)
        {
            textColor.a += Time.deltaTime * fadeSpeed;

            transitionText.color = textColor;

            yield return null;
        }

        // WAIT BEFORE LOADING NEXT SCENE
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(nextSceneName);
    }
}