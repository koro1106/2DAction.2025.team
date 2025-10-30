using UnityEngine;

public class PlayerSolid : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollSpeed = 180f; // Box�p�͍T����

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // �n�ʂ��班����������
        Vector3 pos = transform.position;
        pos.y += 0.1f; // 0.1m��ɂ��炷
        transform.position = pos;

        // �����ݒ�
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        // ���ړ�
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // �[����]
        if (isGrounded && horizontal != 0)
            rb.angularVelocity = -horizontal * rollSpeed;
        else
            rb.angularVelocity = 0;

        // �W�����v
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
