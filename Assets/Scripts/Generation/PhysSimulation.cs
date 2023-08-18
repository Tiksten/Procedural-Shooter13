using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysSimulation : MonoBehaviour
{
    public UnityEvent OnFinish;

    [SerializeField]
    private GameObject test;

    [SerializeField]
    private float step = 1;

    [SerializeField]
    private int simulation = 5;

    private void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.Script;

        Instantiate(test, transform);

        Invoke("Simulate", 0.1f);
    }

    private void Simulate()
    {
        for (int i = 0; i < simulation; i++)
            Physics2D.Simulate(step);

        if (OnFinish != null)
            OnFinish.Invoke();

        Destroy(gameObject);
    }
}
