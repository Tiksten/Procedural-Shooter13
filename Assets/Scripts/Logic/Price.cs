using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Price : MonoBehaviour
{
    public int price = 100;

    public UnityEvent onPaid;

    private void OnDestroy()
    {
        if(onPaid != null) onPaid.Invoke();
    }
}
