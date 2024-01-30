using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Progress.playerInfo.coins += 5;
            FindObjectOfType<Coins>().UpdateText();
            Destroy(gameObject);
        }
    }
}
