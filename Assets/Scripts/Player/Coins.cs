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
        text.text = PlayerPrefs.GetInt("Coins", 0).ToString();
    }
}
