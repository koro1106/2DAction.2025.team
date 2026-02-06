using UnityEngine;

public class StartPerformance : MonoBehaviour
{
    public Camera camera;
    public float endX = 0f;
    public  float moveSpeed = 5f; // 移動速度
    public bool preformanceFinished = false; // スタート演出終わったか
    public GameObject titleUI; // タイトルUI
    
    public CanvasGroup uiCanvasGroup_Title; // TitileUIのCanvasGroup
    public CanvasGroup uiCanvasGroup_Seeson; // 季節UIのCanvasGroup
    private float fadeSpeed = 0.5f; // フェード速度
    private bool isStart = false; // ゲーム開始されたかどうか
    private bool isMovingCamera = false; // カメラ移動が開始されたかどうか
   // private bool cursorHidden = false; // カーソルを一度だけ消す用


    [SerializeField] private GameObject player; // プレイヤー

    private void Start()
    {
        ResetPerformance(); // リセット
    }
    private void Update()
    {
        //　ゲーム開始ボタンでタイトルUIフェードアウト開始
        if(isStart && !preformanceFinished)
        {
            // UIのアルファ値を減少
            uiCanvasGroup_Title.alpha = Mathf.MoveTowards(uiCanvasGroup_Title.alpha, 0f, fadeSpeed * Time.deltaTime);
        
            // UIが透明になったらフェード終了
            if(uiCanvasGroup_Title.alpha == 0f)
            {
                isStart = false;
            }
        }

        //　ゲーム開始ボタンでゲームUIフェードイン開始
        if(isStart && !preformanceFinished)
        {
            uiCanvasGroup_Seeson.alpha = Mathf.MoveTowards(uiCanvasGroup_Seeson.alpha, 1f, fadeSpeed * Time.deltaTime);

            // UIが写ったら終了
            if(uiCanvasGroup_Seeson.alpha == 1f)
            {
                isStart = true;
            }
        }

        if (preformanceFinished) uiCanvasGroup_Title.alpha = 1f; // タイトル演出終わったら１に戻す

        //　ゲーム開始ボタンでカメラ移動開始
        if (isMovingCamera && !preformanceFinished)
        {
            // 右方向にカメラ移動
            float newX = camera.transform.position.x + moveSpeed * Time.deltaTime;
            camera.transform.position = new Vector3(newX, camera.transform.position.y, camera.transform.position.z);

            //end超えたらシーン遷移
            if (camera.transform.position.x > endX )
            {
                Debug.Log("目標地点到着");
                titleUI.SetActive(false); // タイトルUI非表示
                preformanceFinished = true; // スタート演出終了
                isMovingCamera = false;         // 移動停止

                // ここでマウスカーソルを消す
              // HideCursor();

            }
        }
       
    }

    public void GameStart()
    {
        ResetPerformance(); // リセット

      // スタートボタン押されたらフェード開始
       isStart = true;
       isMovingCamera = true;

        // プレイヤー表示
        player.SetActive(true);
    }

    // 演出用フラグ・状態をリセット
    void ResetPerformance()
    {
        preformanceFinished = false;
        isStart = false;
        isMovingCamera = false;
    }


    //void HideCursor()
    //{
    //    if (cursorHidden) return;

    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;

    //    cursorHidden = true;
    //}
}
