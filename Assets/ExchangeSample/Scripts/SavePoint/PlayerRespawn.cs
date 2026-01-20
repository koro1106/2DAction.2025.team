using System;
using System.Collections;
using UnityEngine;
public class PlayerRespawn : MonoBehaviour
{
    private Vector3 currentSavePoint;

    public ScreenFader screenFader;
    public PlayerHP playerHP;

    // ★追加：Gas用死亡エフェクト
    public GameObject gasDeathParticle;

    void Start()
    {
        currentSavePoint = transform.position;

        if (playerHP == null)
            playerHP = GetComponent<PlayerHP>();
    }

    public void Die()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        // ★追加：死亡時エフェクト（Gasが有効なときだけ）
        if (gasDeathParticle != null)
        {
            // 子オブジェクトに GasPlayer が有効なら Gas死亡
            GasPlayer gas = GetComponentInChildren<GasPlayer>(false);
            if (gas != null && gas.gameObject.activeInHierarchy)
            {
                Instantiate(gasDeathParticle, gas.transform.position, Quaternion.identity);
            }
        }

        // フェードアウト
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeOut());

        // ★復活位置
        transform.position = currentSavePoint;

        // HP全回復
        playerHP.ResetHP();

        // フェードイン
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeIn());
    }
    // ★セーブポイント更新（本実装）
    public void SetSavePoint(Vector3 position)
    {
        currentSavePoint = position;
        Debug.Log("SavePoint 更新: " + position);
    }
}