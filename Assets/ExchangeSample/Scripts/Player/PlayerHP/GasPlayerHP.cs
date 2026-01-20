using UnityEngine;

public class GasPlayer : MonoBehaviour
{
    public float normalDamagePerSecond = 1f;
    public float wallDamagePerSecond = 5f;

    public ParticleSystem gasParticles;
    public float maxEmissionRate = 100f;
    public float minEmissionRate = 0f;

    private bool touchingWall = false;

    PlayerHP playerHP;

    void Start()
    {
        playerHP = GetComponentInParent<PlayerHP>();
    }

    void Update()
    {
        if (playerHP == null) return;

        // ★変更点：ダメージを与えるだけ
        float damageRate = touchingWall ? wallDamagePerSecond : normalDamagePerSecond;
        playerHP.Damage(damageRate * Time.deltaTime);

        // ★変更点：見た目制御のみ
        float hpRatio = playerHP.currentHP / playerHP.maxHP;
        var emission = gasParticles.emission;
        emission.rateOverTime = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);
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
}