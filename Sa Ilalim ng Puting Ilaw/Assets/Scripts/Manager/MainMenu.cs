using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro"); 
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
        SceneManager.LoadScene("LevelSelect"); 
    }
    public void Levels1()
    {
        SceneManager.LoadScene("Day 1"); 
    }
    public void Levels2()
    {
        SceneManager.LoadScene("Day 2"); 
    }
    public void Levels3()
    {
        SceneManager.LoadScene("Day 3"); 
    }
    public void Levels4()
    {
        SceneManager.LoadScene("Day 4"); 
    }
    public void Levels5()
    {
        SceneManager.LoadScene("Day 5"); 
    }
    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
    public void Bestiary()
    {
        SceneManager.LoadScene("Bestiary"); 
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}