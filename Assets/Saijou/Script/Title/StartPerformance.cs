using UnityEngine;

public class StartPerformance : MonoBehaviour
{
    public Camera camera;
    public float endX = 0f;
    public  float moveSpeed = 5f; // 移動速度
    public bool preformanceFinished = false; // スタート演出終わったか
    public GameObject titleUI; // タイトルUI
    
    public CanvasGroup uiCanvasGroup; // UIのCanvasGroup
    private float fadeSpeed = 0.5f; // フェード速度
    private bool isFading = false; // フェードが開始されたかどうか
    private bool isMovingCamera = false; // カメラ移動が開始されたかどうか

    private void Update()
    {
        //　ゲーム開始ボタンでフェードアウト開始
        if(isFading && !preformanceFinished)
        {
            // UIのアルファ値を減少
            uiCanvasGroup.alpha = Mathf.MoveTowards(uiCanvasGroup.alpha, 0f, fadeSpeed * Time.deltaTime);
        
            // UIが透明になったらフェード終了
            if(uiCanvasGroup.alpha == 0f)
            {
                isFading = false;
            }
        }

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
                // タイトルUI非表示
                titleUI.SetActive(false); ;
                preformanceFinished = true; // スタート演出終了
            }
        }
       
    }

    public void GameStart()
    {
      // スタートボタン押されたらフェード開始
      if(!isFading)
      {
         isFading = true;
         isMovingCamera = true;
      }
    }
}
