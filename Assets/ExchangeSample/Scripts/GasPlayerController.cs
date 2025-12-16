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

        [Header("Wind Setting")]
        public float windDragMultiplier = 0.2f; // 風中の drag 倍率
        private bool isInWind = false;
        private Vector2 externalVelocity; // 風の外力

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.drag = 0f; // drag�͎���Ŏ���
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
            // 現在の Rigidbody の速度
            Vector2 velocity = rb.velocity;

            // -----------------------------
            // 外力を加える（WindEffect からの加算）
            // -----------------------------
            velocity += externalVelocity;
            // externalVelocity はここではリセットせず、WindEffect が毎フレーム加算する想定

            // -----------------------------
            // 横移動（プレイヤー入力）
            // -----------------------------
            if (moveInput.x > 0f)
                velocity.x += moveSpeed * Time.fixedDeltaTime;
            else if (moveInput.x < 0f)
                velocity.x -= moveSpeed * Time.fixedDeltaTime;
            else if (!isInWind)
                // 風中でなければ減速
                velocity.x = Mathf.Lerp(velocity.x, 0f, drag * Time.fixedDeltaTime);

            // 横方向速度の最大値
            float maxX = isInWind ? horizontalMaxSpeed * 5f : horizontalMaxSpeed;
            velocity.x = Mathf.Clamp(velocity.x, -maxX, maxX);

            // -----------------------------
            // 縦移動（プレイヤー入力）
            // -----------------------------
            if (moveInput.y > 0f)
                velocity.y += floatForce * Time.fixedDeltaTime;
            else if (moveInput.y < 0f)
                velocity.y -= descendForce * Time.fixedDeltaTime;
            else if (!isInWind)
                velocity.y = Mathf.Lerp(velocity.y, 0f, drag * Time.fixedDeltaTime);

            // -----------------------------
            // Rigidbody に反映
            // -----------------------------
            rb.velocity = velocity;

            // -----------------------------
            // 外力はここではリセットしない
            // WindEffect 側で毎フレーム加算される
            // -----------------------------
        }




        // 風状態を受け取る関数
        public void SetWindState(bool inWind)
        {
            isInWind = inWind;
            if (!inWind)
                externalVelocity = Vector2.zero; // 風が止まったら外力もゼロ
        }
        public void AddExternalVelocity(Vector2 v)
        {
            externalVelocity += v;
        }


        //  状態変化用トリガーに接触
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("GasPlayer TriggerEnter: " + other.name);
            //  自分自身に変化させないように判定
            if (!other.gameObject.CompareTag("ToGas"))
            {
                rootCharacter.ChangeCharacter(other.gameObject.tag, transform.position);
            }
        }
    }
}
