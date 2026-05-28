using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource normalMusic;
    public AudioSource chaseMusic;


    public float fadeSpeed = 1.5f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        normalMusic.volume = 1f;
        chaseMusic.volume = 0f;

        normalMusic.Play();
        chaseMusic.Play();
    }

    public void StartChase()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeMusic(0f, 1f));
    }

    public void StopChase()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeMusic(1f, 0f));
    }

    IEnumerator FadeMusic(float normalTarget, float chaseTarget)
    {
        while (Mathf.Abs(normalMusic.volume - normalTarget) > 0.01f ||
               Mathf.Abs(chaseMusic.volume - chaseTarget) > 0.01f)
        {
            normalMusic.volume = Mathf.Lerp(normalMusic.volume, normalTarget, fadeSpeed * Time.deltaTime);
            chaseMusic.volume = Mathf.Lerp(chaseMusic.volume, chaseTarget, fadeSpeed * Time.deltaTime);

            yield return null;
        }

        normalMusic.volume = normalTarget;
        chaseMusic.volume = chaseTarget;
    }
}