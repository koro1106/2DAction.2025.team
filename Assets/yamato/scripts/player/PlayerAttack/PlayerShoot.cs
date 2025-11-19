using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.5f;

    public float maxInitialSpeed = 20f; // ★これ以上速くしない

    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer > shootCooldown)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        Vector2 start = firePoint.position;
        Vector2 peak = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float g = Mathf.Abs(Physics2D.gravity.y);

        Vector2 diff = peak - start;
        float dx = diff.x;
        float dy = diff.y;

        // 頂点が発射位置以下なら、最低でも少し上に補正
        if (dy <= 0) dy = 0.1f;

        // 頂点に到達するための縦方向の初速度
        float vy0 = Mathf.Sqrt(2 * g * dy);

        // 頂点に到達するまでの時間
        float t = vy0 / g;

        // X方向の初速度（頂点のXに合わせる）
        float vx0 = dx / t;

        // 合成した初速度
        Vector2 v = new Vector2(vx0, vy0);

        // ★速度が速すぎる場合クランプ
        if (v.magnitude > maxInitialSpeed)
        {
            v = v.normalized * maxInitialSpeed;
        }

        // 弾生成
        GameObject bullet = Instantiate(bulletPrefab, start, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetVelocity(v);
    }
}
