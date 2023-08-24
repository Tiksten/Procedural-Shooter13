using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public SettingsStructLib.GunData stats;


    private Ammo ammo;

    private bool canFire;

    private void OnEnable()
    {
        if (transform.parent == null)
            return;

        canFire = true;
        ammo = FindObjectOfType<Ammo>();

        GetComponentInParent<Hand>().rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (canFire && ammo.currAmmo > stats.consumesAmmo)
        {
            if (Input.GetKey(KeyCode.Mouse0) && stats.isFullAuto)
            {
                canFire = false;
                ammo.currAmmo-= stats.consumesAmmo;
                
                for (var i = stats.pellets; i > 0; i--)
                {
                    Quaternion newBulletRotation = Quaternion.FromToRotation(Vector3.up, transform.right + (Vector3)Random.insideUnitCircle * stats.inaccuracy);

                    CameraShaker.Instance.Recoil(newBulletRotation * Vector3.up, stats.shakeAmplitude);

                    var b = Instantiate(stats.bullet, transform.position, newBulletRotation).GetComponent<Bullet>();
                    
                    b.transform.localScale = Vector3.one * stats.bulletSize;
                    b.dmg = stats.dmg;
                    b.velocity = stats.bulletSpeed;
                }
                Invoke("Reset", stats.cycletime);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !stats.isFullAuto)
            {
                canFire = false;
                ammo.currAmmo -= stats.consumesAmmo;
                
                for (var i = stats.pellets; i > 0; i--)
                {
                    Quaternion newBulletRotation = Quaternion.FromToRotation(Vector3.up, transform.right + (Vector3)Random.insideUnitCircle * stats.inaccuracy);

                    CameraShaker.Instance.Recoil(newBulletRotation * Vector3.up, stats.shakeAmplitude);

                    var b = Instantiate(stats.bullet, transform.position, newBulletRotation).GetComponent<Bullet>();

                    b.transform.localScale = Vector3.one * stats.bulletSize;
                    b.dmg = stats.dmg;
                    b.velocity = stats.bulletSpeed;
                }
                Invoke("Reset", stats.cycletime);
            }
        }
    }

    private void Reset()
    {
        canFire = true;
    }
}
