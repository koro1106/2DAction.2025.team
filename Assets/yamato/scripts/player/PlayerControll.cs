using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("フォームオブジェクト")]
    public GameObject gasForm;
    public GameObject solidForm;
    public GameObject liquidForm; // 必要なら追加

    [Header("HP設定")]
    public float maxHP = 100f;
    public float currentHP;

    [Header("UI設定")]
    public Slider hpSlider;

    private enum FormType { Gas, Solid, Liquid }
    private FormType currentForm = FormType.Gas;

    void Start()
    {
        currentHP = maxHP;
        SetActiveForm(FormType.Gas);

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }

        ApplyHPToForms();
    }

    void Update()
    {
        // キー操作による手動切替
        if (Input.GetKeyDown(KeyCode.G)) SwitchForm(FormType.Gas);
        if (Input.GetKeyDown(KeyCode.H)) SwitchForm(FormType.Solid);
        if (Input.GetKeyDown(KeyCode.J)) SwitchForm(FormType.Liquid);

        // HP同期
        SyncHPFromActiveForm();

        if (hpSlider != null)
            hpSlider.value = currentHP;
    }

    // フォーム切替
    private void SwitchForm(FormType newForm)
    {
        if (currentForm == newForm) return;

        SyncHPFromActiveForm();
        SetActiveForm(newForm);
        currentForm = newForm;
        ApplyHPToForms();
    }

    private void SetActiveForm(FormType form)
    {
        gasForm.SetActive(form == FormType.Gas);
        solidForm.SetActive(form == FormType.Solid);
        if (liquidForm != null)
            liquidForm.SetActive(form == FormType.Liquid);
    }

    private void SyncHPFromActiveForm()
    {
        switch (currentForm)
        {
            case FormType.Gas:
                if (gasForm.TryGetComponent(out GasPlayer gas))
                    currentHP = gas.currentHP;
                break;
            case FormType.Solid:
                if (solidForm.TryGetComponent(out SolidHP solid))
                    currentHP = solid.currentHP;
                break;
            case FormType.Liquid:
                if (liquidForm != null && liquidForm.TryGetComponent(out LiquidHP liquid))
                    currentHP = liquid.currentHP;
                break;
        }
    }

    private void ApplyHPToForms()
    {
        if (gasForm.TryGetComponent(out GasPlayer gas))
            gas.currentHP = currentHP;
        if (solidForm.TryGetComponent(out SolidHP solid))
            solid.currentHP = currentHP;
        if (liquidForm != null && liquidForm.TryGetComponent(out LiquidHP liquid))
            liquid.currentHP = currentHP;
    }

    // =====================================================
    // ■ 状態変化ブロックに当たったら自動でフォーム切替
    // =====================================================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LiquidToSolid") && currentForm == FormType.Liquid)
        {
            SwitchForm(FormType.Solid);
        }
        else if (collision.CompareTag("SolidToGas") && currentForm == FormType.Solid)
        {
            SwitchForm(FormType.Gas);
        }
        else if (collision.CompareTag("GasToLiquid") && currentForm == FormType.Gas)
        {
            SwitchForm(FormType.Liquid);
        }
    }
}
