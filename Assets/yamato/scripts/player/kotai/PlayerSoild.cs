using UnityEngine;

public class PlayerSolid : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 20f;
    public float rollSpeed = 480f;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 地面から少し浮かせる
        Vector3 pos = transform.position;
        pos.y += 0.1f;
        transform.position = pos;

        // 物理設定
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // 横移動
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // 常に回転（接地していなくても）
        if (horizontal != 0)
            rb.angularVelocity = -horizontal * rollSpeed;
        else
            rb.angularVelocity = 0;

        // ジャンプ
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
