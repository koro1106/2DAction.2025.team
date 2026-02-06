using UnityEngine;
using ExchangeSample.Scripts;   // PlayerRespawn / PlayerHP 用

public class SavePoint : MonoBehaviour
{
    [Header("SavePoint ID（ユニーク）")]
    public int savePointID;

    public AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player 以外は無視
        if (!collision.CompareTag("Player"))
            return;

        // Player 側のコンポーネント取得
        PlayerRespawn respawn =
            collision.GetComponentInParent<PlayerRespawn>();

        PlayerHP hp =
            collision.GetComponentInParent<PlayerHP>();

        // どちらか無ければ処理しない
        if (respawn == null || hp == null)
            return;

        // ================================
        // セーブ位置は必ず更新する
        // ================================
        respawn.SetSavePoint(transform.position);

        // ================================
        // savePointID が 1の場合
        // → 回復しない＆音も鳴らさない
        // ================================
        if (savePointID == 1)
        {
            return;
        }

        // ================================
        // savePointID が 3の場合
        // → 回復しない＆音も鳴らさない
        // ================================
        if (savePointID == 3)
        {
            return;
        }

        // ================================
        // それ以外のセーブポイント
        // ================================

        // SE 再生
        if (audioManager != null)
        {
            audioManager.audioSource.PlayOneShot(
                audioManager.savePoint
            );
        }

        // 回復処理（Player 側で管理）
        hp.TryHealAtSavePoint(savePointID);
    }
}
