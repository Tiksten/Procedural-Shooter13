using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private int value = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Progress.raidCoins += value;
            FindObjectOfType<Coins>().UpdateText();
            Destroy(Instantiate(Resources.Load<GameObject>("Prefabs/Effects/CollectCoin"), transform.position, Quaternion.identity), 1);
            Destroy(gameObject);
        }
    }
}
