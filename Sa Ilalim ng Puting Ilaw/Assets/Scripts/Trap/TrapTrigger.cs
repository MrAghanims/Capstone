using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            TriggerGameOver();
        }
        else if (collision.CompareTag("Monster"))
        {
            TriggerWin();
        }
    }

    void TriggerGameOver()
    {
        triggered = true;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f; // pause game
    }

    void TriggerWin()
    {
        triggered = true;

        winPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}