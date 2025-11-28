using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SoftBodyController : MonoBehaviour
{
    public float moveForce = 8f;
    public float maxSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 input = new Vector2(moveX, moveY);
        if (input.magnitude > 0.1f && rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(input.normalized * moveForce);
        }
    }
}
