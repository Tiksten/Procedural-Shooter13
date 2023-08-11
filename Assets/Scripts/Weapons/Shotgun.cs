using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private int pellets = 5;

    [SerializeField]
    private float inaccuracy = 0.6f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            for(var i = pellets; i >= 0; i--)
            {
                Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, transform.up + (Vector3)Random.insideUnitCircle*inaccuracy));
            }
        }
    }
}
