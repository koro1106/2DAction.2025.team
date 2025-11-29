using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public PlayerState currentState;

    void Start()
    {
        currentState = PlayerState.Gas; // 初期状態
    }

    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
        Debug.Log("State changed: " + currentState);

        // 状態ごとの見た目・能力を変更
        UpdateStateBehavior();
    }

    void UpdateStateBehavior()
    {
        switch (currentState)
        {
            case PlayerState.Gas:
                // 気体の挙動（浮く・当たり判定弱い 等）
                break;

            case PlayerState.Liquid:
                // 液体の挙動（流れる・狭い隙間を通れる 等）
                break;

            case PlayerState.Solid:
                // 固体の挙動（重い・ジャンプ力低い 等）
                break;
        }
    }
}
