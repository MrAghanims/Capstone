using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro"); // Make sure this scene exists
    }

    public void OpenOptions()
    {
        Debug.Log("Options clicked!");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
    public void Levels()
    {
        SceneManager.LoadScene("LevelSelect"); // Make sure this scene exists
    }
    public void Levels1()
    {
        SceneManager.LoadScene("Day 1"); // Make sure this scene exists
    }
    public void Levels2()
    {
        SceneManager.LoadScene("Day 2"); // Make sure this scene exists
    }
    public void Levels3()
    {
        SceneManager.LoadScene("Day 3"); // Make sure this scene exists
    }
    public void Levels4()
    {
        SceneManager.LoadScene("Day 4"); // Make sure this scene exists
    }
    public void Levels5()
    {
        SceneManager.LoadScene("Day 5"); // Make sure this scene exists
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu"); // Make sure this scene exists
    }
}