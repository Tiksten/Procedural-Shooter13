using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour enableOnEyeContact;

    [SerializeField]
    private Vector2 tickTime = new Vector2(1, 2);

    private Transform target;

    [SerializeField]
    private float viewDist = 4;

    [SerializeField]
    private LayerMask layerMask;

    private Vector3 currTarget = Vector3.zero;

    private Rigidbody2D rb;

    private void Start()
    {
        enableOnEyeContact.enabled = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        currTarget = transform.position;
        rb = GetComponent<Rigidbody2D>();
        Invoke("Tick", 1);

        if(Random.value < 0.1f)
        {
            GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Sounds/Zombie/" + Random.Range(1, 4).ToString()));
        }    
    }

    private void Tick()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, viewDist, layerMask);

        if (hit.transform != null)
        {
            if (hit.transform.CompareTag("Player"))
            {
                enableOnEyeContact.enabled = true;
                Destroy(this);
                return;
            }
        }

        currTarget = transform.position + (Vector3)Random.insideUnitCircle * Random.Range(2, 5);

        Invoke("Tick", Random.Range(tickTime.x, tickTime.y));
    }

    public void ForceEyeContact()
    {
        enableOnEyeContact.enabled = true;
        Destroy(this);
    }

    private void FixedUpdate()
    {
        if((currTarget - transform.position).magnitude > 0.1f)
            rb.AddForce((currTarget - transform.position).normalized/5, ForceMode2D.Impulse);
    }
}
