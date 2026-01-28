using UnityEngine;

public class WaterBall : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ground ‚É“–‚½‚Á‚½‚çÁ‚·
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("“G‚É–½’†I");
            Destroy(other.gameObject); // ‰¼F“G‚ğ“|‚·
            Destroy(gameObject);
        }
    }
}
