using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [Header("移動速度")] public float speed = 3f;
    [Header("右向きか")] public bool isRight = false;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 画面に見えたら移動開始
        if (sr.isVisible)
        {
           Debug.Log("敵が画面上に見えた");

            if (isRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
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
