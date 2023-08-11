using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Transform target;

    private Rigidbody2D rb;

    [SerializeField]
    private float dmg = 20;

    [SerializeField]
    private float cycletime = 0.25f;

    [SerializeField]
    private float acceleration = 1;

    private bool canDmg;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        canDmg = true;
    }

    private void FixedUpdate()
    {
        if(target!=null)
            rb.AddForce((((Vector2)target.position - (Vector2)transform.position).normalized + Random.insideUnitCircle) * acceleration, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && canDmg)
        {
            canDmg = false;
            collision.collider.GetComponent<Health>().RecieveDmg(dmg);
            Invoke("Reset", cycletime);
        }
    }

    private void Reset()
    {
        canDmg = true;
    }
}
