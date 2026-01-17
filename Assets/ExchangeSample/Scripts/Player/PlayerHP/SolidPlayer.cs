using UnityEngine;

public class SolidPlayer : MonoBehaviour
{
    PlayerHP playerHP;

    void Start()
    {
        playerHP = GetComponentInParent<PlayerHP>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float damage = playerHP.maxHP * 0.1f; // 10%
            playerHP.Damage(damage);
        }
    }
}
