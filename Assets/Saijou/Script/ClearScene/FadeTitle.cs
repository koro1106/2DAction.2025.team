using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// クリア画面でタイトルボタン押したときに
/// フェードしてタイトルに移動クラス
/// </summary>
public class FadeTitle : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup; // 黒ImageのCanvasGroup
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform titleCameraPos; // タイトル画面用カメラ位置
    [SerializeField] float fadeTime = 1.0f;
    [SerializeField] GameObject titileButton; // タイトルボタン
    [SerializeField] GameObject titileUI;     // タイトルUI
    public void OnTitleButton()
    {
        StartCoroutine(FadeSequence());
        Debug.Log("ボタン押された");
    }
    public IEnumerator FadeSequence()
    {
        // フェードアウト(暗く)
        yield return StartCoroutine(Fade(0f, 1f));

        // 画面が真っ暗になったらカメラ移動
        mainCamera.transform.position = titleCameraPos.position;

        // ボタンfalseにしてUItrueにする
        titileButton.SetActive(false);
        titileUI.SetActive(true);

        // フェードイン(明るく)
        yield return StartCoroutine(Fade(1f, 0f));


    }

    public IEnumerator Fade(float start, float end)
    {
        float time = 0f;
        canvasGroup.alpha = start;
        while(time < fadeTime)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, time / fadeTime);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}
