using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    void Start()
    {
        // 念のため透明からスタート
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator FadeOut()
    {
        Color color = fadeImage.color;

        while (color.a < 1)
        {
            color.a += Time.unscaledDeltaTime * fadeSpeed;
            color.a = Mathf.Clamp01(color.a);
            fadeImage.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {


        StopAllCoroutines();

        Color color = fadeImage.color;

        while (color.a > 0.01f)
        {
            color.a -= Time.unscaledDeltaTime * fadeSpeed;
            fadeImage.color = color;

            // 強制的に0
            fadeImage.color = new Color(
                fadeImage.color.r,
                fadeImage.color.g,
                fadeImage.color.b,
                0f
            );
            yield return null;
        }


        Debug.Log("明るくなった");

    }
}