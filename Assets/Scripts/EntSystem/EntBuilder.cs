using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntBuilder : MonoBehaviour
{
    [SerializeField]
    private EntType type;

    [Serializable]
    private enum EntType
    {
        Gun,
        Enemy
    }

    private void Start()
    {
        if(type == EntType.Gun)
        {
            gameObject.tag = "Weapon";


            gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<BoxCollider2D>().size = Vector2.one * 0.64f;


            gameObject.AddComponent<Gun>();
            if (Resources.Load<GunStats>("Stats/Weapons/" + name) != null)
                gameObject.GetComponent<Gun>().stats = Resources.Load<GunStats>("Stats/Weapons/" + name).gunData;
            else
                gameObject.GetComponent<Gun>().stats = SettingsStructLib.GunData.def;
            gameObject.GetComponent<Gun>().enabled = false;


            gameObject.AddComponent<SpriteRenderer>();
            if (Resources.Load<UnityEngine.U2D.SpriteAtlas>("Textures/Guns").GetSprite(name) != null)
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<UnityEngine.U2D.SpriteAtlas>("Textures/Guns").GetSprite(name);
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<UnityEngine.U2D.SpriteAtlas>("Textures/Guns").GetSprite("Default");
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Loot";
        }
        if (type == EntType.Enemy)
        {

        }

        Destroy(this);
    }
}
