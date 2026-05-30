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
            resultText.text = "Correct!";

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
            resultText.text = "Wrong!";
        }

        playerAnswer = 0;
    }

    void ShowQuestion()
    {
        questionText.text = questions[currentQuestion].questionText;
    }
}