using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();    
    }

    private void Update()
    {
        var dir = (Vector3.Scale(cam.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono) - transform.parent.position, new Vector3(1, 1))).normalized;

        transform.localPosition = dir * 0.3f;

        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
    }
}
