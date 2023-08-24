using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;



public class SettingsStructLib
{
    [Serializable]
    public struct GunData
    {
        public float dmg;

        public float cycletime;

        public bool isFullAuto;

        public int pellets;

        public float inaccuracy;

        [Space(10)]

        public int price;

        public int consumesAmmo;

        public float shakeAmplitude;

        [Space(10)]

        public GameObject bullet;

        public float bulletSpeed;

        public float bulletSize;



        public static GunData def
        {
            get
            {
                GunData data = new GunData();


                data.dmg = 15;

                data.cycletime = 0.1f;

                data.isFullAuto = false;

                data.pellets = 1;

                data.inaccuracy = 0.1f;



                data.price = 5;

                data.consumesAmmo = 2;

                data.shakeAmplitude = 0.15f;



                data.bullet = Resources.Load<GameObject>("Prefabs/Projectiles/Bullet");

                data.bulletSpeed = 10;

                data.bulletSize = 0.2f;
                
                return data;
            }
        }
            
        
    }
}

[CreateAssetMenu(fileName = "Gun", menuName = "Stats")]
public class GunStats : ScriptableObject
{
    public SettingsStructLib.GunData gunData;
}