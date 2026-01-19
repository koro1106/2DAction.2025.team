using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!collision.CompareTag("Player")) return;

            PlayerRespawn respawn = collision.GetComponent<PlayerRespawn>();
            PlayerStateController state = collision.GetComponent<PlayerStateController>();

            if (respawn == null || state == null) return;

            // 気体・液体・固体すべてでセーブ
            respawn.SetSavePoint(transform.position);
            Debug.Log("セーブ更新 : " + state.currentState);
        }
    }
}
