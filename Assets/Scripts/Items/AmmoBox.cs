using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField]
    private int addAmmo = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Ammo>().currAmmo += addAmmo;

            Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/Effects/CollectCoin"), transform.position, Quaternion.identity), 1);
            Destroy(gameObject);
        }
    }
}
