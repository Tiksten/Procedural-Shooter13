using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + 5);
            FindObjectOfType<Coins>().UpdateText();
            Destroy(gameObject);
        }
    }
}
