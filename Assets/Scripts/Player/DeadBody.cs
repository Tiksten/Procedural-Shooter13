using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadBody : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("Coins", Mathf.FloorToInt(PlayerPrefs.GetInt("Coins", 0)*0.8f));
        Invoke("Restart", 3);
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
