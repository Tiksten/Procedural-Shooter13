using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Camera cam;

    private SpriteRenderer rend;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();    
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var dir = (Vector3.Scale(cam.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono) - transform.parent.position, new Vector3(1, 1))).normalized;

        transform.localPosition = dir * 0.5f;

        transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);

        rend.flipY = dir.x < 0;
    }
}
