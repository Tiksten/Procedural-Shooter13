using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool inRaid;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
