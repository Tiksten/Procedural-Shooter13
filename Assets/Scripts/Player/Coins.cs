using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public static Coins instance;

    [SerializeField]
    private Text text;

    private void Start()
    {
        instance = this;
        UpdateText();
    }

    public void UpdateText()
    {
        if (Progress.playerInfo == null)
            return;

        if(Progress.inRaid)
            text.text = "Монет в рейде: " + Progress.raidCoins.ToString();
        else
            text.text = "Монет на базе: " + Progress.playerInfo.coins.ToString();
    }
}
