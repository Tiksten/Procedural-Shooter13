using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

[System.Serializable]
public class PlayerInfo
{
    public int coins;
    public int deaths;
    public int totalDmg;
}

//one instance in hub
[Singleton]
public class Progress : MonoBehaviour
{
    public static PlayerInfo playerInfo;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);

    public static UnityEvent OnLoadDone = new UnityEvent();

    public static bool isLoaded;

    public static int raidCoins;

    public static bool canAct = true;

    public static bool inRaid;

    private void Start()
    {
        playerInfo = new PlayerInfo();
        DontDestroyOnLoad(this);
    }


    //external call with LoadExtern (only once: at load; in: YebaniyKostyl)
    public void SetPlayerInfo(string value)
    {
        playerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        if(playerInfo == null)
        {
            playerInfo = new PlayerInfo();
        }
    }

    public static void Save()
    {
        playerInfo.coins += raidCoins;
        raidCoins = 0;

        Coins.instance.UpdateText();

#if !UNITY_EDITOR
        string jsonString = JsonUtility.ToJson(playerInfo);
        SaveExtern(jsonString);

        SetToLeaderboard(playerInfo.totalDmg);
#endif
    }

    public void YebaniyKostyl()
    {
        LoadExtern();

        FindObjectOfType<Coins>().UpdateText();

        if (FindObjectOfType<StatsTab>() != null)
            FindObjectOfType<StatsTab>().UpdateVisuals();

        Invoke("KostylEnding", 1);
    }

    private void KostylEnding()
    {
        FindObjectOfType<Coins>().UpdateText();

        if (FindObjectOfType<StatsTab>() != null)
            FindObjectOfType<StatsTab>().UpdateVisuals();

        if (OnLoadDone != null)
            OnLoadDone.Invoke();

        isLoaded = true;
    }

    public void AddCoins(int value)
    {
        playerInfo.coins += value;
        Save();
        Coins.instance.UpdateText();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        AudioListener.volume = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }
}
