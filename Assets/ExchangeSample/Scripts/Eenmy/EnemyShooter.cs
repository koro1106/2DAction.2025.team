using UnityEngine;

public class EnemyShooterAnimLoop : MonoBehaviour
{
    [Header("アニメーション用画像")]
    public Sprite[] sprites;          // 1～10の画像を順番に設定（Inspectorで）
    public float frameTime = 0.1f;    // フレーム切替間隔（秒）

    [Header("弾の設定")]
    public GameObject bulletPrefab;   // 弾のPrefab
    public Transform firePoint;       // 弾の発射位置
    public Transform player;          // プレイヤーのTransform
    public float speedX = 6f;         // 弾の横方向速度

    private SpriteRenderer sr;        // 敵のSpriteRenderer
    private int currentFrame = 0;     // 現在のアニメフレーム
    private float frameTimer = 0f;    // フレーム切替用タイマー
    private bool hasShot = false;     // 5フレーム目で弾を発射済みか

    void Start()
    {
        // SpriteRendererを取得
        sr = GetComponent<SpriteRenderer>();

        // 最初の画像を設定
        if (sprites.Length > 0)
            sr.sprite = sprites[0];
    }

    void Update()
    {
        // アニメーションを常に実行
        AnimateAndShoot();
    }

    // アニメーションと弾発射を管理
    void AnimateAndShoot()
    {
        // フレームタイマーを加算
        frameTimer += Time.deltaTime;

        // frameTimeごとに次のフレームへ
        if (frameTimer >= frameTime)
        {
            frameTimer = 0f;        // タイマーリセット
            currentFrame++;         // 次のフレームに進む

            // 配列内にフレームがある場合
            if (currentFrame < sprites.Length)
            {
                sr.sprite = sprites[currentFrame];

                // 5フレーム目（index=4）で弾を発射
                if (currentFrame == 4 && !hasShot)
                {
                    Shoot();          // 弾発射
                    hasShot = true;   // 二重発射防止
                }
            }
            else
            {
                // アニメ終了時にループ
                currentFrame = 0;
                hasShot = false;    // 弾発射フラグもリセット
                sr.sprite = sprites[0];
            }
        }
    }

    // 弾を発射する処理
    void Shoot()
    {
        if (firePoint == null || bulletPrefab == null || player == null) return;

        // プレイヤー方向の判定（右なら+1、左なら-1）
        float dir = Mathf.Sign(player.position.x - firePoint.position.x);

        // 横速度
        float vx = dir * speedX;

        // プレイヤーのX座標に到達するまでの時間
        float distanceX = player.position.x - firePoint.position.x;
        float time = Mathf.Abs(distanceX / vx);

        // Unityの重力
        float gravity = Physics2D.gravity.y;

        // 放物線で到達するためのY速度
        float vy = (player.position.y - firePoint.position.y) / time - 0.5f * gravity * time;

        // 弾生成
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // 弾に初速を与える
        bullet.GetComponent<Projectile>().Shoot(new Vector2(vx, vy));
    }
}
