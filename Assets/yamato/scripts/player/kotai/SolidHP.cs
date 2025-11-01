using UnityEngine;

public class SolidHP : MonoBehaviour
{
    [Header("HP設定")]
    public float currentHP;
    public float maxHP = 100f;

    [Header("ダメージ設定")]
    //public float damagePerSecond = 1f; // 固体でも少しずつ減る or 任意

    private bool isDead = false;

    void Start()
    {
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    void Update()
    {
        if (isDead) return;

        // 固体も少しずつ減らしたいなら（気体と同じ処理）
      //  currentHP -= damagePerSecond * Time.deltaTime;
        currentHP = Mathf.Max(currentHP, 0f);

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // 固体死亡時の処理（必要なら）
        Destroy(gameObject, 1f);
    }
}
