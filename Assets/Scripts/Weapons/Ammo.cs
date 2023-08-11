using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//delete me
public class Ammo : MonoBehaviour
{
    [SerializeField]
    private float oneAmmoTime = 0.2f;

    [SerializeField]
    private int maxAmmo = 100;

    public int currAmmo;

    [SerializeField]
    private Slider slider;

    private void Start()
    {
        currAmmo = maxAmmo;
        Cycle();
    }

    private void Cycle()
    {
        slider.value = currAmmo;

        if (currAmmo < maxAmmo)
            currAmmo += 1;

        Invoke("Cycle", oneAmmoTime);
    }
}
