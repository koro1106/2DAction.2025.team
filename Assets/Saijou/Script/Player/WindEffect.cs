using UnityEngine;
/// <summary>
/// 風の影響受けるクラス
/// </summary>
public class WindEffect : MonoBehaviour
{
    public Vector2 windDir = new Vector2(-1, 0); // 風の方向（左方向）
    public float windStrength = 2f; // 風の強さ
    private bool isWindBlowing = false; // 風吹いてるか
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 風の影響を与える（風が吹いていて、プレイヤーが風の影響範囲内にいるとき）
        if(isWindBlowing)
        {
            ApplyWindForce();
        }
    }

    // 風の力をプレイヤーに加える
    void ApplyWindForce()
    {
        rb.AddForce(windDir * windStrength);
    }

    // 風の状態を切り替える関数
    public void ToggleWind(bool isBlowing)
    {
        isWindBlowing = isBlowing;
    }

    // 風の影響を受けるエリアに入ったら風を吹かせる
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("WindArea")) // 風エリアに入ったら
        {
            ToggleWind(true); // 風が吹くようにする
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("WindArea")) // 風エリア出たら
        {
            ToggleWind(false); // 風止める
        }
    }
}
