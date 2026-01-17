using UnityEngine;

public class LiquidPlayer : MonoBehaviour
{
    PlayerHP playerHP;

    void Start()
    {
        playerHP = GetComponentInParent<PlayerHP>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float damage = playerHP.maxHP * 0.1f;
            playerHP.Damage(damage);
        }
    }
}
