using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;      // Player の子オブジェクト
    public Transform crosshair;       // Crosshair（ワールドオブジェクト）
    public float attackCooldown = 0.2f;
    public float projectileSpeedOverride = -1f; // -1でPrefab側のspeed使用




    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // 左クリックで発射
        if (Input.GetMouseButtonDown(0) && timer >= attackCooldown)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        Vector2 dir = (crosshair.position - shootPoint.position).normalized;
        if (dir.sqrMagnitude < 0.0001f) dir = transform.right; // 0ベクトル対策

        GameObject go = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Projectile p = go.GetComponent<Projectile>();
        if (p != null) p.Init(dir, projectileSpeedOverride);

        // 簡単な発射演出：プレイヤーを少し縮める（optional）
        StartCoroutine(DoRecoil());
    }

    System.Collections.IEnumerator DoRecoil()
    {
        Vector3 original = transform.localScale;
        Vector3 squish = original * 0.9f;
        float t = 0f;
        float dur = 0.08f;
        while (t < dur)
        {
            transform.localScale = Vector3.Lerp(original, squish, t / dur);
            t += Time.deltaTime;
            yield return null;
        }
        // 戻す
        t = 0f;
        while (t < dur)
        {
            transform.localScale = Vector3.Lerp(squish, original, t / dur);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = original;
    }
}
