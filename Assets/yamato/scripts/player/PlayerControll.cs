using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("�v���C���[�t�H�[��")]
    public GameObject gasForm;
    public GameObject solidForm;

    [Header("HP�ݒ�")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("UI�ݒ�")]
    public Slider hpSlider;

    private bool isGas = true;

    void Start()
    {
        currentHP = maxHP;

        // �ŏ��͋C�̏�ԂŊJ�n
        gasForm.SetActive(true);
        solidForm.SetActive(false);

        // HP�o�[������
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

        // �e�t�H�[����HP�������n��
        ApplyHPToForms();
    }

    void Update()
    {
        // �؂�ւ�
        if (Input.GetKeyDown(KeyCode.G)) SwitchToGas();
        if (Input.GetKeyDown(KeyCode.H)) SwitchToSolid();

        // �e�t�H�[����HP�����L�i�Е��Ō����Ă����f�j
        SyncHPFromActiveForm();

        // HP�o�[�X�V
        if (hpSlider != null)
            hpSlider.value = currentHP;
    }

    void SwitchToGas()
    {
        if (isGas) return;

        // ���݂�HP��ێ�
        SyncHPFromActiveForm();

        // �؂�ւ�
        gasForm.SetActive(true);
        solidForm.SetActive(false);
        isGas = true;

        ApplyHPToForms();
    }

    void SwitchToSolid()
    {
        if (!isGas) return;

        // ���݂�HP��ێ�
        SyncHPFromActiveForm();

        // �؂�ւ�
        gasForm.SetActive(false);
        solidForm.SetActive(true);
        isGas = false;

        ApplyHPToForms();
    }

    void SyncHPFromActiveForm()
    {
        if (isGas)
        {
            var gas = gasForm.GetComponent<GasPlayer>();
            currentHP = gas.currentHP;
        }
        else
        {
            // �ő̂�HP�������������Ȃ瓯�l�ɊǗ��i���ʕϐ���OK�j
            var solid = solidForm.GetComponent<SolidHP>();
            currentHP = solid.currentHP;
        }
    }

    void ApplyHPToForms()
    {
        if (gasForm.TryGetComponent(out GasPlayer gas))
            gas.currentHP = currentHP;

        if (solidForm.TryGetComponent(out SolidHP solid))
            solid.currentHP = currentHP;
    }
}
