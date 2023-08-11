using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float cycletime = 0.1f;

    [SerializeField]
    private float inaccuracy = 0.6f;

    private bool canFire;

    private void OnEnable()
    {
        canFire = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, transform.up + (Vector3)Random.insideUnitCircle * inaccuracy));
            Invoke("Reset", cycletime);
        }
    }

    private void Reset()
    {
        canFire = true;
    }
}
