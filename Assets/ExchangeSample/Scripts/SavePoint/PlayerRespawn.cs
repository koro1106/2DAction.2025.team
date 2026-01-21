using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // 現在のセーブポイント座標
    private Vector3 currentSavePoint;

    // 画面フェード制御
    public ScreenFader screenFader;

    // PlayerHP 参照
    public PlayerHP playerHP;

    // ガス状態で死んだ時のエフェクト
    public GameObject gasDeathParticle;

    void Start()
    {
        // 初期位置をセーブポイントに設定
        currentSavePoint = transform.position;

        // PlayerHP が未設定なら自動取得
        if (playerHP == null)
            playerHP = GetComponent<PlayerHP>();
    }

    // PlayerHP から呼ばれる死亡処理
    public void Die()
    {
        StartCoroutine(RespawnCoroutine());
    }

    // リスポーン処理本体
    IEnumerator RespawnCoroutine()
    {
        // ---- ガス死亡エフェクト ----
        if (gasDeathParticle != null)
        {
            // 子オブジェクトに GasPlayer があるか確認
            GasPlayer gas = GetComponentInChildren<GasPlayer>(false);

            // ガス状態で有効ならエフェクト生成
            if (gas != null && gas.gameObject.activeInHierarchy)
            {
                Instantiate(
                    gasDeathParticle,
                    gas.transform.position,
                    Quaternion.identity
                );
            }
        }

        // フェードアウト
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeOut());

        // セーブポイント位置に移動（親Player）
        transform.position = currentSavePoint;

        // HP全回復＆死亡解除
        playerHP.ResetHP();

        // フェードイン
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeIn());
    }

    // セーブポイント更新（外部から呼ばれる）
    public void SetSavePoint(Vector3 position)
    {
        currentSavePoint = position;
        Debug.Log("SavePoint 更新: " + position);
    }
}
