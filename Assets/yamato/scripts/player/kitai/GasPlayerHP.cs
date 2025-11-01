using UnityEngine;
using UnityEngine.UI; // Slider���g�����߂ɕK�v

public class GasPlayer : MonoBehaviour
{
    [Header("HP�ݒ�")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("�_���[�W�ݒ�")]
    public float normalDamagePerSecond = 1f;
    public float wallDamagePerSecond = 5f;

    [Header("Particle�ݒ�")]
    public ParticleSystem gasParticles;
    public float maxEmissionRate = 100f;
    public float minEmissionRate = 0f;

    [Header("���S�G�t�F�N�g")]
    public GameObject deathParticlePrefab;

    [Header("UI�ݒ�")]
    public Slider hpSlider; // �� Slider��Inspector�ŃA�T�C��

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

        // �� Slider�����ݒ�
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    void Update()
    {
        if (isDead) return;

        // HP��������
        float damageRate = touchingWall ? wallDamagePerSecond : normalDamagePerSecond;
        currentHP -= damageRate * Time.deltaTime;
        currentHP = Mathf.Max(currentHP, 0f);

        // �A�̗ʂ�HP�ɉ����ĕω�
        float hpRatio = currentHP / maxHP;
        emission.rateOverTime = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);

        // �� Slider�X�V
        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }

        // HP��0�ɂȂ����玀�S����
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
