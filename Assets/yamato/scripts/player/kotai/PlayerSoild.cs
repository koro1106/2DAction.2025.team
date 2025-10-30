using UnityEngine;

public class PlayerSolid : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollSpeed = 180f; // BoxópÇÕçTÇ¶Çﬂ

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // ínñ Ç©ÇÁè≠ÇµïÇÇ©ÇπÇÈ
        Vector3 pos = transform.position;
        pos.y += 0.1f; // 0.1mè„Ç…Ç∏ÇÁÇ∑
        transform.position = pos;

        // ï®óùê›íË
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // â°à⁄ìÆ
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // ã[éóâÒì]
        if (isGrounded && horizontal != 0)
            rb.angularVelocity = -horizontal * rollSpeed;
        else
            rb.angularVelocity = 0;

        // ÉWÉÉÉìÉv
        if (isGrounded && Input.GetButtonDown("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
