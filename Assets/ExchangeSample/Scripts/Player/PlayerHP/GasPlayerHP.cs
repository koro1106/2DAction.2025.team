using UnityEngine;
using UnityEngine.UI; // Sliderを使うために必要

public class GasPlayer : MonoBehaviour
{
    [Header("HP設定")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("ダメージ設定")]
    public float normalDamagePerSecond = 1f;
    public float wallDamagePerSecond = 5f;

    [Header("Particle設定")]
    public ParticleSystem gasParticles;
    public float maxEmissionRate = 100f;
    public float minEmissionRate = 0f;

    [Header("死亡エフェクト")]
    public GameObject deathParticlePrefab;

    
    private ParticleSystem.EmissionModule emission;
    private bool touchingWall = false;
    private bool isDead = false;

    PlayerHP playerHP;
    PlayerRespawn respawn;


    void Start()
    {
        playerHP = GetComponentInParent<PlayerHP>();
        respawn = GetComponentInParent<PlayerRespawn>();


        if (playerHP == null)
        {
            Debug.LogError("PlayerHP が付いていません！");
        }
    }

    void Update()
    {
        if (playerHP == null) return;


        // HP減少処理
        float damageRate = touchingWall ? wallDamagePerSecond : normalDamagePerSecond;
        playerHP.Damage(damageRate * Time.deltaTime);

        // 泡の量をHPに応じて変化
        float hpRatio = playerHP.currentHP / playerHP.maxHP;
        float rate = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);


        // 毎フレーム取り直す（これが超重要）
        var emission = gasParticles.emission;
        emission.rateOverTime = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);

        // 死亡チェック（Particle用）
        if (playerHP.currentHP <= 0)
        {
            gasParticles.Stop();

            if (deathParticlePrefab != null)
                Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            enabled = false; // 演出停止だけ
        }
        if (playerHP.currentHP <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            touchingWall = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            touchingWall = false;
    }

    void Die()
    {
        isDead = true;
        gasParticles.Stop();

        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }
        respawn.Die();
    }
}
