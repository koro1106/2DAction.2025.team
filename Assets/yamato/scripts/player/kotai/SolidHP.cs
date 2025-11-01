using UnityEngine;

public class SolidHP : MonoBehaviour
{
    [Header("HP�ݒ�")]
    public float currentHP;
    public float maxHP = 100f;

    [Header("�_���[�W�ݒ�")]
    //public float damagePerSecond = 1f; // �ő̂ł����������� or �C��

    private bool isDead = false;

    void Start()
    {
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    void Update()
    {
        if (isDead) return;

        // �ő̂����������炵�����Ȃ�i�C�̂Ɠ��������j
      //  currentHP -= damagePerSecond * Time.deltaTime;
        currentHP = Mathf.Max(currentHP, 0f);

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // �ő̎��S���̏����i�K�v�Ȃ�j
        Destroy(gameObject, 1f);
    }
}
