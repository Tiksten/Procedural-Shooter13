using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "GunStat", menuName = "GunStats")]
public class GunStats : ScriptableObject
{
    [SerializeField]
    public SettingsStructLib.GunData gunData;
}
