using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public static PlayerHP Instance; // どの形態からも参照できる

    [Header("HP設定")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("UI")]
    public Slider hpSlider;

    private bool isDead = false;

    public PlayerRespawn respawn;

    void Awake()
    {
        // シングルトン
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentHP = maxHP;
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    public void Damage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0f);

        if (hpSlider != null)
            hpSlider.value = currentHP;

        if (currentHP <= 0f)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        // Respawn に死亡を通知
        respawn.Die();
        //if (respawn != null)
        //{
        //}
        //else
        //{
        //    Debug.LogError("PlayerRespawn が付いていません！");
        //}
    }

    public void ResetHP()
    {
        currentHP = maxHP;
        isDead = false;

        if (hpSlider != null)
            hpSlider.value = currentHP;
    }
}