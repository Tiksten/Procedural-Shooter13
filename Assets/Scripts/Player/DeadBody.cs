using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadBody : MonoBehaviour
{
    [SerializeField]
    private GameObject newPlayer;

    private void Start()
    {
        Progress.playerInfo.coins = (int)(Progress.playerInfo.coins * 0.8f);
        Progress.playerInfo.deaths += 1;
        Invoke("Restart", 10);
        Destroy(FindObjectOfType<Player>().gameObject);

    #if !UNITY_EDITOR
        Progress.Save();
    #endif
    }

    private void Restart()
    {
        Instantiate(newPlayer, Vector3.zero, Quaternion.identity);
        SceneManager.LoadScene(2);
    }
}
