using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlayerHP : MonoBehaviour
{
    [Header("HP�i���ʁj�ݒ�")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("�_���[�W�ݒ�")]
    public float damagePerSecond = 5f; // �ǂɓ������Ă���ԂɌ����

    [Header("Particle�ݒ�")]
    public ParticleSystem gasParticles;
    public float maxEmissionRate = 100f; // HP�����^�����̖A�̗�
    public float minEmissionRate = 5f;   // HP��0�̎��̖A�̗�

    private ParticleSystem.EmissionModule emission;
    private bool touchingWall = false;

    void Start()
    {
        currentHP = maxHP;

        if (gasParticles == null)
        {
            gasParticles = GetComponentInChildren<ParticleSystem>();
        }

        emission = gasParticles.emission;
        emission.rateOverTime = maxEmissionRate;
    }

    void Update()
    {
        // �ǂɐG��Ă���ԁAHP�����炷
        if (touchingWall)
        {
            currentHP -= damagePerSecond * Time.deltaTime;
            currentHP = Mathf.Max(currentHP, 0f); // 0�����ɂȂ�Ȃ�
        }

        // HP�ɉ����ĖA�̗ʂ𒲐�
        float hpRatio = currentHP / maxHP; // 0�`1
        float targetRate = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);
        emission.rateOverTime = targetRate;
    }

    // �ǂɓ��������猸���J�n
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            touchingWall = true;
        }
    }

    // ���ꂽ�猸���X�g�b�v
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            touchingWall = false;
        }
    }
}