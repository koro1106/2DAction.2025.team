using UnityEngine;

public class LiquidShooter : MonoBehaviour
{
    public GameObject waterBallPrefab;
    public Transform shootPoint;
    public Transform aimPoint; 

    public float shootInterval = 0.5f;
    public float apexTime = 0.6f; // 頂点に到達する時間

    public StartPerformance startPerformance;
    float timer;

    float nextShootTime;

    void Update()
    {
        if (!startPerformance.preformanceFinished) return;
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        Vector2 start = shootPoint.position;
        Vector2 target = aimPoint.position;

        GameObject ball = Instantiate(
            waterBallPrefab,
            start,
            Quaternion.identity
        );

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);

        // 高さ差（頂点）
        float height = target.y - start.y;

        // 下にあるときの保険
        if (height < 0.5f)
            height = 0.5f;

        // Y初速（高さから決める）
        float velocityY = Mathf.Sqrt(2f * gravity * height);

        // 頂点に到達する時間
        float timeToApex = velocityY / gravity;

        // X初速（頂点時に X が合う）
        float velocityX = (target.x - start.x) / timeToApex;

        rb.velocity = new Vector2(velocityX, velocityY);
    }

}
