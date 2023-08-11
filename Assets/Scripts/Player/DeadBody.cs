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
        PlayerPrefs.SetInt("Coins", Mathf.FloorToInt(PlayerPrefs.GetInt("Coins", 0)*0.8f));
        Invoke("Restart", 3);
        Destroy(FindObjectOfType<Player>().gameObject);
    }

    private void Restart()
    {
        Instantiate(newPlayer, Vector3.zero, Quaternion.identity);
        SceneManager.LoadScene(2);
    }
}
