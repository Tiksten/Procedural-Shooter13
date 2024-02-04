using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public interface IPickable
{
    public void OnPickUp();
    public void OnDrop();
}

public class WeaponPickUp : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private Transform hand;

    private GameObject currSelected;

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

        gunToDrop.SetParent(null);
        gunToDrop.SetPositionAndRotation(currSelected.transform.position, currSelected.transform.rotation);
        gunToDrop.GetComponent<BoxCollider2D>().enabled = true;
        gunToDrop.GetComponent<SpriteRenderer>().sortingLayerName = "Loot";

        var comps1 = gunToDrop.GetComponents<IPickable>();

        if(comps1!=null)
        {
            foreach(var comp in comps1)
            {
                comp.OnDrop();
            }
        }





        currSelected.transform.SetParent(hand.transform);
        currSelected.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        currSelected.GetComponent<SpriteRenderer>().sortingLayerName = "Defalut";

        
        var comps2 = currSelected.GetComponents<IPickable>();

        if (comps2 != null)
        {
            foreach (var comp in comps2)
            {
                comp.OnPickUp();
            }
        }

        currSelected.GetComponent<BoxCollider2D>().enabled = false;


        SceneManager.MoveGameObjectToScene(gunToDrop.gameObject, SceneManager.GetActiveScene());

        currSelected = null;
    }
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Weapon" && currSelected == null)
        {
            currSelected = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currSelected == collision.transform.gameObject)
        {
            currSelected = null;
        }
    }
}
