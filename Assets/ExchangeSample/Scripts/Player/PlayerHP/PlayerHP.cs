using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;

    public Slider hpSlider;

    private bool isDead = false;

    public PlayerRespawn respawn;

    private HashSet<int> healedSavePoints = new HashSet<int>();

    void Start()
    {
        currentHP = maxHP;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

        // ★変更点：自動取得
        if (respawn == null)
            respawn = GetComponent<PlayerRespawn>();
    }

    public void Damage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (hpSlider != null)
            hpSlider.value = currentHP;

        if (currentHP <= 0f)
            Die();
    }

    void Die()
    {
        //変更点：死亡は1回だけ
        if (isDead) return;

        isDead = true;

        // 変更点：Respawnにだけ任せる
        if (respawn != null)
            respawn.Die();
        else
            Debug.LogError("PlayerRespawn が設定されていません");
    }

    public void ResetHP()
    {
        // 変更点：復活用
        currentHP = maxHP;
        isDead = false;

        if (hpSlider != null)
            hpSlider.value = currentHP;
    }

    // SavePoint 到達時に呼ばれる
    public void TryHealAtSavePoint(int savePointID)
    {
        // すでにこの SavePoint で回復済みなら何もしない
        if (healedSavePoints.Contains(savePointID))
            return;

        // 初回のみ回復
        ResetHP();

        healedSavePoints.Add(savePointID);

        Debug.Log($"SavePoint {savePointID}：HP回復（初回のみ）");
    }

    // ----------------------------
    // 死亡時に呼ばれる処理
    // ----------------------------
    public void OnPlayerDead()
    {
        //  死んでも回復済み情報はリセットしない
        // 「死ぬまで1回」が守られる
    }
}