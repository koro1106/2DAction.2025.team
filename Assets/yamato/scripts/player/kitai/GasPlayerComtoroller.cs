using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GasPlayerComtoroller : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveSpeed = 5f;
    public float floatForce = 3f;//上昇力
    public float maxFloatSpeed = 4f;//上昇の最高速度
    public float drag = 1.5f;//空気抵抗（ふわふわ感）

    public float descendForce = 3f;    // 下降力（デバッグ用）
    public float maxDescendSpeed = 4f;//下降

    private Rigidbody2D rb;
    private Vector2 moveInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = drag;
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = 0f;

        // 上キーまたはWキーで浮く
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 1f;
        }
        // デバッグ用下降
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }

        moveInput = new Vector2(moveX, moveY);
    }

    private void FixedUpdate()
    {
        //左右移動
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        //上昇処理
        if(moveInput.y > 0)
        {
            //上向きの力を加える
            rb.AddForce(Vector2.up * floatForce, ForceMode2D.Force);

            //上昇速度の上限を制限
            if(rb.velocity.y > maxFloatSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFloatSpeed);
            }
        }
        // 下降（デバッグ用）
        else if (moveInput.y < 0)
        {
            rb.AddForce(Vector2.down * descendForce, ForceMode2D.Force);

            if (rb.velocity.y < -maxDescendSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -maxDescendSpeed);
        }
    }
}
