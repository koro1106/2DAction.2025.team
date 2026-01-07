using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentSavePoint;
    public ScreenFader screenFader;

    void Start()
    {
        currentSavePoint = transform.position;
    }

    public void SetSavePoint(Vector3 newSavePoint)
    {
        currentSavePoint = newSavePoint;
        Debug.Log("新しいセーブポイント確保: " + newSavePoint);
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
        transform.position = currentSavePoint;

        // 画面明転
        yield return StartCoroutine(screenFader.FadeIn());
    }
}
