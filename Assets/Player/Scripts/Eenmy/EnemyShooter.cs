using UnityEngine;

public class EnemyShooterAnimLoop : MonoBehaviour
{
    // ===============================
    // アニメーション設定
    // ===============================

    [Header("アニメーション用画像")]
    public Sprite[] sprites;        // アニメ用スプライト（Inspectorで順番に設定）
    public float frameTime = 0.1f;  // 1フレームあたりの表示時間（秒）

    // ===============================
    // 弾の設定
    // ===============================

    [Header("弾の設定")]
    public GameObject bulletPrefab; // 発射する弾のPrefab
    public Transform firePoint;     // 弾の発射位置

    // 弾の固定初速
    // X:-5 → 左方向
    // Y:+1 → 少し上方向
    public Vector2 bulletVelocity = new Vector2(-5f, 1f);

    // ===============================
    // 内部変数
    // ===============================

    private SpriteRenderer sr;      // SpriteRenderer
    private int currentFrame = 0;   // 現在のフレーム番号
    private float frameTimer = 0f;  // フレーム切替用タイマー
    private bool hasShot = false;   // 1ループ中に弾を撃ったかどうか

    // ===============================
    // 初期化
    // ===============================

    void Start()
    {
        // SpriteRendererを取得
        sr = GetComponent<SpriteRenderer>();

        // 最初のスプライトを設定
        if (sprites.Length > 0)
        {
            sr.sprite = sprites[0];
        }
    }

    // ===============================
    // 毎フレーム呼ばれる
    // ===============================

    void Update()
    {
        // アニメーションと弾発射を管理
        AnimateAndShoot();
    }

    // ===============================
    // アニメーション処理
    // ===============================

    void AnimateAndShoot()
    {
        // 経過時間を加算
        frameTimer += Time.deltaTime;

        // 指定時間を超えたら次のフレームへ
        if (frameTimer >= frameTime)
        {
            frameTimer = 0f;
            currentFrame++;

            // スプライト配列の範囲内なら表示更新
            if (currentFrame < sprites.Length)
            {
                sr.sprite = sprites[currentFrame];

                // 5フレーム目（index = 4）で弾を撃つ
                if (currentFrame == 4 && !hasShot)
                {
                    Shoot();
                    hasShot = true; // 二重発射防止
                }
            }
            else
            {
                // 最後まで行ったら最初に戻す（ループ）
                currentFrame = 0;
                hasShot = false;
                sr.sprite = sprites[0];
            }
        }
    }

    // ===============================
    // 弾を発射する処理
    // ===============================

    void Shoot()
    {
        // 必要なものがなければ何もしない
        if (firePoint == null || bulletPrefab == null) return;

        // 弾を生成
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        // Projectileスクリプトを取得
        Projectile proj = bullet.GetComponent<Projectile>();
        if (proj != null)
        {
            // 固定方向（左-5、上+1）で発射
            proj.Shoot(bulletVelocity);
        }
    }
}