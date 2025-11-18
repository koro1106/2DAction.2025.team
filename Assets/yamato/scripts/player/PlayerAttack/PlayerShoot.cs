using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 0.5f;

    float timer;
    // Start is called before the first frame update
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer > shootCooldown)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // ƒ}ƒEƒX•ûŒü‚É”ò‚Î‚·—á
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePos - (Vector2)firePoint.position);

        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}
