using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    // 最大HP
    public float maxHP = 100f;

    // 現在のHP
    public float currentHP;

    // HP表示用スライダー
    public Slider hpSlider;

    // 死亡済みかどうか（多重死亡防止）
    private bool isDead = false;

    // リスポーン管理クラス
    public PlayerRespawn respawn;

    // 一度回復したセーブポイントIDを記録
    private HashSet<int> healedSavePoints = new HashSet<int>();

    void Start()
    {
        // 開始時はHP満タン
        currentHP = maxHP;

        // スライダー初期化
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

        // Respawn が未設定なら自動取得
        if (respawn == null)
            respawn = GetComponent<PlayerRespawn>();
    }

    // ダメージを受ける処理
    public void Damage(float damage)
    {
        // すでに死んでいたら無視
        if (isDead) return;

        // HPを減らす
        currentHP -= damage;

        // 0～最大HPに制限
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // スライダー更新
        if (hpSlider != null)
            hpSlider.value = currentHP;

        // HPが0になったら死亡
        if (currentHP <= 0f)
            Die();
    }

    // 死亡処理
    void Die()
    {


        // 死亡処理は1回だけ
        if (isDead) return;

        isDead = true;

        // リスポーン処理を PlayerRespawn に任せる
        if (respawn != null)
            respawn.Die();
        else
            Debug.LogError("PlayerRespawn が設定されていません");
    }

    // リスポーン時に呼ばれるHPリセット
    public void ResetHP()
    {
        currentHP = maxHP;
        isDead = false;

        if (hpSlider != null)
            hpSlider.value = currentHP;
    }

    // セーブポイント到達時の回復処理
    public void TryHealAtSavePoint(int savePointID)
    {
        // すでに回復済みなら何もしない
        if (healedSavePoints.Contains(savePointID))
            return;

        // 初回のみHP回復
        ResetHP();

        healedSavePoints.Add(savePointID);

        Debug.Log($"SavePoint {savePointID}：HP回復（初回のみ）");
    }

    // 死亡時に呼ばれる想定の拡張用メソッド
    public void OnPlayerDead()
    {
        // 今は何もしない
        // （回復済みセーブ情報は保持）
    }
}
