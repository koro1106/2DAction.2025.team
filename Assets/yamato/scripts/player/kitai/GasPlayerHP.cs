using UnityEngine;

public class GasPlayer : MonoBehaviour
{
    [Header("HP設定")]
    public float maxHP = 100f;        // 最大HP（水量）
    public float currentHP;           // 現在のHP

    [Header("ダメージ設定")]
    public float damagePerSecond = 10f;  // 壁に触れている間に減るHP量

    [Header("Particle設定")]
    public ParticleSystem gasParticles;   // プレイヤー本体の泡Particle
    public float maxEmissionRate = 100f; // HP満タン時の泡の量
    public float minEmissionRate = 0f;   // HP0時の泡の量（少なめにする）

    [Header("死亡エフェクト")]
    public GameObject deathParticlePrefab; // HP0時に出すParticlePrefab

    private ParticleSystem.EmissionModule emission; // 泡の量を制御するためのモジュール
    private bool touchingWall = false;               // 壁に接触しているか
    private bool isDead = false;                     // 既に死亡済みか判定するフラグ

    void Start()
    {
        // 初期HPを最大に設定
        currentHP = maxHP;

        // ParticleがInspectorで未設定の場合は子オブジェクトから取得
        if (gasParticles == null)
            gasParticles = GetComponentInChildren<ParticleSystem>();

        // Emissionモジュールを取得して、初期泡量をセット
        emission = gasParticles.emission;
        emission.rateOverTime = maxEmissionRate;
    }

    void Update()
    {
        if (isDead) return; // 死亡済みならUpdate処理を停止

        // 壁に触れている場合、毎秒HPを減らす
        if (touchingWall)
        {
            currentHP -= damagePerSecond * Time.deltaTime;
            currentHP = Mathf.Max(currentHP, 0f); // HPが0未満にならないように制限
        }

        // HPに応じて泡の量を線形補間で変更
        float hpRatio = currentHP / maxHP; // 0～1
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(
            Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio)
        );

        // HPが0になったら死亡処理を呼ぶ
        if (currentHP <= 0f && !isDead)
        {
            Die();
        }
    }

    // -------------------------
    // 壁との接触を検知する関数
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
    // HP0になったときの死亡処理
    // -------------------------
    void Die()
    {
        isDead = true;       // 既に死亡したフラグを立てる

        gasParticles.Stop(); // 元の泡Particleを止める

        // 死亡用Particleを生成
        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }

        // 2秒後にプレイヤーオブジェクトを消す
        Destroy(gameObject, 2f);
    }
}
