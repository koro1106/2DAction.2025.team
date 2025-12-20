using System.Collections;
using UnityEngine;

namespace ExchangeSample.Scripts
{
    public class SlimeMove : MonoBehaviour
    {
        //  他状態(Liquid/Gas)の位置同期をさせるため
        [SerializeField] private Character rootCharacter;   //  親のCharacter
        
        // ===========================
        //  �p�����[�^
        // ===========================

        public float speed = 3f;         // ���ړ��̃X�s�[�h
        public float jumpForce = 40f;    // �W�����v�̋���
        public float groundCheckDistance = 1.0f; // �n�ʃ`�F�b�N����
        public ParticleSystem landingEffect;     // ���n�G�t�F�N�g�iInspector�Őݒ�j

        // ===========================
        //  �����ϐ�
        // ===========================
        private Rigidbody2D rb;

        private bool isGround = false;   // ���t���[���̒n�ʔ���
        private bool wasGround = false;  // �O�t���[���̒n�ʔ���
        private bool isJumping = false;  // SoftJump�A���h�~

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // -------------------------------
            // �� ���ړ�
            // -------------------------------
            float x = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(x * speed, rb.velocity.y);

            // -------------------------------
            // �� �n�ʔ���iTag����j
            // -------------------------------
            wasGround = isGround; // �O�t���[����ۑ�

            RaycastHit2D hit = Physics2D.Raycast(
                new Vector2(transform.position.x, transform.position.y - 0.5f),
                Vector2.down,
                groundCheckDistance
            );

            // Ray���������Ă���Tag��"Ground"�Ȃ�n�ʂƔ���
            isGround = (hit.collider != null && hit.collider.CompareTag("Ground"));

            // -------------------------------
            // �� �� �n�ʂ��痣�� �� ���n�����u�� Particle
            // -------------------------------
            if (!wasGround && isGround)
            {
                if (landingEffect != null)
                    landingEffect.Play(); // 1�񂾂��Đ�
            }

            // -------------------------------
            // �� �W�����v�����i�n�ʂɂ��鎞�����j
            // -------------------------------
            if (Input.GetKeyDown(KeyCode.Space) && isGround && !isJumping)
            {
                StartCoroutine(SoftJump());
            }
        }

        // =====================================================
        // �� �_�炩�W�����v�iAddForce�� 0.1�b �����ĕ��U�j
        // =====================================================
        private IEnumerator SoftJump()
        {
            isJumping = true;

            float duration = 0.1f;
            float timer = 0f;

            while (timer < duration)
            {
                // AddForce �𕪎U���� "�ӂ��" �ƃW�����v
                rb.AddForce(Vector2.up * jumpForce * 0.4f * Time.deltaTime * 60f, ForceMode2D.Force);

                timer += Time.deltaTime;
                yield return null;
            }

            // �딻��ő��W�����v�ł��Ȃ��悤�N�[���^�C��
            yield return new WaitForSeconds(0.1f);

            isJumping = false;
        }

        // =====================================================
        // �� Scene�r���[�� Ray �������i�f�o�b�O�j
        // =====================================================
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                new Vector3(transform.position.x, transform.position.y - 0.5f),
                new Vector3(transform.position.x, transform.position.y - 0.5f - groundCheckDistance)
            );
        }
        
        //  状態変化用トリガーに接触
        private void OnTriggerEnter2D(Collider2D other)
        {
            //  自分自身に変化させないように判定
            if (!other.gameObject.CompareTag("ToLiquid"))
            {
                rootCharacter.ChangeCharacter(other.gameObject.tag, transform.position);
            }
        }
    }
}
