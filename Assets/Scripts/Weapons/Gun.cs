using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour, IPickable
{
    [SerializeField]
    private float dmg = 5;

    [SerializeField]
    private float cycletime = 0.1f;

    [SerializeField]
    private bool isFullAuto = false;

    [SerializeField] 
    private int pellets = 1;

    [SerializeField] 
    private float inaccuracy = 0.1f;

    [Space(10)]

    [SerializeField]
    private int consumesAmmo = 1;

    [SerializeField]
    private float shakeAmplitude = 0.1f;

    [Space(10)]

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float bulletSpeed = 15;

    [SerializeField]
    private float bulletSize = 0.1f;

    [SerializeField]
    private float bulletDist = 0.5f;

    [SerializeField]
    private GunShotSound gunShotSound;

    [SerializeField]
    private GunEquipSound gunEquipSound;

    [SerializeField]
    private float bulletLiveTime = 2;


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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ammo = FindObjectOfType<Ammo>();

        if (transform.parent.GetComponent<WeaponSwitch>() != null)
            OnPickUp();
        else
            OnDrop();
    }

    public void OnDrop()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        canFire = true;
    }

    public void OnPickUp()
    {
        enabled = true;

        if (transform.parent.GetComponent<WeaponSwitch>() == null)
        {
            return;
        }

        canFire = true;

        

        audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Guns/Equip/" + gunEquipSound.ToString()));
    }

    private void Update()
    {
        if (!Progress.canAct) return;

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

            var go = Instantiate(bullet, transform.position + transform.right * bulletDist, newBulletRotation);

            if (go.GetComponent<Bullet>() != null)
            {
                var b = go.GetComponent<Bullet>();

                b.transform.localScale = b.transform.localScale * bulletSize;
                b.dmg = dmg;
                b.velocity = bulletSpeed;
            }

            Destroy(go, bulletLiveTime);
        }
        Invoke("Reset", cycletime);
    }

    private void Reset()
    {
        canFire = true;
    }
}
