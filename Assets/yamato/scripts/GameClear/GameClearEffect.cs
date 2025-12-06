using UnityEngine;
using UnityEngine.UI;

public class GameClearEffect : MonoBehaviour
{
    [Header("表示するクリア画像（1,2,3）")]
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;

    [Header("画面中央に表示するImage")]
    public Image centerImage;

    [Header("GasPlayer（HP情報）")]
    public GasPlayer player;

    public void ShowEffect()
    {
        float hpRatio = player.currentHP / player.maxHP;

        // 表示するスプライトを決める
        if (hpRatio < 0.3f)
        {
            centerImage.sprite = sprite1;
        }
        else if (hpRatio < 0.5f)
        {
            centerImage.sprite = sprite2;
        }
        else
        {
            centerImage.sprite = sprite3;
        }

        // 画像を表示
        centerImage.enabled = true;

        Debug.Log("クリア画像を中央に表示");
    }
}
