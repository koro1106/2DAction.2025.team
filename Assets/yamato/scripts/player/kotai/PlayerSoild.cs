using UnityEngine;

public class PlayerSolid : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float rollMultiplier = 360f; // 転がる回転速度の倍率

    private Rigidbody2D rb;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 左右移動
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // 回転（転がる表現）
        if (isGrounded && horizontal != 0)
        {
            float rotationAmount = -horizontal * moveSpeed * rollMultiplier * Time.deltaTime;
            rb.MoveRotation(rb.rotation + rotationAmount);
        }

        // ジャンプ
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 足元に接地判定
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
        // 接地判定をリセット
        isGrounded = false;
    }
}
