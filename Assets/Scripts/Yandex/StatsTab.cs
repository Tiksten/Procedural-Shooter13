using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsTab : MonoBehaviour
{
    [SerializeField]
    private Text deaths;

    [SerializeField]
    private Text totalDmg;

    private void Start()
    {
        totalDmg.text = "Total Damage: " + Progress.playerInfo.totalDmg.ToString();
        deaths.text = "Total Deaths: " + Progress.playerInfo.deaths.ToString();
    }
}
