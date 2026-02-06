using UnityEngine;

public class LiquidShooter : MonoBehaviour
{
    // ================================
    // 発射設定
    // ================================
    public GameObject waterBallPrefab; // 水球Prefab
    public Transform shootPoint;        // 発射位置
    public Transform aimPoint;          // 狙い位置

    // ================================
    // クロスヘアUI
    // ================================
    public GameObject crossHair;

    // ================================
    // 連射制御
    // ================================
    public float shootInterval = 0.5f;

    // ================================
    // HP消費
    // ================================
    public float shootHpCost = 5f; // ★撃つたびに減るHP

    // ================================
    // スタート演出管理
    // ================================
    public StartPerformance startPerformance;

    private float nextShootTime;

    // HP管理
    private PlayerHP playerHP;

    // --------------------------------
    // Liquid状態になった瞬間
    // --------------------------------
    void OnEnable()
    {
        if (crossHair != null)
            crossHair.SetActive(true);

        // 親から PlayerHP を取得
        playerHP = GetComponentInParent<PlayerHP>();
    }

    // --------------------------------
    // Liquid状態解除
    // --------------------------------
    void OnDisable()
    {
        if (crossHair != null)
            crossHair.SetActive(false);
    }

    void Update()
    {
        // スタート演出中は撃てない
        if (!startPerformance.preformanceFinished)
            return;

        // 左クリック ＆ クールタイム
        if (Input.GetMouseButtonDown(0) && Time.time >= nextShootTime)
        {
            // HPが足りないなら撃てない
            if (playerHP != null && playerHP.currentHP <= shootHpCost)
                return;

            Shoot();

            // ★HP消費
            if (playerHP != null)
                playerHP.Damage(shootHpCost);

            nextShootTime = Time.time + shootInterval;
        }
    }

    // ================================
    // 水球発射
    // ================================
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

        float gravity = Mathf.Abs(
            Physics2D.gravity.y * rb.gravityScale
        );

        float height = target.y - start.y;
        if (height < 0.5f)
            height = 0.5f;

        float velocityY = Mathf.Sqrt(2f * gravity * height);
        float timeToApex = velocityY / gravity;
        float velocityX = (target.x - start.x) / timeToApex;

        rb.velocity = new Vector2(velocityX, velocityY);
    }
}
