using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject hitWall;

    [SerializeField]
    private GameObject hitBody;

    public float velocity = 5;

    public float dmg = 20;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce((Vector2)transform.up * velocity, ForceMode2D.Impulse);
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var effect = hitWall;

        if (collision.collider.GetComponent<Health>() != null)
        {
            collision.collider.GetComponent<Health>().RecieveDmg(dmg);
            effect = hitBody;
        }
        
        Destroy(Instantiate(effect, transform.position, Quaternion.FromToRotation(Vector3.up, (Vector3)collision.contacts[0].normal)), 2);
        
        Destroy(gameObject);
    }
}
