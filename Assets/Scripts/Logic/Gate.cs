using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private int neededDmg = 100;

    [SerializeField]
    private GameObject movingPart;

    private void Awake()
    {
        if (Progress.isLoaded)
            StartOnLoad();
        else
            Progress.OnLoadDone.AddListener(StartOnLoad);
    }

    private void StartOnLoad()
    {
        if(Progress.playerInfo.totalDmg > neededDmg)
        {
            Destroy(movingPart);
        }
    }

    private void OnDestroy()
    {
        Progress.OnLoadDone.RemoveListener(StartOnLoad);
    }
}
