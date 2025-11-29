using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // 状態の種類
    public enum State { Gas, Liquid, Solid }

    [Header("状態オブジェクト")]
    public GameObject gasObject;    // 気体状態のオブジェクト
    public GameObject liquidObject; // 液体状態のオブジェクト
    public GameObject solidObject;  // 固体状態のオブジェクト

    // 現在の状態
    public State currentState = State.Gas;

    void Update()
    {
        // キー入力で状態を切り替え
        if (Input.GetKeyDown(KeyCode.B)) ChangeToLiquid();
        if (Input.GetKeyDown(KeyCode.N)) ChangeToSolid();
        if (Input.GetKeyDown(KeyCode.M)) ChangeToGas();
    }

    /// <summary>
    /// 現在アクティブな状態の Rigidbody2D を取得
    /// </summary>
    private Rigidbody2D GetCurrentActiveRigidbody()
    {
        switch (currentState)
        {
            case State.Solid: return solidObject != null ? solidObject.GetComponent<Rigidbody2D>() : null;
            case State.Liquid: return liquidObject != null ? liquidObject.GetComponent<Rigidbody2D>() : null;
            case State.Gas: return gasObject != null ? gasObject.GetComponent<Rigidbody2D>() : null;
            default: return null;
        }
    }

    /// <summary>
    /// 現在の状態の位置を取得
    /// Rigidbody2D があれば位置を取得、なければ Transform から取得
    /// </summary>
    private Vector3 GetCurrentPosition()
    {
        Rigidbody2D rb = GetCurrentActiveRigidbody();
        if (rb != null) return rb.position;

        // Rigidbody が無ければ Transform から取得
        switch (currentState)
        {
            case State.Solid: return solidObject.transform.position;
            case State.Liquid: return liquidObject.transform.position;
            case State.Gas: return gasObject.transform.position;
            default: return transform.position;
        }
    }

    /// <summary>
    /// 現在の状態の速度を取得
    /// Rigidbody2D があれば速度を取得、なければ 0 を返す
    /// </summary>
    private Vector2 GetCurrentVelocity()
    {
        Rigidbody2D rb = GetCurrentActiveRigidbody();
        return rb != null ? rb.velocity : Vector2.zero;
    }

    /// <summary>
    /// 指定したオブジェクトに位置と速度をセットする
    /// Rigidbody2D があれば物理情報を設定し、無ければ Transform の位置だけ設定
    /// </summary>
    private void SetObjectTransform(GameObject obj, Vector3 pos, Vector2 velocity)
    {
        if (obj == null) return;

        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;           // 一時的にキネマティックにして物理の影響を止める
            rb.transform.position = pos;     // Transformで位置を設定
            rb.velocity = velocity;          // 前の状態の速度を反映
            rb.isKinematic = false;          // キネマティックを戻す
        }
        else
        {
            obj.transform.position = pos;    // Rigidbody がない場合は位置だけ設定
        }
    }

    /// <summary>
    /// 状態を変更する共通処理
    /// </summary>
    private void ChangeState(State newState, GameObject newObj)
    {
        // 現在の位置と速度を取得して次の状態に引き継ぐ
        Vector3 currentPos = GetCurrentPosition();
        Vector2 currentVel = GetCurrentVelocity();

        // 全オブジェクトを非アクティブ化
        gasObject.SetActive(false);
        liquidObject.SetActive(false);
        solidObject.SetActive(false);

        // 新しい状態をアクティブ化
        if (newObj != null) newObj.SetActive(true);

        // 状態を更新
        currentState = newState;

        // 位置と速度をセット
        if (newObj != null) SetObjectTransform(newObj, currentPos, currentVel);
    }

    // 状態ごとの切り替え関数
    public void ChangeToGas() => ChangeState(State.Gas, gasObject);
    public void ChangeToLiquid() => ChangeState(State.Liquid, liquidObject);
    public void ChangeToSolid() => ChangeState(State.Solid, solidObject);
}
