using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;

    public Slider hpSlider;

    private bool isDead = false;

    public PlayerRespawn respawn;

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
        // ★変更点：死亡は1回だけ
        if (isDead) return;

        isDead = true;

        // ★変更点：Respawnにだけ任せる
        if (respawn != null)
            respawn.Die();
        else
            Debug.LogError("PlayerRespawn が設定されていません");
    }

    public void ResetHP()
    {
        // ★変更点：復活用
        currentHP = maxHP;
        isDead = false;

        if (hpSlider != null)
            hpSlider.value = currentHP;
    }
}