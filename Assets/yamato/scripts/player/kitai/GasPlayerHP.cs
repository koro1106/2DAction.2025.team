using UnityEngine;

public class GasPlayer : MonoBehaviour
{
    [Header("HP�ݒ�")]
    public float maxHP = 100f;        // �ő�HP�i���ʁj
    public float currentHP;           // ���݂�HP

    [Header("�_���[�W�ݒ�")]
    public float damagePerSecond = 10f;  // �ǂɐG��Ă���ԂɌ���HP��

    [Header("Particle�ݒ�")]
    public ParticleSystem gasParticles;   // �v���C���[�{�̖̂AParticle
    public float maxEmissionRate = 100f; // HP���^�����̖A�̗�
    public float minEmissionRate = 0f;   // HP0���̖A�̗ʁi���Ȃ߂ɂ���j

    [Header("���S�G�t�F�N�g")]
    public GameObject deathParticlePrefab; // HP0���ɏo��ParticlePrefab

    private ParticleSystem.EmissionModule emission; // �A�̗ʂ𐧌䂷�邽�߂̃��W���[��
    private bool touchingWall = false;               // �ǂɐڐG���Ă��邩
    private bool isDead = false;                     // ���Ɏ��S�ς݂����肷��t���O

    void Start()
    {
        // ����HP���ő�ɐݒ�
        currentHP = maxHP;

        // Particle��Inspector�Ŗ��ݒ�̏ꍇ�͎q�I�u�W�F�N�g����擾
        if (gasParticles == null)
            gasParticles = GetComponentInChildren<ParticleSystem>();

        // Emission���W���[�����擾���āA�����A�ʂ��Z�b�g
        emission = gasParticles.emission;
        emission.rateOverTime = maxEmissionRate;
    }

    void Update()
    {
        if (isDead) return; // ���S�ς݂Ȃ�Update�������~

        // �ǂɐG��Ă���ꍇ�A���bHP�����炷
        if (touchingWall)
        {
            currentHP -= damagePerSecond * Time.deltaTime;
            currentHP = Mathf.Max(currentHP, 0f); // HP��0�����ɂȂ�Ȃ��悤�ɐ���
        }

        // HP�ɉ����ĖA�̗ʂ���`��ԂŕύX
        float hpRatio = currentHP / maxHP; // 0�`1
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(
            Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio)
        );

        // HP��0�ɂȂ����玀�S�������Ă�
        if (currentHP <= 0f && !isDead)
        {
            Die();
        }
    }

    // -------------------------
    // �ǂƂ̐ڐG�����m����֐�
    // -------------------------
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

    // -------------------------
    // HP0�ɂȂ����Ƃ��̎��S����
    // -------------------------
    void Die()
    {
        isDead = true;       // ���Ɏ��S�����t���O�𗧂Ă�

        gasParticles.Stop(); // ���̖AParticle���~�߂�

        // ���S�pParticle�𐶐�
        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }

        // 2�b��Ƀv���C���[�I�u�W�F�N�g������
        Destroy(gameObject, 2f);
    }
}
