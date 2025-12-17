//using UnityEngine;
///// <summary>
///// プレイヤーに風の力を与えるクラス
///// </summary>
//namespace ExchangeSample.Scripts
//{
//    public class WindEffect : MonoBehaviour
//    {
//        public float windStrength = 2f; // 風の強さ
//        private Rigidbody2D rb;
//        public GameObject[] wind;
//        private WindDirection currentWind; // 今影響を受けている風
//        public GasPlayerController player;
//        void Start()
//        {
//            rb = GetComponent<Rigidbody2D>();
//        }

//        void FixedUpdate()
//        {
//            // 風の影響を与える（風が吹いていて、プレイヤーが風の影響範囲内にいるとき）
//            if (currentWind != null)
//            {
//                Vector2 force = currentWind.windDir.normalized * windStrength;
//                player.GetComponent<Rigidbody2D>()
//                      .AddForce(force, ForceMode2D.Force);
//            }
//        }

//        // 風の影響を受けるエリアに入ったら風を吹かせる
//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            for (int i = 0; i < wind.Length; i++)
//            {
//                if (other.gameObject == wind[i])
//                {
//                    currentWind = wind[i].GetComponent<WindDirection>();
//                    player.SetWindState(true);
//                    Debug.Log($"風エリア {i + 1} に入った。方向: {currentWind.windDir}");
//                }
//            }
//        }

//        private void OnTriggerExit2D(Collider2D other)
//        {
//            for (int i = 0; i < wind.Length; i++)
//            {
//                if (other.gameObject == wind[i])
//                {
//                    currentWind = null;
//                    player.SetWindState(false);
//                    Debug.Log($"風エリア {i + 1} から出た");
//                }
//            }
//        }
//    }
//}
//using UnityEngine;

//namespace ExchangeSample.Scripts
//{
//    /// <summary>
//    /// プレイヤーが風を受けるためのクラス
//    /// </summary>
//    [RequireComponent(typeof(Rigidbody2D))]
//    public class WindEffect : MonoBehaviour
//    {
//        public float windStrength = 20f;

//        private Rigidbody2D rb;
//        private WindDirection currentWind;

//        private void Awake()
//        {
//            rb = GetComponent<Rigidbody2D>();
//        }

//        private void FixedUpdate()
//        {
//            if (currentWind == null) return;

//            Vector2 force =
//                currentWind.windDir.normalized * windStrength;

//            rb.AddForce(force, ForceMode2D.Force);
//        }

//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            // WindDirectionついてるか調べる
//            WindDirection wind = other.GetComponent<WindDirection>();
//            if (wind == null) return;

//            currentWind = wind;

//            GasPlayerController player =
//                GetComponent<GasPlayerController>();
//            if (player != null)
//                player.SetWindState(true);

//            Debug.Log($"風に入った: {wind.windDir}");
//        }

//        private void OnTriggerExit2D(Collider2D other)
//        {
//            // WindDirectionついてるか調べる
//            WindDirection wind = other.GetComponent<WindDirection>();
//            if (wind == null) return;

//            if (currentWind == wind)
//            {
//                currentWind = null;

//                GasPlayerController player =
//                    GetComponent<GasPlayerController>();
//                if (player != null)
//                    player.SetWindState(false);

//                Debug.Log("風から出た");
//            }
//        }
//    }
//}
using UnityEngine;

namespace ExchangeSample.Scripts
{
    public class WindEffect : MonoBehaviour
    {
        public float windStrength = 2f;           // 風の強さ
        public GameObject[] windAreas;            // 風エリアのオブジェクト

        private WindDirection currentWind;        // 現在影響を受けている風
        private GasPlayerController player;       // プレイヤー

        private void Start()
        {
            player = GetComponent<GasPlayerController>();
        }

        private void FixedUpdate()
        {
            //if (currentWind != null && player != null)
            //{
            //    // 風方向に力を加える
            //    Vector2 force = currentWind.windDir.normalized * windStrength;
            //    player.GetComponent<Rigidbody2D>()
            //          .AddForce(force, ForceMode2D.Force);
            //}
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //WindDirection wind = other.GetComponent<WindDirection>();
            //if (wind == null) return;

            //currentWind = wind;

            //if (player != null)
            //    player.SetWindState(true);

            //Debug.Log($"風エリアに入った: {wind.windDir}");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //WindDirection wind = other.GetComponent<WindDirection>();
            //if (wind == null) return;

            //if (currentWind == wind)
            //{
            //    currentWind = null;

            //    if (player != null)
            //        player.SetWindState(false);

            //    Debug.Log($"風エリアから出た");
            //}
        }
    }
}
