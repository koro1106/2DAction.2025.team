using UnityEngine;
/// <summary>
/// ボタンマネージャー
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameObject options;
    public GameObject userGuides;
    public StartPerformance startPf;
    //オプション画面
    public void OnOptionButton()
    {
        options.SetActive(true);
    }
    public void OnOptionBackButton()
    {
        options.SetActive(false);
    }
    //操作説明画面
    public void OnUserGideButton()
    {
        userGuides.SetActive(true);
    }
    public void OnUserGideBackButton()
    {
        userGuides.SetActive(false);
    }
    // ゲームスタート
    public void OnStartButton()
    {
        startPf.GameStart();
    }
}
