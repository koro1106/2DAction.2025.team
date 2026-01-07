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
            color.a += Time.deltaTime * fadeSpeed;
            color.a = Mathf.Clamp01(color.a);
            fadeImage.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        Color color = fadeImage.color;

        while (color.a > 0)
        {
            color.a -= Time.deltaTime * fadeSpeed;
            color.a = Mathf.Clamp01(color.a);
            fadeImage.color = color;
            yield return null;
        }

        // 完全に透明に戻す
        fadeImage.color = new Color(0, 0, 0, 0);
    }
}
