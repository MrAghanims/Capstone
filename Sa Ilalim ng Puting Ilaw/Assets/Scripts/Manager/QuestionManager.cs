using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text resultText;
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
        resultText.alpha = 0f;
        ShowQuestion();
    }

    public void AddLoop()
    {
        playerAnswer++;

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
                questionText.text = "You escaped.";
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
}