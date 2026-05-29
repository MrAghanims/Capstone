using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next : MonoBehaviour
{
    public string Nextscene;

    private bool isPaused = false;

    void Update()
    {


    }

    public void GoNext()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(Nextscene);
    }

}