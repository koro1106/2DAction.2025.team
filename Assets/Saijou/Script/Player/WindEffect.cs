using UnityEngine;
/// <summary>
/// 風の影響受けるクラス
/// </summary>
public class WindEffect : MonoBehaviour
{
    public float windStrength = 2f; // 風の強さ
    private bool isWindBlowing = false; // 風吹いてるか
    private Rigidbody2D rb;
    public GameObject[] wind;
    private WindDirection currentWind; // 今影響を受けている風
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
        rb.AddForce(currentWind.windDir * windStrength);
    }

    // 風の状態を切り替える関数
    public void ToggleWind(bool isBlowing)
    {
        isWindBlowing = isBlowing;
    }

    // 風の影響を受けるエリアに入ったら風を吹かせる
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.CompareTag("WindArea")) // 風エリアに入ったら
        //{
        //    ToggleWind(true); // 風が吹くようにする
        //    Debug.Log("エリア入った");

        //}
        // wind配列の中から、どの風エリアに入ったか調べる
        for (int i = 0; i < wind.Length; i++)
        {
            if (other.gameObject == wind[i])
            {
                currentWind = wind[i].GetComponent<WindDirection>();
                ToggleWind(true);
                Debug.Log($"風エリア {i + 1} に入った。方向: {currentWind.windDir}");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if(other.CompareTag("WindArea")) // 風エリア出たら
        //{
        //    ToggleWind(false); // 風止める
        //}
        for (int i = 0; i < wind.Length; i++)
        {
            if (other.gameObject == wind[i])
            {
                ToggleWind(false);
                currentWind = null;
                Debug.Log($"風エリア {i + 1} から出た");
            }
        }
    }
}
