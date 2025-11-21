using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    [Header("ジャンプの強さ")] public float jumpForce= 3f;
    [Header("ジャンプ間隔")] public float jumpInterval = 2f;
    [Header("プレイヤーへ跳ぶ強さ")] public float horizontalPower = 2f;
    public Transform player;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isGround = false; // 地面判定
    private float timer = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(isGround && timer >= jumpInterval)
        {
            Jump();
        }

    }
    void Jump()
    {
        // 画面に見えたら移動開始
        if (sr.isVisible)
        {
            timer = 0f;
            isGround = false;

            // プレイヤーの方向に向く
            float dir = Mathf.Sign(player.position.x - transform.position.x);

            // 力加える
            rb.velocity = new Vector2(dir * horizontalPower, jumpForce);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }
}
