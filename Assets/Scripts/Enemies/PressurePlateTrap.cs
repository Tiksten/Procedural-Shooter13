using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateTrap : MonoBehaviour
{
    [SerializeField]
    public UnityEvent OnActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(OnActivated != null)
                OnActivated.Invoke();

            Destroy(this);
        }
    }
}
