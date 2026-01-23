using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
/// <summary>
/// 壊れる床のクラス
/// </summary>
public class BreakingGround : MonoBehaviour
{
    public float fallDelay = 3f; // 落ちるまでの時間
    public float destroyDelay = 0.5f; // 消えるまでの時間
    public float respawnDelay = 3f;     // 復活までの時間
    public float shakeAmount = 0.8f;      // 揺れ幅（X方向）
    public float shakeSpeed =100f;      // 揺れるスピード

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isFalling = false;
    private Vector3 originalPos;
    public AudioManager audioManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        originalPos = transform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが踏んだか
        if(!isFalling && collision.gameObject.CompareTag("Player"))
        {
            audioManager.audioSource.PlayOneShot(audioManager.breakGround);
            isFalling = true;
            StartCoroutine(ShakeAndFall());
        }
    }
    IEnumerator ShakeAndFall()
    {
        float elapsed = 0f;  //どのぐらい経過したか

        while (elapsed < fallDelay)
        {
            float offsetX = Mathf.Sin(elapsed * shakeSpeed) * shakeAmount;
            transform.localPosition = originalPos + new Vector3(offsetX, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        // 位置を元に戻す
        transform.localPosition = originalPos;
        // 落下開始
        rb.bodyType = RigidbodyType2D.Dynamic;
        // 少し待って消える
        yield return new WaitForSeconds(destroyDelay);

        // 非表示＆当たり判定OFF
        col.enabled = false;
        rb.simulated = false;
        GetComponent<SpriteRenderer>().enabled = false;

        // 復活待ち
        yield return new WaitForSeconds(respawnDelay);

        Respawn();
    }
    void Respawn()
    {
        // 状態リセット
        transform.localPosition = originalPos;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Static;

        col.enabled = true;
        rb.simulated = true;
        GetComponent<SpriteRenderer>().enabled = true;

        isFalling = false;
    }
}
