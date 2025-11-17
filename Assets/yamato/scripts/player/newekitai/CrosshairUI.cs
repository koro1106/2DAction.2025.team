using UnityEngine;

public class CrosshairUI : MonoBehaviour
{
    public RectTransform crosshair; // Crosshair Image の RectTransform
    public Canvas canvas;           // Crosshair が属する Canvas（Inspector でセット）

    void Start()
    {
        if (canvas == null)
        {
            // シーンに Canvas が一つなら自動で取る（複数ある場合は Inspector で指定推奨）
            canvas = GetComponentInParent<Canvas>();
        }

        // マウスカーソルを消す（要るなら true）
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        // Screen Space - Overlay の場合、最も簡単に位置をセットできる：
        if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            crosshair.position = mousePos;
            return;
        }

        // もし Canvas が Screen Space - Camera や World Space の場合の安全ルート
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            mousePos,
            canvas.worldCamera,
            out Vector2 localPoint);

        crosshair.anchoredPosition = localPoint;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }
}
