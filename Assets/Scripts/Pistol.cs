using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            Instantiate(bullet, transform.position, transform.rotation);
    }
}
