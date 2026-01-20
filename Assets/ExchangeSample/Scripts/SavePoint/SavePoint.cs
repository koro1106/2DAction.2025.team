using UnityEngine;
using ExchangeSample.Scripts;   // Åöí«â¡

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerRespawn respawn =
            collision.GetComponentInParent<PlayerRespawn>();

        if (respawn == null) return;

        respawn.SetSavePoint(transform.position);
    }
}