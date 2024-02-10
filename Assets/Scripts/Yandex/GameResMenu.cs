using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameResMenu : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject btn;

    [SerializeField]
    private Text coins;

    [SerializeField]
    private Text dmg;

    [SerializeField]
    private Text deaths;

    private void Start()
    {
        btn.SetActive(false);
        Progress.canAct = false;
        Invoke("ShowBanner", 0.5f);

        coins.text = "Всего монет: " + Progress.playerInfo.coins.ToString();
        dmg.text = "Всего урона: " + Progress.playerInfo.totalDmg.ToString();
        deaths.text = "Всего смертей: " + Progress.playerInfo.deaths.ToString();
    }

    private void ShowBanner()
    {
#if !UNITY_EDITOR
        ShowAdv();
#endif
        
        btn.SetActive(true);
    }


    public void OnBtnClick()
    {
        Progress.canAct = true;
        Destroy(menu);
    }
}
