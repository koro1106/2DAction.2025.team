using UnityEngine;
/// <summary>
/// ボタンマネージャー
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameObject options;
    public GameObject userGuides;
    public StartPerformance startPf;
    public UIMove move;

   
    //オプション画面
    public void OnOptionButton()
    {
        move.upMoving = true;
        //options.SetActive(true);

    }
    public void OnOptionBackButton()
    {
        move.downMoving = true;
        //options.SetActive(false);
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
        Debug.Log("スタートボタン押された");
    }
}
