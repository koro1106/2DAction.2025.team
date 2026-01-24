using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [Header("ジャンプの強さ")] public float jumpForce= 3f;
    [Header("ジャンプ間隔")] public float jumpInterval = 2f;
    [Header("プレイヤーへ跳ぶ強さ")] public float horizontalPower = 2f;
    public Transform player;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGround = false; // 地面判定
    private float timer = 0f;

    public AudioManager audioManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(isGround && timer >= jumpInterval)
        {
            Jump();
        }

        // Playerが左側にいるかどうかで画像を反転
        if (player.position.x < transform.position.x)
        {
            sr.flipX = true; // 左側にいるから反転
        }
        else
        {
            sr.flipX = false; // 右側にいるから正方向
        }
    }
    void Jump()
    {
        // 画面に見えたら移動開始
        if (sr.isVisible)
        {
            audioManager.audioSource.PlayOneShot(audioManager.dolphin);

            timer = 0f;
            isGround = false;

            // プレイヤーの方向に向く
            float dir = Mathf.Sign(player.position.x - transform.position.x);

            // 水平方向の移動
            rb.velocity = new Vector2(dir * horizontalPower, jumpForce);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
        
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }
    }
}
