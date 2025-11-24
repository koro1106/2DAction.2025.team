using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("移動速度")] public float speed = 3f;
    [Header("往復距離")] public float distance = 3f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector2 startPos;
    private int dir = 1; // 1:右　-1:左
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // 重力の影響無効
        rb.gravityScale = 0;

        startPos = transform.position; // スタート位置
    }

    void Update()
    {
        // 画面に見えたら移動開始
        if (sr.isVisible)
        {
            rb.velocity = new Vector2(speed * dir, 0);

            if(Vector2.Distance(startPos, transform.position) >= distance)
            {
                dir *= -1; // 方向反転
                startPos = transform.position; // 新しい位置に更新
            }
        }
        else
        {
            // 画面外では止めておく
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
        }
    }
}
