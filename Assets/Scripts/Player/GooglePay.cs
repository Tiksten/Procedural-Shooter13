using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GooglePay : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private Price currSelected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currSelected != null)
        {
            if(Player.inRaid)
            {
                if (currSelected.price <= Progress.raidCoins)
                {
                    Progress.raidCoins -= currSelected.price;
                    Destroy(currSelected);
                    Coins.instance.UpdateText();
                }
            }
            else
            {
                if (currSelected.price <= Progress.playerInfo.coins)
                {
                    Progress.playerInfo.coins -= currSelected.price;
                    Destroy(currSelected);
                    Coins.instance.UpdateText();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currSelected == null && collision.GetComponent<Price>()!=null)
        {
            currSelected = collision.GetComponent<Price>();

            text.text = (currSelected.name + " Price:" + currSelected.GetComponent<Price>().price.ToString());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currSelected == collision.GetComponent<Price>())
        {
            currSelected = null;
            text.text = "";
        }
    }
}
