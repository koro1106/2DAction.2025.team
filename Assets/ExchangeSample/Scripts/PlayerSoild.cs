using System;
using UnityEngine;

namespace ExchangeSample.Scripts
{
    public class PlayerSolid : MonoBehaviour
    {
        //  他状態(Liquid/Gas)の位置同期をさせるため
        [SerializeField] private Character rootCharacter;   //  親のCharacter
        
        public float moveSpeed = 3f;
        public float jumpForce = 20f;
        public float rollSpeed = 480f;

        private Rigidbody2D rb;
        private bool isGrounded = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            // 地面から少し浮かせる
            Vector3 pos = transform.position;
            pos.y += 0.1f;
            transform.position = pos;

            // 物理設定
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
        }

        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            // 横移動
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

            // 常に回転（接地していなくても）
            if (horizontal != 0)
                rb.angularVelocity = -horizontal * rollSpeed;
            else
                rb.angularVelocity = 0;

            // ジャンプ
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
            //  [UpDownGround]というTagが登録されておらず、この判定でエラーになっている
            #if false
            if (collision.gameObject.CompareTag("UpDownGround"))
            {
                isGrounded = false;
            }
            #endif
        }

        //  状態変化用トリガーに接触
        private void OnTriggerEnter2D(Collider2D other)
        {
            //  自分自身に変化させないように判定
            if (!other.gameObject.CompareTag("ToSolid"))
            {
                rootCharacter.ChangeCharacter(other.gameObject.tag, transform.position);
            }
        }
    }
}
