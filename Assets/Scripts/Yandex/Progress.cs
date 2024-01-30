using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int coins;
    public int deaths;
    public float totalDmg;
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

    private void Start()
    {
        playerInfo = new PlayerInfo();
        DontDestroyOnLoad(this);
#if !UNITY_EDITOR
        LoadExtern();
#endif  
        FindObjectOfType<Coins>().UpdateText();
    }


    //external call with LoadExtern (only once: at load)
    public void SetPlayerInfo(string value)
    {
        playerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        if(playerInfo == null)
        {
            playerInfo = new PlayerInfo();
        }
        playerInfo.coins += 10000;
    }

    public static void Save()
    {
#if !UNITY_EDITOR
        string jsonString = JsonUtility.ToJson(playerInfo);
        SaveExtern(jsonString);
#endif
    }
}