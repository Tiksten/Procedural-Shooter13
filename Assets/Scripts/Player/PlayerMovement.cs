using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 input;

    private Rigidbody2D rb;

    [SerializeField]
    private float acceleration = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Progress.canAct)
        {
            rb.velocity = Vector2.zero;
            transform.position = Vector3.zero;
            return;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        rb.AddForce(input.normalized * acceleration, ForceMode2D.Impulse);
    }
}
