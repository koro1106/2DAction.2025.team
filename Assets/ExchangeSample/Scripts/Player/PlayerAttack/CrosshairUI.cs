using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // マウス位置（スクリーン座標）
        Vector2 mousePos = Input.mousePosition;

        // RectTransform をスクリーン座標に合わせる
        rectTransform.position = mousePos;
    }
}
