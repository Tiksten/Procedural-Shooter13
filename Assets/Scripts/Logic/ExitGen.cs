using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGen : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> exits;

    private void Start()
    {
        foreach (GameObject go in exits)
        {
            go.SetActive(false);
        }

        var popInd = Random.Range(0, exits.Count);
        exits[popInd].SetActive(true);
        exits.RemoveAt(popInd);

        exits[Random.Range(0, exits.Count)].SetActive(true);
    }
}
