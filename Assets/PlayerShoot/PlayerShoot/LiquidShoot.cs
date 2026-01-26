using UnityEngine;

public class LiquidShoot : MonoBehaviour
{
    public GameObject waterBallPrefab;
    public Transform shootPoint;
    public RectTransform crossHairRect;
    public float shootPower = 8f;

   // private PlayerState state;

    void Start()
    {
        //state = GetComponent<PlayerState>();
    }

    void Update()
    {
        //if (state.currentState != PlayerState.State.Liquid) return;

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // UIクロスヘア → スクリーン座標
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(
            null,
            crossHairRect.position
        );

        // ★ ここが重要：Z距離を正しく指定
        float zDistance = -Camera.main.transform.position.z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, zDistance)
        );
        worldPos.z = 0f;

        GameObject ball = Instantiate(
            waterBallPrefab,
            shootPoint.position,
            Quaternion.identity
        );

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        Vector2 dir = (worldPos - shootPoint.position);

        rb.velocity = dir.normalized * shootPower;

        // デバッグ用（赤線で確認）
        Debug.DrawLine(shootPoint.position, worldPos, Color.red, 1f);
    }

}
