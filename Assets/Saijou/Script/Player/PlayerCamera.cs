using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerCamera: MonoBehaviour
{
    public Transform[] players;  // 追従させるターゲット（プレイヤー）のTransform
    public float followSpeed = 5f;  // 追従のスピード
    public StartPerformance startPerformance;

    private const float followStartY = 11f; // 追従開始Y
    private const float fixedY = -1f;       // 固定Y

    // 表示されてるプレイヤー取得
    Transform GetActivePlayer()
    {
        foreach (Transform p in players)
        {
            if (p.gameObject.activeInHierarchy)
            {
                return p;
            }
        }
        return null;
    }
    private void Update()
    {
        Transform player = GetActivePlayer();
        if (player == null) return;

        // スタート演出終わってたらプレイヤーにカメラ追従させる
        if (startPerformance.preformanceFinished)
        {
            // プレイヤーのX座標にカメラのX座標を追従させ、Yは固定
            if (player != null)
            {
                // プレイヤーの位置を追従（X座標のみ）
                float newX = Mathf.Lerp(transform.position.x, player.position.x, followSpeed * Time.deltaTime);
                float newY;

                // プレイヤーが11以上なら追従
                if(player.position.y >= followStartY)
                {
                    newY = Mathf.Lerp(transform.position.y, player.position.y, followSpeed * Time.deltaTime);
                }
                else
                {
                    // Y11未満なら -1 に固定
                    newY = Mathf.Lerp(transform.position.y,fixedY,followSpeed * Time.deltaTime
            );
                }
                transform.position = new Vector3(newX, newY, transform.position.z);
            }
        }
       
    }
}
