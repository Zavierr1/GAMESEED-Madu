using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    public string nextSceneName;

    private void Start()
    {
        // Fade in dari hitam saat scene dimulai
        StartCoroutine(FadeIn());
    }

    public void StartGameWithFade()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    private IEnumerator FadeIn()
    {
        float t = 1;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = 0 - (1 / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1;
        fadeCanvasGroup.interactable = false;
        fadeCanvasGroup.blocksRaycasts = false;
    }

    private IEnumerator FadeOutAndLoad()
    {
        fadeCanvasGroup.interactable = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1;

        // Ganti scene setelah fade selesai
        SceneManager.LoadScene(nextSceneName);
    }
}
