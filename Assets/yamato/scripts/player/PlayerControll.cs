using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーフォーム")]
    public GameObject gasForm;
    public GameObject solidForm;

    [Header("HP設定")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("UI設定")]
    public Slider hpSlider;

    private bool isGas = true;

    void Start()
    {
        currentHP = maxHP;

        // 最初は気体状態で開始
        gasForm.SetActive(true);
        solidForm.SetActive(false);

        // HPバー初期化
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

        // 各フォームにHPを引き渡す
        ApplyHPToForms();
    }

    void Update()
    {
        // 切り替え
        if (Input.GetKeyDown(KeyCode.G)) SwitchToGas();
        if (Input.GetKeyDown(KeyCode.H)) SwitchToSolid();

        // 各フォームのHPを共有（片方で減っても反映）
        SyncHPFromActiveForm();

        // HPバー更新
        if (hpSlider != null)
            hpSlider.value = currentHP;
    }

    void SwitchToGas()
    {
        if (isGas) return;

        // 現在のHPを保持
        SyncHPFromActiveForm();

        // 切り替え
        gasForm.SetActive(true);
        solidForm.SetActive(false);
        isGas = true;

        ApplyHPToForms();
    }

    void SwitchToSolid()
    {
        if (!isGas) return;

        // 現在のHPを保持
        SyncHPFromActiveForm();

        // 切り替え
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
            // 固体もHPを持たせたいなら同様に管理（共通変数でOK）
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
