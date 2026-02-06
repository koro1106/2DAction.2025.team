using UnityEngine;

namespace ExchangeSample.Scripts
{
    public class PlayerSolid : MonoBehaviour
    {
        // ================================
        // 状態切り替え用（Liquid / Gas）
        // ================================
        [SerializeField] private Character rootCharacter;

        // ================================
        // 移動・ジャンプ設定
        // ================================
        public float moveSpeed = 3f;      // 横移動速度
        public float jumpForce = 20f;     // ジャンプ力
        public float rollSpeed = 480f;    // 回転速度

        // ================================
        // 接地判定用
        // ================================
        [SerializeField] private Transform groundCheck;   // 足元チェック位置
        [SerializeField] private float groundCheckRadius = 0.08f;
        [SerializeField] private LayerMask groundLayer;   // 地面レイヤー

        private Rigidbody2D rb;

        // ================================
        // 状態管理
        // ================================
        private float horizontal;     // 横入力
        private bool jumpRequest;      // ジャンプ入力受付
        private bool isGrounded;       // 接地中か
        private bool hasJumped;        // すでにジャンプしたか（無限ジャンプ防止）

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            // 初期位置を少し浮かせる（埋まり防止）
            Vector3 pos = transform.position;
            pos.y += 0.1f;
            transform.position = pos;

            // 物理安定化
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        }

        // =====================================
        // 入力取得（物理処理はしない）
        // =====================================
        void Update()
        {
            // 横移動入力
            horizontal = Input.GetAxisRaw("Horizontal");

            // ジャンプ入力
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpRequest = true;
            }
        }

        // =====================================
        // 物理処理（必ず FixedUpdate）
        // =====================================
        void FixedUpdate()
        {
            // ---------- 接地判定 ----------
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );

            // ---------- 着地したらジャンプ解禁 ----------
            if (isGrounded && rb.velocity.y <= 0f)
            {
                hasJumped = false;
            }

            // ---------- 横移動 ----------
            rb.velocity = new Vector2(
                horizontal * moveSpeed,
                rb.velocity.y
            );

            // ---------- 回転 ----------
            if (horizontal != 0)
                rb.angularVelocity = -horizontal * rollSpeed;
            else
                rb.angularVelocity = 0;

            // ---------- ジャンプ ----------
            if (jumpRequest && isGrounded && !hasJumped)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                hasJumped = true;   // 空中ではもうジャンプ不可
            }

            // ジャンプ入力リセット
            jumpRequest = false;
        }

        // =====================================
        // 状態変化トリガー
        // =====================================
        private void OnTriggerEnter2D(Collider2D other)
        {
            // 自分自身に変化させない
            if (!other.CompareTag("ToSolid"))
            {
                rootCharacter.ChangeCharacter(
                    other.gameObject.tag,
                    transform.position
                );
            }
        }

        // =====================================
        // Sceneビューで接地判定を表示
        // =====================================
        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null) return;

            Gizmos.color = Color.red;

            Vector3 size = new Vector3(
                groundCheckRadius * 2f, // 横幅
                groundCheckRadius * 2f, // 高さ
                0.01f
            );

            Gizmos.DrawWireCube(groundCheck.position, size);
        }

    }
}
