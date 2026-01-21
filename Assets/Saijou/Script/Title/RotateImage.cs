using UnityEngine;
/// <summary>
/// 水車Imageの回転スクリプト
/// </summary>
public class RotateImage : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100f; // 度/秒

    void Update()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
