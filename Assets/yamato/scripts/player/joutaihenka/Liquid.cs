using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour
{
    [SerializeField] private Character character;

    public void OnTriggerEnter2D(Collider2D other)
    {
        character.ChangeCharacter(other.gameObject.tag);
    }
}
