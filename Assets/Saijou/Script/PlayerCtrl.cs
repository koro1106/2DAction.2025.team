using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 3f;
    private Rigidbody2D rb;
    void Start() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
    }
}
