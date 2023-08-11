using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 100;

    private Health health;

    [SerializeField]
    private float oneHpTime = 0.5f;

    private void Start()
    {
        health = GetComponent<Health>();
        Cycle();
    }

    private void Cycle()
    {
        if (health.hp < maxHp)
            health.RecieveDmg(-1);

        Invoke("Cycle", oneHpTime);
    }
}
