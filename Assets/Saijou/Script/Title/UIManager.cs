using UnityEngine;
/// <summary>
/// ボタンマネージャー
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameObject options;
    public StartPerformance startPf;
    public UIMove move;

   
    
    public void OnUserGideButton()
    {
        move.upMoving = true;
    }
    public void OnUserGideBackButton()
    {
        move.downMoving = true;
    }
    // ゲームスタート
    public void OnStartButton()
    {
        startPf.GameStart();
        Debug.Log("スタートボタン押された");
    }
}
