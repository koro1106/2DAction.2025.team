using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlayerController : MonoBehaviour
{
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

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;

        // -----------------------------
        // ���E
        // -----------------------------
        if (moveInput.x > 0)
            velocity.x += moveSpeed * Time.fixedDeltaTime;
        else if (moveInput.x < 0)
            velocity.x -= moveSpeed * Time.fixedDeltaTime;
        else
            velocity.x = Mathf.Lerp(velocity.x, 0, drag * Time.fixedDeltaTime);

        // ���E���x����
        velocity.x = Mathf.Clamp(velocity.x, -horizontalMaxSpeed, horizontalMaxSpeed);

        // -----------------------------
        // �㉺
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
