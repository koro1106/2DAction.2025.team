using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState currentState = PlayerState.Liquid;

    [Header("各形態のプレイヤー")]
    public GameObject liquidPlayer;   // SlimeMove 付き
    public GameObject gasPlayer;      // GasPlayer + GasPlayerController
    public GameObject solidPlayer;    // PlayerSolid + SolidHP

    [Header("UI")]
    public Slider hpSlider;

    private float currentHP;
    private float maxHP = 100f;

    void Start()
    {
        currentHP = maxHP;
        UpdateActiveForm();
    }

    public void ChangeState(PlayerState newState)
    {
        // 現在の形態のHPを取り出す
        SyncHPFromCurrentState();

        currentState = newState;

        // 新しい形態へHP適用
        ApplyHPToState();

        UpdateActiveForm();
    }


    // --- 形態ごとの ON/OFF ----
    void UpdateActiveForm()
    {
        liquidPlayer.SetActive(currentState == PlayerState.Liquid);
        gasPlayer.SetActive(currentState == PlayerState.Gas);
        solidPlayer.SetActive(currentState == PlayerState.Solid);

        if (hpSlider != null)
            hpSlider.value = currentHP;
    }


    // ---- HPを現在の形態から読み取る ----
    void SyncHPFromCurrentState()
    {
        if (currentState == PlayerState.Liquid)
        {
            currentHP = liquidPlayer.GetComponent<SlimeMoveHP>().currentHP;
        }
        else if (currentState == PlayerState.Gas)
        {
            currentHP = gasPlayer.GetComponent<GasPlayer>().currentHP;
        }
        else if (currentState == PlayerState.Solid)
        {
            currentHP = solidPlayer.GetComponent<SolidHP>().currentHP;
        }
    }

    // ---- HPを新しい形態に適用 ----
    void ApplyHPToState()
    {
        if (currentState == PlayerState.Liquid)
            liquidPlayer.GetComponent<SlimeMoveHP>().currentHP = currentHP;

        if (currentState == PlayerState.Gas)
            gasPlayer.GetComponent<GasPlayer>().currentHP = currentHP;

        if (currentState == PlayerState.Solid)
            solidPlayer.GetComponent<SolidHP>().currentHP = currentHP;
    }
}
