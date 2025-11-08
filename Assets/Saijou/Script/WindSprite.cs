using UnityEngine;

public class WindSprite: MonoBehaviour
{
    public Vector2 windDirection = new Vector2(-1, 0);  // 風の方向（左方向）
    public float windSpeed = 2f;  // 風の速さ（風スプライトの動き）

    // 風のスプライトを動かす
    void Update()
    {
        transform.Translate(windDirection * windSpeed * Time.deltaTime);
    }

}
