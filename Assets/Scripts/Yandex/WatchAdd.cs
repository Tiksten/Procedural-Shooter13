using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WatchAdd : MonoBehaviour
{
    [SerializeField]
    private GameObject button;

    [DllImport("__Internal")]
    private static extern void AddCoinsExtern(int value);

    private void Start()
    {
        button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        button.SetActive(false);
    }

    public void StartAdvVideo()
    {
#if !UNITY_EDITOR
        AddCoinsExtern(250);
#endif
    }
}
