using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GasPlayerComtoroller : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveSpeed = 5f;
    public float floatForce = 3f;//�㏸��
    public float maxFloatSpeed = 4f;//�㏸�̍ō����x
    public float drag = 1.5f;//��C��R�i�ӂ�ӂ튴�j

    public float descendForce = 3f;    // ���~�́i�f�o�b�O�p�j
    public float maxDescendSpeed = 4f;//���~

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

        // ��L�[�܂���W�L�[�ŕ���
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = 1f;
        }
        // �f�o�b�O�p���~
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }

        moveInput = new Vector2(moveX, moveY);
    }

    private void FixedUpdate()
    {
        //���E�ړ�
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        //�㏸����
        if(moveInput.y > 0)
        {
            //������̗͂�������
            rb.AddForce(Vector2.up * floatForce, ForceMode2D.Force);

            //�㏸���x�̏���𐧌�
            if(rb.velocity.y > maxFloatSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxFloatSpeed);
            }
        }
        // ���~�i�f�o�b�O�p�j
        else if (moveInput.y < 0)
        {
            rb.AddForce(Vector2.down * descendForce, ForceMode2D.Force);

            if (rb.velocity.y < -maxDescendSpeed)
                rb.velocity = new Vector2(rb.velocity.x, -maxDescendSpeed);
        }
    }
}
