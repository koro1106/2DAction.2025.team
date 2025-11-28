using UnityEngine;

// ===========================
// スライム本体に追従し、地面着地時にParticleSystemを再生
// ===========================
public class LandingEffectFollow : MonoBehaviour
{
    // 追従するスライム本体
    public Transform slimeTransform;

    // スライムからのオフセット（下方向）
    public Vector3 offset = new Vector3(0, -0.5f, 0);

    // 地面判定距離
    public float groundCheckDistance = 0.1f;

    // 地面のTag
    public string groundTag = "Ground";

    // 再生するParticleSystem
    public ParticleSystem particleSystem;

    // 前フレームの地面判定
    private bool wasGround = false;

    void Update()
    {
        if (slimeTransform == null) return;

        // -------------------------------
        // ■ スライム本体に追従
        // -------------------------------
        transform.position = slimeTransform.position + offset;

        // -------------------------------
        // ■ Raycastで地面判定
        // -------------------------------
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
        bool isGround = (hit.collider != null && hit.collider.CompareTag(groundTag));

        // -------------------------------
        // ■ 地面に着地した瞬間だけParticleSystem再生
        // -------------------------------
        if (!wasGround && isGround)
        {
            if (particleSystem != null)
                particleSystem.Play();
        }

        // 前フレームの判定更新
        wasGround = isGround;
    }

    // デバッグ用：RayをSceneビューに表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (slimeTransform != null)
        {
            Vector3 pos = slimeTransform.position + offset;
            Gizmos.DrawLine(pos, pos + Vector3.down * groundCheckDistance);
        }
    }
}
