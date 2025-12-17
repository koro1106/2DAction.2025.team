using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform player;
    public float bulletSpeed = 8f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        if (player == null)
        {
            Debug.LogError("Player が見つからない！");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shoot straight to player");
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || player == null) return;

        Vector2 startPos = transform.position;
        Vector2 targetPos = player.position;

        // ① 方向ベクトル（正規化）
        Vector2 direction = (targetPos - startPos).normalized;

        GameObject bullet = Instantiate(bulletPrefab, startPos, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D が弾にない！");
            return;
        }

        // ② まっすぐ飛ばす
        rb.velocity = direction * bulletSpeed;
    }
}
