using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPointController : MonoBehaviour
{
    public Transform aimPoint;

    void Update()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(
            new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z
            )
        );
        world.z = 0f;

        aimPoint.position = world;
    }
}
