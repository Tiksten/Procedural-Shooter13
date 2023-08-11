using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float dmg = 20;

    [SerializeField]
    private float bulletSpeed = 15;

    [SerializeField]
    private float bulletSize = 1;

    [SerializeField]
    private int consumesAmmo = 1;

    private Ammo ammo;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float cycletime = 0.1f;

    [SerializeField]
    private float inaccuracy = 0.6f;

    [SerializeField]
    private int pellets = 5;

    private bool canFire;

    [SerializeField]
    private bool isFullAuto;

    private void OnEnable()
    {
        canFire = true;
        ammo = FindObjectOfType<Ammo>();
    }

    private void Update()
    {
        if (canFire && ammo.currAmmo > consumesAmmo)
        {
            if (Input.GetKey(KeyCode.Mouse0) && isFullAuto)
            {
                canFire = false;
                ammo.currAmmo-=consumesAmmo;
                for (var i = pellets; i > 0; i--)
                {
                    var b = Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, transform.up + (Vector3)Random.insideUnitCircle * inaccuracy)).GetComponent<Bullet>();
                    b.transform.localScale = Vector3.one * bulletSize;
                    b.dmg = dmg;
                    b.velocity = bulletSpeed;
                }
                Invoke("Reset", cycletime);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !isFullAuto)
            {
                canFire = false;
                ammo.currAmmo -= consumesAmmo;
                for (var i = pellets; i > 0; i--)
                {
                    var b = Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, transform.up + (Vector3)Random.insideUnitCircle * inaccuracy)).GetComponent<Bullet>();
                    b.transform.localScale = Vector3.one * bulletSize;
                    b.dmg = dmg;
                    b.velocity = bulletSpeed;
                }
                Invoke("Reset", cycletime);
            }
        }
    }

    private void Reset()
    {
        canFire = true;
    }
}
