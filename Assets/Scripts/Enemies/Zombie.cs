using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Transform target;

    private Rigidbody2D rb;

    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float dmg = 20;

    [SerializeField]
    private float cycletime = 0.25f;

    [SerializeField]
    private float acceleration = 1;

    [SerializeField]
    private Vector2Int coinCount = new Vector2Int(1, 5);

    private bool canDmg;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Sounds/Battle/Bite"));

            canDmg = false;
            collision.collider.GetComponent<Health>().RecieveDmg(dmg);
            Invoke("Reset", cycletime);
        }
    }

    private void Reset()
    {
        canDmg = true;
    }

    private void OnDestroy()
    {
        if (GetComponent<Health>().hp <= 0)
        {
            for (var i = Random.Range(coinCount.x, coinCount.y); i > 0; i--)
            {
                var dir = Random.insideUnitCircle;

                var hit = Physics2D.Raycast(transform.position, dir, 2);

                if (hit.point!=null)
                {
                    Instantiate(coin, hit.point, Quaternion.identity);
                }
                else
                {
                    Instantiate(coin, (Vector2)transform.position + dir * 2, Quaternion.identity);
                }
            }
        }
    }
}
