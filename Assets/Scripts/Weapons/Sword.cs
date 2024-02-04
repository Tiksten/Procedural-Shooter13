using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IPickable
{
    [SerializeField]
    private float dist = 0.5f;

    [SerializeField]
    private float radius = 0.5f;


    [SerializeField]
    private float dmg = 20;

    [SerializeField]
    private float cycletime = 0.5f;

    [SerializeField]
    private float knockback = 5;

    [SerializeField]
    private GameObject blood;

    [SerializeField]
    private float swingAngle = 90;

    private bool canSwing;

    private float swingTime;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + transform.right * dist, radius);
    }

    public void OnPickUp()
    {
        canSwing = true;
        enabled = true;
    }

    public void OnDrop()
    {
        enabled = false;
    }

    public void Update()
    {
        if (canSwing && Input.GetKeyDown(KeyCode.Mouse0))
            Swing();

        if(swingTime > 0 && !canSwing)
        {
            var angle = swingAngle * Mathf.Sin(Mathf.PI * swingTime / cycletime);

            if (!Hand.prevFlipped)
                angle = -angle;

            transform.localRotation = Quaternion.Euler(0, 0, angle);


            swingTime += Time.deltaTime;
        }

        
    }

    private void Swing()
    {
        canSwing = false;

        var res = Physics2D.OverlapCircleAll(transform.position + transform.right * dist, radius);

        foreach(Collider2D c in res)
        {
            if (c.GetComponent<Health>() != null)
            {
                c.GetComponent<Health>().RecieveDmg(dmg);
                Destroy(Instantiate(blood, c.transform.position, Quaternion.identity), 2);
            }

            if (c.GetComponent<Rigidbody2D>() != null)
                c.GetComponent<Rigidbody2D>().AddForce((c.transform.position - transform.position).normalized * knockback, ForceMode2D.Impulse);
        }

        swingTime += Time.deltaTime;

        Invoke("Reset", cycletime);
    }

    private void Reset()
    {
        canSwing = true;
        swingTime = 0;
        transform.localRotation = Quaternion.identity;
    }
}
