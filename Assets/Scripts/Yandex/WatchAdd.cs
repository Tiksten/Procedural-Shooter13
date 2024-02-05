using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WatchAdd : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AddCoinsExtern(int value);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("money");

#if !UNITY_EDITOR
        AddCoinsExtern(1000);
#endif
    }
}
