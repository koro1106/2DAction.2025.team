using UnityEngine;

public class GasPlayer : MonoBehaviour
{
    // 通常時に毎秒与えるダメージ量
    public float normalDamagePerSecond = 1f;

    // 壁に触れている時に毎秒与えるダメージ量
    public float wallDamagePerSecond = 5f;

    // ガスの見た目用パーティクル
    public ParticleSystem gasParticles;

    // HP最大時のパーティクル量
    public float maxEmissionRate = 100f;

    // HP最小時のパーティクル量
    public float minEmissionRate = 0f;

    // 壁に触れているかどうか
    private bool touchingWall = false;

    // 親オブジェクトにある PlayerHP を参照
    PlayerHP playerHP;

    void Start()
    {
        
        // GasPlayer 自身ではなく「親(Player)」の PlayerHP を取得する
        playerHP = GetComponentInParent<PlayerHP>();
    }

    void Update()
    {
        // PlayerHP が見つからなければ何もしない
        if (playerHP == null) return;

        // HPが0以下でも Damage は呼ばれるが
        // PlayerHP 側で isDead を見て無視される
        float damageRate = touchingWall ? wallDamagePerSecond : normalDamagePerSecond;

        // 毎フレーム少しずつダメージを与える
        playerHP.Damage(damageRate * Time.deltaTime);

        // ---- 見た目制御 ----

        // HPの割合（0～1）
        float hpRatio = playerHP.currentHP / playerHP.maxHP;

        // パーティクルの放出量をHPに応じて変化させる
        var emission = gasParticles.emission;
        emission.rateOverTime = Mathf.Lerp(minEmissionRate, maxEmissionRate, hpRatio);
    }

    // 壁に触れた瞬間
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            touchingWall = true;
    }

    // 壁から離れた瞬間
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            touchingWall = false;
    }
}
