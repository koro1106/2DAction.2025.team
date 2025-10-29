using UnityEngine;

public class PlayerSolid : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollMultiplier = 360f; // �]�����]���x�̔{��

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ���E�ړ�
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // ��]�i�]����\���j
        if (isGrounded && horizontal != 0)
        {
            float rotationAmount = -horizontal * moveSpeed * rollMultiplier * Time.deltaTime;
            rb.MoveRotation(rb.rotation + rotationAmount);
        }

        // �W�����v
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ɐڒn����
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
        // �ڒn��������Z�b�g
        isGrounded = false;
    }
}
