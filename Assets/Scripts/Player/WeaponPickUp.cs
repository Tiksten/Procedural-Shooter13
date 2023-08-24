using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private Transform hand;

    private Transform currSelected;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currSelected != null)
        {
            if(currSelected.GetComponent<Price>()!=null)
            {
                if (currSelected.GetComponent<Price>().price > PlayerPrefs.GetInt("Coins", 0))
                    return;
            }

            SwitchGuns();
        }
    }

    private void SwitchGuns()
    {
        if (currSelected.GetComponent<Price>() != null)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) - currSelected.GetComponent<Price>().price);
            Destroy(currSelected.GetComponent<Price>());
            FindObjectOfType<Coins>().UpdateText();
        }

        Transform gunToDrop = null;

        foreach(Transform gun in hand.transform)
        {
            if(gun.gameObject.activeInHierarchy)
            {
                gunToDrop = gun;
                break;
            }
        }

        foreach (MonoBehaviour comp in gunToDrop.GetComponents<MonoBehaviour>())
        {
            if(!(comp is SpriteRenderer || comp is BoxCollider2D))
                comp.enabled = false;
        }

        gunToDrop.parent = null;
        gunToDrop.position = currSelected.position;
        gunToDrop.rotation = currSelected.rotation;
        gunToDrop.localScale = Vector3.one;
        gunToDrop.AddComponent<BoxCollider2D>().isTrigger = true;
        gunToDrop.GetComponent<SpriteRenderer>().sortingLayerName = "Loot";

        currSelected.transform.parent = hand.transform;

        foreach (MonoBehaviour comp in currSelected.GetComponents<MonoBehaviour>())
            comp.enabled = true;

        currSelected.transform.localPosition = Vector3.zero;
        currSelected.transform.localRotation = Quaternion.identity;
        currSelected.GetComponent<SpriteRenderer>().sortingLayerName = "Defalut";
        Destroy(currSelected.GetComponent<BoxCollider2D>());

        SceneManager.MoveGameObjectToScene(currSelected.gameObject, SceneManager.GetActiveScene());

        currSelected = null;
    }
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && currSelected == null)
        {
            currSelected = collision.transform;

            if (currSelected.GetComponent<Price>() != null)
                text.text = (currSelected.name + " Price:" + currSelected.GetComponent<Price>().price.ToString());
            else
                text.text = currSelected.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && currSelected == collision.transform)
        {
            currSelected = null;
            text.text = "";
        }
    }
}
