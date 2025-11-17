using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// マウスに追従するワールド上のクロスヘア
/// - カーソル非表示
/// - カメラ視界外へ出ないように制限（可変マージン）
public class Crosshair : MonoBehaviour
{
    [Tooltip("カメラ参照。空欄なら Camera.main を使う")]
    public Camera cam;

    [Tooltip("カーソルを非表示にするか")]
    public bool hideSystemCursor = true;

    [Tooltip("クロスヘアの動きを滑らかにする（0 = 直接移動）")]
    [Range(0f, 0.2f)]
    public float smoothTime = 0.02f;

    [Tooltip("カメラ端からどれだけ内側に制限するか（ワールド単位）")]
    public float margin = 0.1f;

    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        if (cam == null) cam = Camera.main;
        if (hideSystemCursor) Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // カメラのビューボーダー取得（ワールド座標）
        Vector3 bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 topRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

        // 制限（margin を考慮）
        float minX = bottomLeft.x + margin;
        float minY = bottomLeft.y + margin;
        float maxX = topRight.x - margin;
        float maxY = topRight.y - margin;

        Vector3 target = mouseWorld;
        target.x = Mathf.Clamp(target.x, minX, maxX);
        target.y = Mathf.Clamp(target.y, minY, maxY);

        if (smoothTime <= 0f)
            transform.position = target;
        else
            transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }

    void OnDisable()
    {
        if (hideSystemCursor) Cursor.visible = true;
    }
}
