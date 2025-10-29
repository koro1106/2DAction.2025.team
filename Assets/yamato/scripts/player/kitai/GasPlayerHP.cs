using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPlayerHP : MonoBehaviour
{
    [Header("HP（水量）設定")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("ダメージ設定")]
    public float damagePerSecond = 5f; // 壁に当たっている間に減る量

    [Header("Particle設定")]
    public ParticleSystem gasParticles;
    public float maxEmissionRate = 100f; // HPが満タン時の泡の量
    public float minEmissionRate = 5f;   // HPが0の時の泡の量

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
        // 壁に触れている間、HPを減らす
        if (touchingWall)
        {
            currentHP -= damagePerSecond * Time.deltaTime;
            currentHP = Mathf.Max(currentHP, 0f); // 0未満にならない
        }

        // HPに応じて泡の量を調整
        float hpRatio = currentHP / maxHP; // 0～1
        float targetRate = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);
        emission.rateOverTime = targetRate;
    }

    // 壁に当たったら減少開始
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            touchingWall = true;
        }
    }

    // 離れたら減少ストップ
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            touchingWall = false;
        }
    }
}