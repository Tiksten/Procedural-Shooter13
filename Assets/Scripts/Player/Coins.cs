using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (Progress.playerInfo == null)
            return;

        text.text = Progress.playerInfo.coins.ToString();
    }
}
