using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("弾のダメージ")]
    public float damage = 5f;  // 弾が与えるダメージ量

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 弾に速度を与える
    public void Shoot(Vector2 velocity)
    {
        if (rb != null)
        {
            rb.velocity = velocity;
        }
    }

    // 衝突判定（ColliderがIsTrigger OFFの場合）
    void OnCollisionEnter2D(Collision2D collision)
    {
        // -----------------------------
        // 1. プレイヤーに当たった場合
        // -----------------------------
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("playerに当たった");
            // PlayerHPを取得（親も含める）
            PlayerHP playerHP = collision.collider.GetComponentInParent<PlayerHP>();
            if (playerHP != null)
            {
                playerHP.Damage(damage);   // HPを減らす
                Debug.Log("Playerにダメージ: " + damage);
            }
            else
            {
                Debug.LogWarning("PlayerHPが見つかりません！");
            }

            // 弾を消す
            Destroy(gameObject);
        }

        // -----------------------------
        // 2. 地面に当たった場合
        // -----------------------------
        else if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("地面に当たった");
            // 地面に当たったら弾だけ消す
            Destroy(gameObject);
        }
    }

    // トリガー判定（ColliderがIsTrigger ONの場合）
    void OnTriggerEnter2D(Collider2D other)
    {
        // -----------------------------
        // 1. プレイヤーに当たった場合
        // -----------------------------
        if (other.CompareTag("Player"))
        {
            Debug.Log("playerに当たった");
            PlayerHP playerHP = other.GetComponentInParent<PlayerHP>();
            if (playerHP != null)
            {
                playerHP.Damage(damage);
                Debug.Log("Playerにダメージ: " + damage);
            }
            else
            {
                Debug.LogWarning("PlayerHPが見つかりません！");
            }

            Destroy(gameObject);
        }

        // -----------------------------
        // 2. 地面に当たった場合
        // -----------------------------
        else if (other.CompareTag("Ground"))
        {
            Debug.Log("地面に当たった");
            Destroy(gameObject);
        }
    }
}
