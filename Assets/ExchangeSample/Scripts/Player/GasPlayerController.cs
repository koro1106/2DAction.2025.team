using System;
using UnityEngine;

namespace ExchangeSample.Scripts
{
    public class GasPlayerController : MonoBehaviour
    {
        //  他状態(Liquid/Gas)の位置同期をさせるため
        [SerializeField] private Character rootCharacter;   //  親のCharacter
        
        [Header("Movement Setting")]
        public float moveSpeed = 5f;        // ����������
        public float floatForce = 3f;       // �㏸��
        public float maxFloatSpeed = 4f;    // �㏸�̍ō����x
        public float drag = 1.5f;           // ��C��R�i�ӂ�ӂ튴�j

        public float descendForce = 3f;     // ���~��
        public float maxDescendSpeed = 4f;  // ���~�̍ō����x

        public float horizontalMaxSpeed = 5f; // ���E�ő呬�x

        private Rigidbody2D rb;
        private Vector2 moveInput;

        private bool isInWind = false;
        private ParticleSystem ps; // パーティクルシステム
        public WindEffect windEffect;
        private WindDirection currentWind = null;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.drag = 0f; // drag�͎���Ŏ���

            ps = GetComponent<ParticleSystem>();
            var emission = ps.emission;
            emission.rateOverTime = 60f; //  粒をどれくらいの頻度で出すかを固定
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

        //private void FixedUpdate()
        //{
        //    Vector2 velocity = rb.velocity;

        //    // 風中かどうかで drag を切り替える
        //    float currentDrag = isInWind ? drag * windDragMultiplier : drag;

        //    // -----------------------------
        //    // 横移動
        //    // -----------------------------
        //    if (moveInput.x > 0)
        //        velocity.x += moveSpeed * Time.fixedDeltaTime;
        //    else if (moveInput.x < 0)
        //        velocity.x -= moveSpeed * Time.fixedDeltaTime;
        //    else
        //        velocity.x = Mathf.Lerp(velocity.x, 0, drag * Time.fixedDeltaTime);

        //    // 横方向の最大速度制限
        //    velocity.x = Mathf.Clamp(velocity.x, -horizontalMaxSpeed, horizontalMaxSpeed);

        //    // -----------------------------
        //    // 縦移動
        //    // -----------------------------
        //    if (moveInput.y > 0)
        //    {
        //        velocity.y += floatForce * Time.fixedDeltaTime;
        //        if (velocity.y > maxFloatSpeed) velocity.y = maxFloatSpeed;
        //    }
        //    else if (moveInput.y < 0)
        //    {
        //        velocity.y -= descendForce * Time.fixedDeltaTime;
        //        if (velocity.y < -maxDescendSpeed) velocity.y = -maxDescendSpeed;
        //    }
        //    else
        //    {
        //        velocity.y = Mathf.Lerp(velocity.y, 0, drag * Time.fixedDeltaTime);
        //    }

        //    // 風の力加える
        //    velocity += externalVelocity;
        //    externalVelocity = Vector2.zero;

        //    rb.velocity = velocity;
        //}

        private void FixedUpdate()
        {
            Vector2 velocity = rb.velocity;

            // -------- 横移動 --------
            float targetX = moveInput.x * horizontalMaxSpeed; // 入力に応じた目標速度
            velocity.x = Mathf.Lerp(velocity.x, targetX, drag * Time.fixedDeltaTime);

            // -------- 縦移動 --------
            float targetY = 0f;
            if (moveInput.y > 0f)
                targetY = maxFloatSpeed;       // 上方向の最大速度
            else if (moveInput.y < 0f)
                targetY = -maxDescendSpeed;    // 下方向の最大速度
            else
                targetY = 0f;                  // キーを押していなければ減速

            velocity.y = Mathf.Lerp(velocity.y, targetY, drag * Time.fixedDeltaTime);

            // -------- 風の影響 --------
            //if (isInWind && currentWind != null)
            //{
            //    // 風の影響を速度に直接加算
            //    velocity += currentWind.windDir.normalized * windEffect.windStrength * Time.fixedDeltaTime;
            //}
            //  風の影響(HORIKOSHI Masahiro)
            if (currentWind != null)
            {
                velocity += currentWind.windDir * Time.fixedDeltaTime;
            }

            // -------- 最大速度制限 --------
            velocity.x = Mathf.Clamp(velocity.x, -horizontalMaxSpeed, horizontalMaxSpeed);
            velocity.y = Mathf.Clamp(velocity.y, -maxDescendSpeed, maxFloatSpeed);

            rb.velocity = velocity;
        }


        // --- 風状態 ---
        public void SetWindState(bool inWind)
        {
            isInWind = inWind;
        }

        //  コライダー(Trigger)に入った際に呼び出される
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("GasPlayer TriggerEnter: " + other.name);
            
            //  WindAreaだった場合、情報を受け取る(HORIKOSHI Masahiro)
            if (other.gameObject.CompareTag("WindArea"))
            {
                currentWind = other.GetComponent<WindDirection>();
            }
            //  自分自身に変化させないように判定
            else if (!other.gameObject.CompareTag("ToGas"))
            {
                rootCharacter.ChangeCharacter(other.gameObject.tag, transform.position);
            }
        }

        //  コライダー(Trigger)から抜けた際に呼び出される(HORIKOSHI Masahiro)
        private void OnTriggerExit2D(Collider2D other)
        {
            //  WindAreaから抜けた場合、情報をクリアする
            if (other.gameObject.CompareTag("WindArea"))
            {
                currentWind = null;
            }
        }
    }
}
