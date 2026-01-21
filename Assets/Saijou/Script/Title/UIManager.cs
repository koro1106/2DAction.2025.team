using UnityEngine;
/// <summary>
/// ボタンマネージャー
/// </summary>
public class UIManager : MonoBehaviour
{
    public GameObject options;
    public StartPerformance startPf;
    public UIMove move;
    public AudioManager audioManager;

    public void OnUserGideButton()
    {
        move.upMoving = true;
        audioManager.audioSource.PlayOneShot(audioManager.userGide);

    }
    public void OnUserGideBackButton()
    {
        move.downMoving = true;
    }
    // ゲームスタート
    public void OnStartButton()
    {
        startPf.GameStart();
        audioManager.audioSource.PlayOneShot(audioManager.start);
    }
}
