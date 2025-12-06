using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearTrigger : MonoBehaviour
{
    public GameClearEffect clearEffect;

   private void OnTrrigerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<GasPlayer>() != null)
        {
            Debug.Log("ÉQÅ[ÉÄÉNÉäÉA");
            clearEffect.ShowEffect();
        }
    }
}
