using UnityEngine;
using ExchangeSample.Scripts;   // ★追加

public class SavePoint : MonoBehaviour
{
    [Header("SavePoint ID（ユニーク）")]
    public int savePointID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerRespawn respawn =
            collision.GetComponentInParent<PlayerRespawn>();
        PlayerHP hp =
            collision.GetComponentInParent<PlayerHP>();

        if (respawn == null || hp == null) return;

        // セーブ位置は毎回更新
        respawn.SetSavePoint(transform.position);

        // 回復判定は Player 側に任せる
        hp.TryHealAtSavePoint(savePointID);
    }
}