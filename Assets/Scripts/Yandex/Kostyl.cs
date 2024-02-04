using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kostyl : MonoBehaviour
{
    [SerializeField]
    private Progress progress;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
#if !UNITY_EDITOR
            progress.YebaniyKostyl();
#endif
            Destroy(this);
        }
    }
}
