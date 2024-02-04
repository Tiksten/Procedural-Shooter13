using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField]
    private float startSpeed = 5;

    [SerializeField]
    private float startTorque = 360;


    [SerializeField]
    private float timer = 3;

    [SerializeField]
    private Vector2 dmg = new Vector2(15, 60);

    [SerializeField]
    private float impulse = 5;

    [SerializeField]
    private float dist = 3;

    [SerializeField]
    private GameObject blood;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private float camShake = 0.2f;

    private void Start()
    {
        Invoke("Explode", timer);

        GetComponent<Rigidbody2D>().AddForce(startSpeed * transform.up, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(-startTorque, startTorque), ForceMode2D.Impulse);
    }

    private void Explode()
    {
        var res = Physics2D.OverlapCircleAll(transform.position, dist);

        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 5);

        foreach (Collider2D c in res)
        {
            var dir = c.transform.position - transform.position;

            var k = 1 - (dir.magnitude / dist);

            if (k < 0) continue;

            if (c.GetComponent<Health>() != null)
            {
                c.GetComponent<Health>().RecieveDmg(dmg.x + (dmg.y-dmg.x) * k);
                Destroy(Instantiate(blood, c.transform.position, Quaternion.identity), 2);
            }

            if (c.GetComponent<Rigidbody2D>() != null)
                c.GetComponent<Rigidbody2D>().AddForce(k * dir.normalized * impulse, ForceMode2D.Impulse);
        }

        CameraShaker.Instance.Recoil(Random.insideUnitCircle.normalized, camShake);

        Destroy(gameObject);
    }
}
