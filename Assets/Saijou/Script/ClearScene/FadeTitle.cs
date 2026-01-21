using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
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

    [SerializeField] private Transform startPoint; // スタート位置
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayableDirector clearA; // タイムライン
    [SerializeField] private PlayableDirector clearB;
    [SerializeField] private PlayableDirector clearC;
    [SerializeField] private PlayableDirector clearD;

    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private Slider slider;


    public void OnTitleButton()
    {
        playerHP.currentHP = playerHP.maxHP; // HP100に
        slider.value = 100;
        StartCoroutine(FadeSequence());
        // タイムライン停止する
        clearA.Stop();
        clearB.Stop();
        clearC.Stop();
        clearD.Stop();
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

        // スタート地点にプレイヤーワープ
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.transform.position = startPoint.position;

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
