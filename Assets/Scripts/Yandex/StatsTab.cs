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

    private void OnEnable()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (Progress.playerInfo == null)
            return;

        totalDmg.text = "Всего Урона: " + Progress.playerInfo.totalDmg.ToString();
        deaths.text = "Всего Смертей: " + Progress.playerInfo.deaths.ToString();
    }
}
