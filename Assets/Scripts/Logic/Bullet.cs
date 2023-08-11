using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float velocity = 5;

    public float dmg = 20;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce((Vector2)transform.up * velocity, ForceMode2D.Impulse);
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Health>() != null)
            collision.collider.GetComponent<Health>().RecieveDmg(dmg);

        Destroy(gameObject);
    }
}
