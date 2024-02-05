using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;

    [SerializeField]
    private bool showAdv;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        Player.inRaid = !Player.inRaid;

        Progress.Save(showAdv);

        SceneManager.LoadScene(sceneIndex);

        collision.transform.position = Vector3.zero;
    }
}
