using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SocialPlatforms.Impl;
/// <summary>
/// クリア演出クラス
/// </summary>
public class ClearPerformance : MonoBehaviour
{
    // タイムライン
    [SerializeField] private PlayableDirector clearA; // 一番評価高い
    [SerializeField] private PlayableDirector clearB;
    [SerializeField] private PlayableDirector clearC;
    [SerializeField] private PlayableDirector clearD; // 一番評価引くい

    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Clear")) return;

        clearD.Play();

        //if (hp > 1000) clearA.Play();
        //else if (hp > 500) clearB.Play();
        //else if (hp > 100) clearC.Play();
        //else clearD.Play();

      
        // プレイヤーを無効化
        player.SetActive(false);
    }

   
}
