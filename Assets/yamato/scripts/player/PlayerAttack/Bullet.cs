using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    //public float speed = 10f;
    public float lifeTime = 5f;
    //private Vector2 direction;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
        Destroy(gameObject, lifeTime);
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
