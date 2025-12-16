using UnityEngine;
/// <summary>
/// 風の影響受けるクラス
/// </summary>
namespace ExchangeSample.Scripts
{
    public class WindEffect : MonoBehaviour
    {
        public float windStrength = 2f; // 風の強さ
        private bool isWindBlowing = false; // 風吹いてるか
        private Rigidbody2D rb;
        public GameObject[] wind;
        private WindDirection currentWind; // 今影響を受けている風
        public GasPlayerController player;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            // 風の影響を与える（風が吹いていて、プレイヤーが風の影響範囲内にいるとき）
            if (isWindBlowing && currentWind != null)
            {
                player.AddExternalVelocity(
                     currentWind.windDir * windStrength * Time.fixedDeltaTime
                );
            }
        }

        // 風の状態を切り替える関数
        public void ToggleWind(bool isBlowing)
        {
            isWindBlowing = isBlowing;
        }

        // 風の影響を受けるエリアに入ったら風を吹かせる
        private void OnTriggerEnter2D(Collider2D other)
        {
            for (int i = 0; i < wind.Length; i++)
            {
                if (other.gameObject == wind[i])
                {
                    currentWind = wind[i].GetComponent<WindDirection>();
                    ToggleWind(true);
                    player.SetWindState(true);
                    Debug.Log($"風エリア {i + 1} に入った。方向: {currentWind.windDir}");
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            for (int i = 0; i < wind.Length; i++)
            {
                if (other.gameObject == wind[i])
                {
                    ToggleWind(false);
                    currentWind = null;
                    player.SetWindState(false);
                    Debug.Log($"風エリア {i + 1} から出た");
                }
            }
        }
    }
}
