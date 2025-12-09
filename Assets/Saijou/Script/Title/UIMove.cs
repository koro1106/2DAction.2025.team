using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// オプション移動クラス
/// </summary>
public class UIMove : MonoBehaviour
{
    public float startX = 0f;   // 開始位置
    public float startY = -1100f;
    public float targetX = 0f; // 目標位置
    public float targetY = 0f;
    public float speed = 50f;   // 移動スピード
    public bool upMoving = false;
    public bool downMoving = false;

    RectTransform rect;
    Vector2 targetPos;
    Vector2 startPos;
    void Start()
    {
       rect = GetComponent<RectTransform>();
       // 開始位置に配置
       rect.anchoredPosition = new Vector2(startX, startY);
       // 目標位置セット 
       targetPos = new Vector2(targetX, targetY);
       // 開始位置セット 
       startPos = new Vector2(startX, startY);
    }

    void Update()
    {
        if (upMoving) UpMove();
        if (downMoving) DownMove();
    }

    void UpMove()
    {
        // StartからTargetへ
        rect.anchoredPosition = Vector2.MoveTowards(
            rect.anchoredPosition, targetPos, speed * Time.deltaTime);

        // 到達判定
        if (Vector2.Distance(rect.anchoredPosition, targetPos) < 0.1f)
        {
            upMoving = false;
        }
    }
    void DownMove()
    {
        // TargetからStartへ
        rect.anchoredPosition = Vector2.MoveTowards(
            rect.anchoredPosition, startPos, speed * Time.deltaTime);

        // 到達判定
        if (Vector2.Distance(rect.anchoredPosition, startPos) < 0.1f)
        {
            downMoving = false;
        }
    }
}
