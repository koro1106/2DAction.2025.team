using System.Collections;
using UnityEngine;

public class SlimeMove1 : MonoBehaviour
{
    // ===========================
    //  パラメータ
    // ===========================

    public float speed = 3f;         // 横移動のスピード
    public float jumpForce = 40f;    // ジャンプの強さ
    public float groundCheckDistance = 1.0f; // 地面チェック距離
    public ParticleSystem landingEffect;     // 着地エフェクト（Inspectorで設定）

    // ===========================
    //  内部変数
    // ===========================
    private Rigidbody2D rb;

    private bool isGround = false;   // 今フレームの地面判定
    private bool wasGround = false;  // 前フレームの地面判定
    private bool isJumping = false;  // SoftJump連続防止

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // -------------------------------
        // ■ 横移動
        // -------------------------------
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        // -------------------------------
        // ■ 地面判定（Tag判定）
        // -------------------------------
        wasGround = isGround; // 前フレームを保存

        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y - 0.5f),
            Vector2.down,
            groundCheckDistance
        );

        // Rayが当たっていてTagが"Ground"なら地面と判定
        isGround = (hit.collider != null && hit.collider.CompareTag("Ground"));

        // -------------------------------
        // ■ ★ 地面から離れ → 着地した瞬間 Particle
        // -------------------------------
        if (!wasGround && isGround)
        {
            if (landingEffect != null)
                landingEffect.Play(); // 1回だけ再生
        }

        // -------------------------------
        // ■ ジャンプ処理（地面にいる時だけ）
        // -------------------------------
        if (Input.GetKeyDown(KeyCode.Space) && isGround && !isJumping)
        {
            StartCoroutine(SoftJump());
        }
    }

    // =====================================================
    // ■ 柔らかジャンプ（AddForceを 0.1秒 かけて分散）
    // =====================================================
    private IEnumerator SoftJump()
    {
        isJumping = true;

        float duration = 0.1f;
        float timer = 0f;

        while (timer < duration)
        {
            // AddForce を分散して "ふわっ" とジャンプ
            rb.AddForce(Vector2.up * jumpForce * 0.4f * Time.deltaTime * 60f,
                        ForceMode2D.Force);

            timer += Time.deltaTime;
            yield return null;
        }

        // 誤判定で即ジャンプできないようクールタイム
        yield return new WaitForSeconds(0.1f);

        isJumping = false;
    }

    // =====================================================
    // ■ Sceneビューで Ray を可視化（デバッグ）
    // =====================================================
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(transform.position.x, transform.position.y - 0.5f),
            new Vector3(transform.position.x, transform.position.y - 0.5f - groundCheckDistance)
        );
    }
}
