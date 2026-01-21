using UnityEngine;
using UnityEngine.Playables;
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
    [SerializeField] private PlayerHP　playerHP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Clear")) return;

        clearD.Play();

        if (playerHP.currentHP >= 80) clearA.Play();
        else if (playerHP.currentHP >= 50) clearB.Play();
        else if (playerHP.currentHP >= 30) clearC.Play();
        else if(playerHP.currentHP < 30) clearD.Play();
      
        // プレイヤーを無効化
        player.SetActive(false);
    }
}
