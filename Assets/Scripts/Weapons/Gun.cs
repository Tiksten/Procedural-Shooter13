using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float dmg = 5;

    public float cycletime = 0.1f;

    public bool isFullAuto = false;

    public int pellets = 1;

    public float inaccuracy = 0.1f;

    [Space(10)]

    public int price = 0;

    public int consumesAmmo = 1;

    public float shakeAmplitude = 0.1f;

    [Space(10)]

    public GameObject bullet;

    public float bulletSpeed = 15;

    public float bulletSize = 0.1f;

    public float bulletDist = 0.5f;

    public GunShotSound gunShotSound;

    public GunEquipSound gunEquipSound;


    public enum GunShotSound
    {
        Pistol,
        Rifle,
        Deagle,
        Shotgun,
        BigGun
    }

    public enum GunEquipSound
    {
        Pistol,
        Revolver,
        Shotgun
    }


    private Ammo ammo;

    private AudioSource audioSource;

    private bool canFire;


    private void Awake()
    {
        if (price > 0 && transform.parent.GetComponent<WeaponSwitch>() == null)
        {
            gameObject.AddComponent<Price>().price = price;
            price = 0;
        }
    }

    private void OnDisable()
    {
        if (GetComponent<Price>() == null && price > 0 && transform.parent.GetComponent<WeaponSwitch>() == null)
        {
            gameObject.AddComponent<Price>().price = price;
            price = 0;
        }

        Destroy(gameObject.GetComponent<AudioSource>());
    }

    private void OnEnable()
    {
        if (transform.parent.GetComponent<WeaponSwitch>() == null)
        {
            return;
        }

        canFire = true;
        ammo = FindObjectOfType<Ammo>();

        GetComponentInParent<Hand>().rend = GetComponent<SpriteRenderer>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Guns/Equip/" + gunEquipSound.ToString()));
    }

    private void Update()
    {
        if (canFire && ammo.currAmmo > consumesAmmo)
        {
            if (Input.GetKey(KeyCode.Mouse0) && isFullAuto)
            {
                Fire();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !isFullAuto)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Guns/" + gunShotSound.ToString() + "/" + Random.Range(1, 4).ToString()));

        canFire = false;
        ammo.currAmmo -= consumesAmmo;

        for (var i = pellets; i > 0; i--)
        {
            Quaternion newBulletRotation = Quaternion.FromToRotation(Vector3.up, transform.right + (Vector3)Random.insideUnitCircle * inaccuracy);

            CameraShaker.Instance.Recoil(newBulletRotation * Vector3.up, shakeAmplitude);

            var b = Instantiate(bullet, transform.position + transform.right * bulletDist, newBulletRotation).GetComponent<Bullet>();

            b.transform.localScale = b.transform.localScale * bulletSize;
            b.dmg = dmg;
            b.velocity = bulletSpeed;

            Destroy(b, 2);
        }
        Invoke("Reset", cycletime);
    }

    private void Reset()
    {
        canFire = true;
    }
}
