using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    public Transform cameraTransform; // カメラ
    public RectTransform seasonUI; // 季節UI
    public float rotateSpeed = 50f; // 回転スピード
    public Transform[] passPoints; // 春・夏・秋の通過地点

    private bool[] passed; // 各地点通過したか
    private float targetRotateZ = 0f; // 目標の最終角度
    private void Start()
    {
        passed = new bool[passPoints.Length];
    }
    void Update()
    {
        // 各ポイントを通過したかチェック
        for(int i = 0; i < passPoints.Length; i++)
        {
            if (!passed[i] && cameraTransform.position.x >= passPoints[i].position.x)
            {
                passed[i] = true;
                // 90度回転
                targetRotateZ -= 90f;
            }
        }

        // UIを targetRotateZ に向けてスムーズに回転
        float currentZ = seasonUI.localEulerAngles.z;
        // 一定速度で目標角度へ近づける
        float newZ = Mathf.MoveTowardsAngle(currentZ, targetRotateZ, rotateSpeed * Time.deltaTime);
        seasonUI.localRotation = Quaternion.Euler(0, 0, newZ);
    }
}
