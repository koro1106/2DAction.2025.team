using UnityEngine;

public class CrossHairUI : MonoBehaviour
{
    public Transform aimPoint;
    public RectTransform crossHairRect;
    public Canvas canvas;

    void Update()
    {
        Vector3 screen = Camera.main.WorldToScreenPoint(aimPoint.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screen,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out Vector2 pos
        );

        crossHairRect.localPosition = pos;
    }
}
