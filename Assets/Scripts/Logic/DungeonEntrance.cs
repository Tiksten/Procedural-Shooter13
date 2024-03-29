using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        Progress.inRaid = !Progress.inRaid;

        Progress.Save();

        SceneManager.LoadScene(sceneIndex);

        collision.transform.position = Vector3.zero;
    }
}
