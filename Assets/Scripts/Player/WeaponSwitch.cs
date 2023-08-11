using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [HideInInspector]
    public int selectedWeapon = 0;

    [HideInInspector]
    public bool canChange = true;

    private int prevWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (transform.childCount - 1 >= 0)
            {
                selectedWeapon = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (transform.childCount - 1 >= 1)
            {
                selectedWeapon = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (transform.childCount - 1 >= 2)
            {
                selectedWeapon = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (transform.childCount - 1 >= 3)
            {
                selectedWeapon = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (transform.childCount - 1 >= prevWeapon)
            {
                selectedWeapon = prevWeapon;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f & (canChange))
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f & (canChange))
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            prevWeapon = previousSelectedWeapon;
            SelectWeapon();
        }
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }

            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
