using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public RectTransform crosshairUI;

    public float shootCooldown = 0.5f;
    public float gravityScale = 1f;     // 重力の強さ（上下に動く）
    public float maxInitialSpeed = 20f; // 速度上限

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && timer > shootCooldown)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        Vector2 start = firePoint.position;

        Vector3 screenPos = crosshairUI.position;
        Vector2 peak = Camera.main.ScreenToWorldPoint(screenPos);

        float g = Mathf.Abs(Physics2D.gravity.y);

        // 頂点の高さが発射点より下でもOK
        float dy = Mathf.Abs(peak.y - start.y);

        // Y方向の初速度
        float vy0 = Mathf.Sqrt(2f * g * dy);

        // 頂点がプレイヤーより上なら上向き
        // 下なら下向き
        float dirY = (peak.y >= start.y) ? 1f : -1f;
        vy0 *= dirY;

        // 頂点までの時間
        float t = vy0 / g;

        // X方向の初速度
        float dx = peak.x - start.x;
        float vx0 = dx / t;

        Vector2 v = new Vector2(vx0, vy0);

        // 速度上限
        if (v.magnitude > maxInitialSpeed)
            v = v.normalized * maxInitialSpeed;

        // 弾生成
        GameObject bullet = Instantiate(bulletPrefab, start, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = v;
        rb.gravityScale = 1f; // 弾専用で重力
    }

}
