using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour
{
    public AudioClip typingSound;
    private AudioSource audioSource;
    public GameObject nextSceneButton;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject arry, lola;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextSceneButton.SetActive(false);
        textComponent.text = string.Empty;
        StartDialogue();
        AryySprite();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                audioSource.Play();
                NextLine();
                AryySprite();
            }
            else
            {
                StopAllCoroutines();
                audioSource.Stop();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;

            if (c != ' ')
            {
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            nextSceneButton.SetActive(true);
        }
    }
    void AryySprite()
    {
        if (index % 2 == 0)
        {

            arry.SetActive(true);
            lola.SetActive(false);
        }
        else
        {
            arry.SetActive(false);
            lola.SetActive(true);
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene("GameScene");
    }


}
