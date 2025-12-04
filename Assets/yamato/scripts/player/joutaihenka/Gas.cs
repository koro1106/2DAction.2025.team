using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    [SerializeField] private Character character;
    //  “–‚½‚è”»’è
    public void OnTriggerEnter2D(Collider2D other)
    {
        character.ChangeCharacter(other.gameObject.tag);
    }
}
