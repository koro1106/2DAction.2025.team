using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ínñ Ç…ìñÇΩÇ¡ÇΩÇÁè¡Ç¶ÇÈ
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
