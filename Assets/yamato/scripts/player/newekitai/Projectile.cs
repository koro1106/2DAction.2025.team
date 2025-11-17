using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 3f;
    public float damage = 1f;
    public bool rotateToVelocity = true;

    Rigidbody2D rb;
    Vector2 dir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 発射時に呼ぶ（direction は normalized 推奨）
    public void Init(Vector2 direction, float speedOverride = -1f)
    {
        dir = direction.normalized;
        float s = speedOverride > 0f ? speedOverride : speed;
        rb.velocity = dir * s;

        if (rotateToVelocity)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rb.SetRotation(angle);
        }

        if (lifeTime > 0f)
            Destroy(gameObject, lifeTime);
    }

    // 物理ベースで他と当たる場合は OnTriggerEnter2D / OnCollisionEnter2D を使う
    void OnTriggerEnter2D(Collider2D other)
    {
        // 発射したプレイヤー自身を無視したいならタグやレイヤーで判定
        if (other.CompareTag("Enemy"))
        {
            // 仮のヒット処理（Enemy スクリプトにダメージ送る例）
           //var e = other.GetComponent<Enemy>();
          //  if (e != null) e.TakeDamage(damage);

            // 衝突エフェクトあれば Instantiate して消す
            Destroy(gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    // 速度を途中で変えたい場合の API
    public void SetSpeed(float newSpeed)
    {
        rb.velocity = dir * newSpeed;
    }
}
