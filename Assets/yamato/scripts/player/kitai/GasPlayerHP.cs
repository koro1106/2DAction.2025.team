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

    [Header("UI設定")]
    public Slider hpSlider; // ← SliderをInspectorでアサイン

    private ParticleSystem.EmissionModule emission;
    private bool touchingWall = false;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;

        if (gasParticles == null)
            gasParticles = GetComponentInChildren<ParticleSystem>();

        emission = gasParticles.emission;
        emission.rateOverTime = maxEmissionRate;

        // ★ Slider初期設定
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    void Update()
    {
        if (isDead) return;

        // HP減少処理
        float damageRate = touchingWall ? wallDamagePerSecond : normalDamagePerSecond;
        currentHP -= damageRate * Time.deltaTime;
        currentHP = Mathf.Max(currentHP, 0f);

        // 泡の量をHPに応じて変化
        float hpRatio = currentHP / maxHP;
        emission.rateOverTime = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);

        // ★ Slider更新
        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }

        // HPが0になったら死亡処理
        if (currentHP <= 0f && !isDead)
        {
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

        Destroy(gameObject, 2f);
    }
}
