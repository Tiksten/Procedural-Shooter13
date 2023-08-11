using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField]
    private Transform hand;

    private Transform currSelected;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && currSelected != null)
        {
            SwitchGuns();
        }
    }

    private void SwitchGuns()
    {
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
        gunToDrop.AddComponent<BoxCollider2D>().isTrigger = true;

        foreach (MonoBehaviour comp in currSelected.GetComponents<MonoBehaviour>())
            comp.enabled = true;

        currSelected.transform.parent = hand.transform;
        currSelected.transform.localPosition = Vector3.zero;
        currSelected.transform.localRotation = Quaternion.identity;
        Destroy(currSelected.GetComponent<BoxCollider2D>());

        currSelected = gunToDrop;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && currSelected == null)
        {
            currSelected = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Weapon" && currSelected == collision.transform)
        {
            currSelected = null;
        }
    }
}
