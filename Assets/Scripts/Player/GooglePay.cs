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
            if(Progress.inRaid)
            {
                if (currSelected.price <= Progress.raidCoins)
                {
                    var cl = currSelected.GetComponent<Collider2D>();
                    Progress.raidCoins -= currSelected.price;
                    Destroy(currSelected);
                    cl.enabled = false;
                    cl.enabled = true;
                    Coins.instance.UpdateText();
                }
            }
            else
            {
                if (currSelected.price <= Progress.playerInfo.coins)
                {
                    var cl = currSelected.GetComponent<Collider2D>();
                    Progress.playerInfo.coins -= currSelected.price;
                    Destroy(currSelected);
                    cl.enabled = false;
                    cl.enabled = true;
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

            text.text = ("Нажмите Е чтобы купить " + currSelected.name + " за " + currSelected.GetComponent<Price>().price.ToString() + " золота");
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
