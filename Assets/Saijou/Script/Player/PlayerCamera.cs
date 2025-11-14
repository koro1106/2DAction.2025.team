using UnityEngine;

public class PlayerCamera: MonoBehaviour
{
    public Transform player;  // 追従させるターゲット（プレイヤー）のTransform
    public float followSpeed = 5f;  // 追従のスピード
    public StartPerformance startPerformance;
    private void Update()
    {
        // スタート演出終わってたらプレイヤーにカメラ追従させる
        if (startPerformance.preformanceFinished)
        {
            // プレイヤーのX座標にカメラのX座標を追従させ、Yは固定
            if (player != null)
            {
                // プレイヤーの位置を追従（X座標のみ）
                float newX = Mathf.Lerp(transform.position.x, player.position.x, followSpeed * Time.deltaTime);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            }
        }
       
    }
}
