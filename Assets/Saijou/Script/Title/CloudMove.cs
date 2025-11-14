using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float startX = -110; // 雲の開始位置
    public float endX = 50;  // 雲の終了位置
    public float speedMin = 1f; // 移動速度の最小値
    public float speedMax= 1f; // 移動速度の最大値
    
    private float moveSpeed; // 移動速度

    void Start()
    {
        //　ランダムに移動速度決定
        moveSpeed = Random.Range(speedMin, speedMax);

        // 雲の初期位置をランダムに決定
        transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // 横方向に移動
        float newX = transform.position.x + moveSpeed * Time.deltaTime;
        //Xがend超えたらStartに戻る
        if (newX > endX)
            newX = startX;
        // 新しい位置に設定
        transform.position = new Vector3(newX,transform.position.y,transform.position.z); 
    }
}
