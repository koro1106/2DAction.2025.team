using UnityEngine;

public class SoftBodyController : MonoBehaviour
{
    public Rigidbody2D core;  // Core��Rigidbody
    public float speed = 5f;

    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        core.velocity = move * speed;
    }
}
