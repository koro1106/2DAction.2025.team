using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 3f;
    private Rigidbody2D rb;
    private bool isGround = false;

    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Debug.Log("ジャンプ");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true; // 地面着地
            Debug.Log("地面ついてるか：" +isGround);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = false; // 地面離脱
            Debug.Log("地面ついてるか：" + isGround);
        }
    }
}