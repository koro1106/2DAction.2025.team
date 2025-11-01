using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlayerController : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveSpeed = 5f;        // 水平加速力
    public float floatForce = 3f;       // 上昇力
    public float maxFloatSpeed = 4f;    // 上昇の最高速度
    public float drag = 1.5f;           // 空気抵抗（ふわふわ感）

    public float descendForce = 3f;     // 下降力
    public float maxDescendSpeed = 4f;  // 下降の最高速度

    public float horizontalMaxSpeed = 5f; // 左右最大速度

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = 0f; // dragは自作で実装
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveY = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveY = -1f;

        moveInput = new Vector2(moveX, moveY);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        // -----------------------------
        // 左右
        // -----------------------------
        if (moveInput.x > 0)
            velocity.x += moveSpeed * Time.fixedDeltaTime;
        else if (moveInput.x < 0)
            velocity.x -= moveSpeed * Time.fixedDeltaTime;
        else
            velocity.x = Mathf.Lerp(velocity.x, 0, drag * Time.fixedDeltaTime);

        // 左右速度制限
        velocity.x = Mathf.Clamp(velocity.x, -horizontalMaxSpeed, horizontalMaxSpeed);

        // -----------------------------
        // 上下
        // -----------------------------
        if (moveInput.y > 0)
        {
            velocity.y += floatForce * Time.fixedDeltaTime;
            if (velocity.y > maxFloatSpeed) velocity.y = maxFloatSpeed;
        }
        else if (moveInput.y < 0)
        {
            velocity.y -= descendForce * Time.fixedDeltaTime;
            if (velocity.y < -maxDescendSpeed) velocity.y = -maxDescendSpeed;
        }
        else
        {
            velocity.y = Mathf.Lerp(velocity.y, 0, drag * Time.fixedDeltaTime);
        }

        rb.velocity = velocity;
    }
}
