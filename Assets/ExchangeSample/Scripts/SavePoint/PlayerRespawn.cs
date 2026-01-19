using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentSavePoint;
    public ScreenFader screenFader;
    public KillZone killZone;
    public PlayerHP playerHP;
    void Start()
    {
        currentSavePoint = transform.position;
    }

    public void SetSavePoint(Vector3 newSavePoint)
    {
        currentSavePoint = newSavePoint;
        //   Debug.Log("新しいセーブポイント確保: " + newSavePoint);

        if (playerHP == null)
            playerHP = GetComponent<PlayerHP>();
    }

    public void Die()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        // 画面暗転
        yield return StartCoroutine(screenFader.FadeOut());

        // リスポーン
        Debug.Log("今のせーぶぽいんと" + currentSavePoint);
        transform.position = currentSavePoint;

        // HP回復
        if (playerHP == null)
        {
            Debug.Log("HP回復");
            playerHP.ResetHP();
        }
        else
        {
            Debug.LogError("PlayerHP が設定されていません");
        }

        // 画面明転
        yield return StartCoroutine(screenFader.FadeIn());
    }
}