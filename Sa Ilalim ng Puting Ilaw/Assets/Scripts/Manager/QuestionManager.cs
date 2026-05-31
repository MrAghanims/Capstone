using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ColorPuzzleManager;

public class QuestionManager : MonoBehaviour
{
    public Image fadeImage;
    public TMP_Text transitionText;
    public string nextSceneName;
    public GameObject instructionPanel;
    public TMP_Text questionText;
    public TMP_Text resultText;
    public TMP_Text CurrentLoop;
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public int correctAnswer;
    }

    public Question[] questions;

    private int currentQuestion = 0;
    private int playerAnswer = 0;

    void Start()
    {
        questionText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        CurrentLoop.gameObject.SetActive(false);
        Time.timeScale = 0f;
        if (instructionPanel != null) instructionPanel.SetActive(true);
        resultText.alpha = 0f;
        ShowQuestion();
    }
    public void StartGame()
    {
        questionText.gameObject.SetActive(true);
        resultText.gameObject.SetActive(true);
        CurrentLoop.gameObject.SetActive(true);
        Time.timeScale = 1f;
        if (instructionPanel != null)
        {
            instructionPanel.SetActive(false);
        }
       
    }


    public void AddLoop()
    {
        playerAnswer++;
        CurrentLoop.text = "Loops: " + playerAnswer.ToString();

        Debug.Log("Current Answer: " + playerAnswer);
    }

    public void SubmitAnswer()
    {
        if (playerAnswer == questions[currentQuestion].correctAnswer)
        {
            resultText.color = Color.green;
            resultText.text = "Correct!";
            StopAllCoroutines();
            StartCoroutine(FadeResultText());

            currentQuestion++;

            if (currentQuestion >= questions.Length)
            {
                StartCoroutine(TransitionToNextScene());
            }
            else
            {
                ShowQuestion();
            }
        }
        else
        {
            resultText.color = Color.red;
            resultText.text = "Wrong!";
            StopAllCoroutines();
            StartCoroutine(FadeResultText());
        }

        playerAnswer = 0;
        CurrentLoop.text = "Loops: 0";
    }

    void ShowQuestion()
    {
        questionText.text = questions[currentQuestion].questionText;
    }
    IEnumerator FadeResultText()
    {
        resultText.alpha = 1f;

        yield return new WaitForSeconds(1f);

        float fadeTime = 2f;
        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            resultText.alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);

            yield return null;
        }

        resultText.alpha = 0f;
    }

    IEnumerator TransitionToNextScene()
    {
        FindObjectOfType<SceneTransition2>()
   .StartTransition2("You answered everything correctly!");

        Color fadeColor = fadeImage.color;
        fadeColor.a = 0f;
        fadeImage.color = fadeColor;

        float duration = 2f;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            fadeColor.a = Mathf.Lerp(0f, 1f, timer / duration);
            fadeImage.color = fadeColor;

            yield return null;
        }

        fadeColor.a = 1f;
        fadeImage.color = fadeColor;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(nextSceneName);
    }
}